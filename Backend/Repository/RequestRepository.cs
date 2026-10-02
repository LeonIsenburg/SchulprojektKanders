using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Repository.Interfaces;
using Backend.Models.Event;
using Backend.Models.Location;
using Backend.Models.Music;
using Backend.Models.Relations;
using Backend.Models.Requests;

namespace Backend.Repository
{
    public class RequestRepository : iRequestRepository
    {
        private readonly AppDbContext context;

        public RequestRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Event> Create(EventRequest request, Guid memberId)
        {
            if (!await context.Member.AnyAsync(m => m.Guid == memberId))
                throw new ArgumentException($"Member '{memberId}' existiert nicht.");

            var (name, dates) = Validate(request);

            var newEvent = new Event
            {
                Guid = Guid.NewGuid(),
                EventID = 0,
                Name = name,
                Type = ResolveType(request.Type, dates),
                Status = ParseStatus(request.Status),
                MemberId = memberId
            };
            await ApplyDetailsAsync(newEvent, request);
            context.Event.Add(newEvent);

            await AddDatesAndArtistsAsync(newEvent, dates, request.Artists);

            await context.SaveChangesAsync();
            return (await GetById(newEvent.Guid))!;
        }

        public async Task<List<Event>> GetAll()
        {
            return await QueryWithDetails()
                .OrderBy(e => e.Dates.Min(d => d.Start))
                .ToListAsync();
        }

        public async Task<Event?> GetById(Guid id)
        {
            return await QueryWithDetails().FirstOrDefaultAsync(e => e.Guid == id);
        }

        public async Task<Event?> Update(Guid id, EventRequest request)
        {
            var existing = await context.Event
                .Include(e => e.Dates)
                .Include(e => e.Artists)
                .FirstOrDefaultAsync(e => e.Guid == id);
            if (existing is null) return null;

            var (name, dates) = Validate(request);

            await using var transaction = await context.Database.BeginTransactionAsync();

            context.EventDate.RemoveRange(existing.Dates);
            context.EventArtist.RemoveRange(existing.Artists);
            await context.SaveChangesAsync();

            existing.Name = name;
            existing.Type = ResolveType(request.Type, dates);
            if (request.Status is not null) existing.Status = ParseStatus(request.Status);
            await ApplyDetailsAsync(existing, request);

            await AddDatesAndArtistsAsync(existing, dates, request.Artists);

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetById(id);
        }

        public async Task<Event?> UpdateStatus(Guid id, Status status)
        {
            var existing = await context.Event.FirstOrDefaultAsync(e => e.Guid == id);
            if (existing is null) return null;

            existing.Status = status;
            await context.SaveChangesAsync();

            return await GetById(id);
        }

        public async Task<bool> Delete(Guid id)
        {
            var existing = await context.Event.FirstOrDefaultAsync(e => e.Guid == id);
            if (existing is null) return false;

            context.Event.Remove(existing);
            await context.SaveChangesAsync();
            return true;
        }

        private IQueryable<Event> QueryWithDetails()
        {
            return context.Event
                .AsNoTracking()
                .AsSplitQuery()
                .Include(e => e.Organizer)
                .Include(e => e.Dates).ThenInclude(d => d.ValidLocation!).ThenInclude(v => v.City)
                .Include(e => e.Dates).ThenInclude(d => d.ValidLocation!).ThenInclude(v => v.District)
                .Include(e => e.Dates).ThenInclude(d => d.ValidLocation!).ThenInclude(v => v.Region)
                .Include(e => e.Dates).ThenInclude(d => d.ValidLocation!).ThenInclude(v => v.State)
                .Include(e => e.Dates).ThenInclude(d => d.ValidLocation!).ThenInclude(v => v.EventLocation)
                .Include(e => e.Artists).ThenInclude(a => a.Band).ThenInclude(b => b.Musicians).ThenInclude(bm => bm.Musician)
                .Include(e => e.Artists).ThenInclude(a => a.Band).ThenInclude(b => b.MusicGenres).ThenInclude(bg => bg.MusicGenre)
                .Include(e => e.Artists).ThenInclude(a => a.Band).ThenInclude(b => b.Songs).ThenInclude(bs => bs.Song);
        }

        private static (string Name, List<EventDateRequest> Dates) Validate(EventRequest request)
        {
            var name = Clean(request.Name);
            var dates = (request.Dates ?? []).Where(d => d?.Start is not null).Select(d => d!).ToList();

            var missing = new List<string>();
            if (name is null) missing.Add("name");
            if (dates.Count == 0) missing.Add("dates[].start");
            if (missing.Count > 0)
                throw new ArgumentException($"Es fehlen Pflichtangaben: {string.Join(", ", missing)}.");

            return (name!, dates);
        }

        private static EventType ResolveType(string? type, List<EventDateRequest> dates)
        {
            if (TryParseEnum<EventType>(type, out var parsed))
                return parsed;

            if (dates.Count > 1)
                return EventType.Tour;

            var first = dates[0];
            if (first.End is not null && first.End.Value.Date > first.Start!.Value.Date)
                return EventType.Festival;

            return EventType.Concert;
        }

        private static Status ParseStatus(string? status)
        {
            return TryParseEnum<Status>(status, out var parsed) ? parsed : Status.Planned;
        }

        private static bool TryParseEnum<T>(string? value, out T result) where T : struct, Enum
        {
            result = default;
            var normalized = Clean(value)?.Replace("_", "").Replace("-", "").Replace(" ", "");
            if (normalized is null || int.TryParse(normalized, out _)) return false;

            return Enum.TryParse(normalized, ignoreCase: true, out result) && Enum.IsDefined(result);
        }

        private static string? Clean(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private async Task ApplyDetailsAsync(Event @event, EventRequest request)
        {
            var organizer = await GetOrCreateOrganizerAsync(request.Organizer);

            @event.OrganizerId = organizer?.Guid;
            @event.AgeRestriction = request.AgeRestriction;
            @event.Description = Clean(request.Description);
            @event.Website = Clean(request.Website);
            @event.Instagram = Clean(request.Socials?.Instagram);
            @event.Facebook = Clean(request.Socials?.Facebook);
            @event.TikTok = Clean(request.Socials?.Tiktok);
            @event.ExtraInfo = (request.ExtraInfo ?? []).Select(Clean).Where(i => i is not null).Select(i => i!).ToList();
        }

        private async Task<Organizer?> GetOrCreateOrganizerAsync(OrganizerRequest? request)
        {
            var name = Clean(request?.Name);
            if (name is null) return null;

            var organizer = await GetOrCreateAsync(context.Organizer, o => o.Name == name,
                () => new Organizer { Guid = Guid.NewGuid(), OrganizerID = 0, Name = name });

            var website = Clean(request!.Website);
            if (website is not null) organizer.Website = website;

            return organizer;
        }

        private async Task AddDatesAndArtistsAsync(Event @event, List<EventDateRequest> dates, List<ArtistRequest?>? artists)
        {
            foreach (var (position, date) in dates.Index())
            {
                var location = await GetOrCreateLocationAsync(date.Location);

                context.EventDate.Add(new EventDate
                {
                    Guid = Guid.NewGuid(),
                    EventId = @event.Guid,
                    Position = position,
                    Start = date.Start!.Value,
                    End = date.End,
                    DoorsOpen = date.DoorsOpen,
                    ValidLocationId = location?.Guid,
                    Price = date.Tickets?.Price,
                    Currency = Clean(date.Tickets?.Currency),
                    Presale = date.Tickets?.Presale,
                    TicketUrl = Clean(date.Tickets?.Url)
                });
            }

            var addedBandIds = new HashSet<Guid>();
            foreach (var artist in artists ?? [])
            {
                var bandName = Clean(artist?.Name);
                if (bandName is null) continue;

                var band = await GetOrCreateBandAsync(bandName, artist!);
                if (!addedBandIds.Add(band.Guid)) continue;

                context.EventArtist.Add(new EventArtist
                {
                    Guid = Guid.NewGuid(),
                    EventId = @event.Guid,
                    BandId = band.Guid,
                    Position = addedBandIds.Count - 1,
                    Performance = artist!.Performance
                });
            }
        }

        private async Task<Band> GetOrCreateBandAsync(string name, ArtistRequest request)
        {
            var band = await GetOrCreateAsync(context.Band, b => b.Name == name,
                () => new Band { Guid = Guid.NewGuid(), BandID = 0, Name = name });

            var members = (request.Members ?? [])
                .Where(m => m is not null)
                .Select(m => (
                    ArtistName: Clean(m!.ArtistName) ?? Clean($"{m.FirstName} {m.LastName}"),
                    FirstName: Clean(m.FirstName),
                    LastName: Clean(m.LastName)))
                .Where(m => m.ArtistName is not null)
                .ToList();

            foreach (var (position, member) in members.Index())
            {
                var artistName = member.ArtistName!;
                var musician = await GetOrCreateAsync(context.Musician, m => m.ArtistName == artistName,
                    () => new Musician
                    {
                        Guid = Guid.NewGuid(),
                        MusicianID = 0,
                        ArtistName = artistName,
                        FirstName = member.FirstName,
                        LastName = member.LastName
                    });
                musician.FirstName ??= member.FirstName;
                musician.LastName ??= member.LastName;

                var alreadyLinked = context.BandMusician.Local.Any(bm => bm.BandId == band.Guid && bm.MusicianId == musician.Guid)
                    || await context.BandMusician.AnyAsync(bm => bm.BandId == band.Guid && bm.MusicianId == musician.Guid);
                if (!alreadyLinked)
                {
                    context.BandMusician.Add(new BandMusician
                    {
                        Guid = Guid.NewGuid(),
                        BandId = band.Guid,
                        MusicianId = musician.Guid,
                        Position = position
                    });
                }
            }

            var genres = (request.Genres ?? []).Select(Clean).Where(g => g is not null).Select(g => g!).ToList();
            foreach (var (position, genreDescription) in genres.Index())
            {
                var genre = await GetOrCreateAsync(context.MusicGenre, g => g.Description == genreDescription,
                    () => new MusicGenre { Guid = Guid.NewGuid(), MusicGenreID = 0, Description = genreDescription });

                var alreadyLinked = context.BandMusicGenre.Local.Any(bg => bg.BandId == band.Guid && bg.MusicGenreId == genre.Guid)
                    || await context.BandMusicGenre.AnyAsync(bg => bg.BandId == band.Guid && bg.MusicGenreId == genre.Guid);
                if (!alreadyLinked)
                {
                    context.BandMusicGenre.Add(new BandMusicGenre
                    {
                        Guid = Guid.NewGuid(),
                        BandId = band.Guid,
                        MusicGenreId = genre.Guid,
                        Position = position
                    });
                }
            }

            var songs = (request.Songs ?? []).Select(Clean).Where(s => s is not null).Select(s => s!).ToList();
            foreach (var (position, songName) in songs.Index())
            {
                var song = await GetOrCreateAsync(context.Song, s => s.Name == songName,
                    () => new Song { Guid = Guid.NewGuid(), SongID = 0, Name = songName });

                var alreadyLinked = context.BandSong.Local.Any(bs => bs.BandId == band.Guid && bs.SongId == song.Guid)
                    || await context.BandSong.AnyAsync(bs => bs.BandId == band.Guid && bs.SongId == song.Guid);
                if (!alreadyLinked)
                {
                    context.BandSong.Add(new BandSong
                    {
                        Guid = Guid.NewGuid(),
                        BandId = band.Guid,
                        SongId = song.Guid,
                        Position = position
                    });
                }
            }

            return band;
        }

        private async Task<ValidLocation?> GetOrCreateLocationAsync(LocationRequest? request)
        {
            if (request is null) return null;

            var venue = Clean(request.Venue);
            var street = Clean(request.Street);
            var houseNumber = Clean(request.HouseNumber);
            var postalCode = Clean(request.PostalCode);
            var cityName = Clean(request.City);
            var districtName = Clean(request.District);
            var regionName = Clean(request.Region);
            var stateName = Clean(request.State);
            var country = Clean(request.Country);

            if (new[] { venue, street, houseNumber, postalCode, cityName, districtName, regionName, stateName, country }.All(v => v is null))
                return null;

            var cityId = cityName is null ? (Guid?)null : (await GetOrCreateAsync(context.Cities, c => c.Name == cityName,
                () => new City { Guid = Guid.NewGuid(), CityId = 0, Name = cityName })).Guid;

            var districtId = districtName is null ? (Guid?)null : (await GetOrCreateAsync(context.District, d => d.Name == districtName,
                () => new District { Guid = Guid.NewGuid(), DistrictID = 0, Name = districtName })).Guid;

            var regionId = regionName is null ? (Guid?)null : (await GetOrCreateAsync(context.Region, r => r.Name == regionName,
                () => new Region { Guid = Guid.NewGuid(), RegionID = 0, Name = regionName })).Guid;

            var stateId = stateName is null ? (Guid?)null : (await GetOrCreateAsync(context.State, s => s.Name == stateName,
                () => new State { Guid = Guid.NewGuid(), StateID = 0, Name = stateName })).Guid;

            var eventLocationId = venue is null ? (Guid?)null : (await GetOrCreateAsync(context.EventLocation, l => l.Name == venue,
                () => new EventLocation { Guid = Guid.NewGuid(), Event_LocationID = 0, Name = venue })).Guid;

            return await GetOrCreateAsync(context.ValidLocation, v =>
                    v.EventLocationId == eventLocationId &&
                    v.Street == street &&
                    v.HouseNumber == houseNumber &&
                    v.PostalCode == postalCode &&
                    v.CityId == cityId &&
                    v.DistrictId == districtId &&
                    v.RegionId == regionId &&
                    v.StateId == stateId &&
                    v.Country == country,
                () => new ValidLocation
                {
                    Guid = Guid.NewGuid(),
                    ValidLocationID = 0,
                    PostalCode = postalCode,
                    Street = street,
                    HouseNumber = houseNumber,
                    Country = country,
                    CityId = cityId,
                    DistrictId = districtId,
                    RegionId = regionId,
                    StateId = stateId,
                    EventLocationId = eventLocationId
                });
        }

        private static async Task<T> GetOrCreateAsync<T>(DbSet<T> set, Expression<Func<T, bool>> predicate, Func<T> factory)
            where T : class
        {
            var existing = set.Local.FirstOrDefault(predicate.Compile())
                ?? await set.FirstOrDefaultAsync(predicate);
            if (existing is not null) return existing;

            var created = factory();
            set.Add(created);
            return created;
        }
    }
}
