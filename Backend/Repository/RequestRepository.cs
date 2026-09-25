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

            var location = await GetOrCreateLocationAsync(request.Location);

            var newEvent = new Event
            {
                Guid = Guid.NewGuid(),
                EventID = 0,
                StartDate = request.StartDate,
                StartTime = request.StartTime,
                StartDay = request.StartDate.DayOfWeek.ToString(),
                EntryTime = request.EntryTime,
                Price = request.Price,
                PriceType = request.PriceType,
                EventType = request.EventType,
                Status = Status.Geplant,
                MemberId = memberId,
                ValidLocationId = location.Guid
            };
            context.Event.Add(newEvent);

            await CreateSubEventAsync(newEvent, request);

            await context.SaveChangesAsync();
            return (await GetById(newEvent.Guid))!;
        }

        public async Task<List<Event>> GetAll()
        {
            return await QueryWithDetails().ToListAsync();
        }

        public async Task<Event?> GetById(Guid id)
        {
            return await QueryWithDetails().FirstOrDefaultAsync(e => e.Guid == id);
        }

        public async Task<Event?> Update(Guid id, EventRequest request)
        {
            var existing = await context.Event
                .Include(e => e.Concert)
                .Include(e => e.Festival)
                .Include(e => e.Party)
                .FirstOrDefaultAsync(e => e.Guid == id);
            if (existing is null) return null;

            await using var transaction = await context.Database.BeginTransactionAsync();

            // Altes Concert/Festival/Party zuerst löschen, sonst kollidiert der eindeutige Index auf EventId
            if (existing.Concert is not null) context.Concert.Remove(existing.Concert);
            if (existing.Festival is not null) context.Festival.Remove(existing.Festival);
            if (existing.Party is not null) context.Party.Remove(existing.Party);
            await context.SaveChangesAsync();

            var location = await GetOrCreateLocationAsync(request.Location);

            existing.StartDate = request.StartDate;
            existing.StartTime = request.StartTime;
            existing.StartDay = request.StartDate.DayOfWeek.ToString();
            existing.EntryTime = request.EntryTime;
            existing.Price = request.Price;
            existing.PriceType = request.PriceType;
            existing.EventType = request.EventType;
            existing.ValidLocationId = location.Guid;

            await CreateSubEventAsync(existing, request);

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
                .Include(e => e.ValidLocation).ThenInclude(v => v.City)
                .Include(e => e.ValidLocation).ThenInclude(v => v.District)
                .Include(e => e.ValidLocation).ThenInclude(v => v.Region)
                .Include(e => e.ValidLocation).ThenInclude(v => v.State)
                .Include(e => e.ValidLocation).ThenInclude(v => v.EventLocation)
                .Include(e => e.Concert!).ThenInclude(c => c.Bands).ThenInclude(cb => cb.Band).ThenInclude(b => b.Musicians).ThenInclude(bm => bm.Musician)
                .Include(e => e.Concert!).ThenInclude(c => c.Bands).ThenInclude(cb => cb.Band).ThenInclude(b => b.MusicGenres).ThenInclude(bg => bg.MusicGenre)
                .Include(e => e.Concert!).ThenInclude(c => c.Bands).ThenInclude(cb => cb.Band).ThenInclude(b => b.Songs).ThenInclude(bs => bs.Song)
                .Include(e => e.Festival!).ThenInclude(f => f.FestivalName)
                .Include(e => e.Festival!).ThenInclude(f => f.Bands).ThenInclude(fb => fb.Band).ThenInclude(b => b.Musicians).ThenInclude(bm => bm.Musician)
                .Include(e => e.Festival!).ThenInclude(f => f.Bands).ThenInclude(fb => fb.Band).ThenInclude(b => b.MusicGenres).ThenInclude(bg => bg.MusicGenre)
                .Include(e => e.Festival!).ThenInclude(f => f.Bands).ThenInclude(fb => fb.Band).ThenInclude(b => b.Songs).ThenInclude(bs => bs.Song)
                .Include(e => e.Party!).ThenInclude(p => p.PartyName)
                .Include(e => e.Party!).ThenInclude(p => p.Bands).ThenInclude(pb => pb.Band).ThenInclude(b => b.Musicians).ThenInclude(bm => bm.Musician)
                .Include(e => e.Party!).ThenInclude(p => p.Bands).ThenInclude(pb => pb.Band).ThenInclude(b => b.MusicGenres).ThenInclude(bg => bg.MusicGenre)
                .Include(e => e.Party!).ThenInclude(p => p.Bands).ThenInclude(pb => pb.Band).ThenInclude(b => b.Songs).ThenInclude(bs => bs.Song);
        }

        private async Task CreateSubEventAsync(Event @event, EventRequest request)
        {
            switch (request.EventType)
            {
                case EventType.Konzert:
                    if (request.Concert is null)
                        throw new ArgumentException("EventType ist Konzert, aber es wurden keine Concert-Daten mitgeschickt.");
                    await CreateConcertAsync(@event, request.Concert);
                    break;

                case EventType.Festival:
                    if (request.Festival is null)
                        throw new ArgumentException("EventType ist Festival, aber es wurden keine Festival-Daten mitgeschickt.");
                    await CreateFestivalAsync(@event, request.Festival);
                    break;

                case EventType.Party:
                    if (request.Party is null)
                        throw new ArgumentException("EventType ist Party, aber es wurden keine Party-Daten mitgeschickt.");
                    await CreatePartyAsync(@event, request.Party);
                    break;
            }
        }

        private async Task CreateConcertAsync(Event @event, ConcertRequest request)
        {
            if (request.Bands.Count == 0)
                throw new ArgumentException("Ein Concert braucht mindestens eine Band.");

            var bands = new List<Band>();
            foreach (var bandRequest in request.Bands)
                bands.Add(await GetOrCreateBandAsync(bandRequest));
            bands = bands.DistinctBy(b => b.Guid).ToList();

            var concert = new Concert
            {
                Guid = Guid.NewGuid(),
                ConcertID = 0,
                Organizer = request.Organizer,
                EventId = @event.Guid,
                BandId = bands[0].Guid
            };
            context.Concert.Add(concert);

            foreach (var band in bands)
            {
                context.ConcertBand.Add(new ConcertBand
                {
                    Guid = Guid.NewGuid(),
                    ConcertId = concert.Guid,
                    BandId = band.Guid
                });
            }
        }

        private async Task CreateFestivalAsync(Event @event, FestivalRequest request)
        {
            if (request.Bands.Count == 0)
                throw new ArgumentException("Ein Festival braucht mindestens eine Band.");

            var festivalName = await GetOrCreateFestivalNameAsync(request.FestivalName);

            var festival = new Festival
            {
                Guid = Guid.NewGuid(),
                FestivalID = 0,
                EndDate = request.EndDate,
                EndTime = request.EndTime,
                EndDay = request.EndDate.DayOfWeek.ToString(),
                Organizer = request.Organizer,
                EventId = @event.Guid,
                FestivalNameId = festivalName.Guid
            };
            context.Festival.Add(festival);

            var addedBandIds = new HashSet<Guid>();
            foreach (var performance in request.Bands)
            {
                var band = await GetOrCreateBandAsync(performance.Band);
                if (!addedBandIds.Add(band.Guid)) continue;

                context.FestivalBand.Add(new FestivalBand
                {
                    Guid = Guid.NewGuid(),
                    FestivalId = festival.Guid,
                    BandId = band.Guid,
                    Date = performance.Date,
                    Time = performance.Time,
                    Day = performance.Day
                });
            }
        }

        private async Task CreatePartyAsync(Event @event, PartyRequest request)
        {
            if (request.Bands.Count == 0)
                throw new ArgumentException("Eine Party braucht mindestens eine Band.");

            var partyName = await GetOrCreatePartyNameAsync(request.PartyName);

            var party = new Party
            {
                Guid = Guid.NewGuid(),
                PartyID = 0,
                EndDate = request.EndDate,
                EndTime = request.EndTime,
                EndDay = request.EndDate.DayOfWeek.ToString(),
                Organizer = request.Organizer,
                EventId = @event.Guid,
                PartyNameId = partyName.Guid
            };
            context.Party.Add(party);

            var addedBandIds = new HashSet<Guid>();
            foreach (var bandRequest in request.Bands)
            {
                var band = await GetOrCreateBandAsync(bandRequest);
                if (!addedBandIds.Add(band.Guid)) continue;

                context.PartyBand.Add(new PartyBand
                {
                    Guid = Guid.NewGuid(),
                    PartyId = party.Guid,
                    BandId = band.Guid
                });
            }
        }

        private async Task<Band> GetOrCreateBandAsync(BandRequest request)
        {
            var band = await GetOrCreateAsync(context.Band, b => b.Name == request.Name,
                () => new Band { Guid = Guid.NewGuid(), BandID = 0, Name = request.Name });

            if (request.Musicians.Count == 0)
                throw new ArgumentException($"Band '{request.Name}' braucht mindestens einen Musiker.");

            foreach (var musicianRequest in request.Musicians)
            {
                var musician = await GetOrCreateAsync(context.Musician, m => m.ArtistName == musicianRequest.ArtistName,
                    () => new Musician
                    {
                        Guid = Guid.NewGuid(),
                        MusicianID = 0,
                        ArtistName = musicianRequest.ArtistName,
                        FirstName = musicianRequest.FirstName ?? musicianRequest.ArtistName,
                        LastName = musicianRequest.LastName ?? musicianRequest.ArtistName
                    });

                var alreadyLinked = context.BandMusician.Local.Any(bm => bm.BandId == band.Guid && bm.MusicianId == musician.Guid)
                    || await context.BandMusician.AnyAsync(bm => bm.BandId == band.Guid && bm.MusicianId == musician.Guid);
                if (!alreadyLinked)
                {
                    context.BandMusician.Add(new BandMusician
                    {
                        Guid = Guid.NewGuid(),
                        BandId = band.Guid,
                        MusicianId = musician.Guid
                    });
                }
            }

            foreach (var genreDescription in request.Genres ?? [])
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
                        MusicGenreId = genre.Guid
                    });
                }
            }

            foreach (var songName in request.Songs ?? [])
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
                        SongId = song.Guid
                    });
                }
            }

            return band;
        }

        private async Task<ValidLocation> GetOrCreateLocationAsync(LocationRequest request)
        {
            var city = await GetOrCreateAsync(context.Cities, c => c.Name == request.City,
                () => new City { Guid = Guid.NewGuid(), CityId = 0, Name = request.City });

            var district = await GetOrCreateAsync(context.District, d => d.Name == request.District,
                () => new District { Guid = Guid.NewGuid(), DistrictID = 0, Name = request.District });

            var region = await GetOrCreateAsync(context.Region, r => r.Name == request.Region,
                () => new Region { Guid = Guid.NewGuid(), RegionID = 0, Name = request.Region });

            var state = await GetOrCreateAsync(context.State, s => s.Name == request.State,
                () => new State { Guid = Guid.NewGuid(), StateID = 0, Name = request.State });

            var eventLocation = await GetOrCreateAsync(context.EventLocation, l => l.Name == request.EventLocationName,
                () => new EventLocation { Guid = Guid.NewGuid(), Event_LocationID = 0, Name = request.EventLocationName });

            var existing = await context.ValidLocation.FirstOrDefaultAsync(v =>
                v.CityId == city.Guid &&
                v.DistrictId == district.Guid &&
                v.RegionId == region.Guid &&
                v.StateId == state.Guid &&
                v.EventLocationId == eventLocation.Guid);
            if (existing is not null) return existing;

            var location = new ValidLocation
            {
                Guid = Guid.NewGuid(),
                ValidLocationID = 0,
                PostalCode = request.PostalCode,
                Street = request.Street,
                HouseNumber = request.HouseNumber,
                CityId = city.Guid,
                DistrictId = district.Guid,
                RegionId = region.Guid,
                StateId = state.Guid,
                EventLocationId = eventLocation.Guid
            };
            context.ValidLocation.Add(location);
            return location;
        }

        private Task<FestivalName> GetOrCreateFestivalNameAsync(string name)
        {
            return GetOrCreateAsync(context.FestivalName, f => f.Name == name,
                () => new FestivalName { Guid = Guid.NewGuid(), Festival_NameID = 0, Name = name });
        }

        private Task<PartyName> GetOrCreatePartyNameAsync(string name)
        {
            return GetOrCreateAsync(context.Partyname, p => p.Name == name,
                () => new PartyName { Guid = Guid.NewGuid(), Party_NameID = 0, Name = name });
        }

        private static async Task<T> GetOrCreateAsync<T>(DbSet<T> set, Expression<Func<T, bool>> predicate, Func<T> factory)
            where T : class
        {
            // Erst in den noch nicht gespeicherten Einträgen dieser Anfrage suchen, dann in der Datenbank
            var existing = set.Local.FirstOrDefault(predicate.Compile())
                ?? await set.FirstOrDefaultAsync(predicate);
            if (existing is not null) return existing;

            var created = factory();
            set.Add(created);
            return created;
        }
    }
}
