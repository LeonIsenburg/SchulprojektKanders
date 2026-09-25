using Backend.Models.Location;
using Backend.Models.Music;

namespace Backend.Models.Responses;

public static class EventMapper
{
    public static EventResponse ToResponse(this Backend.Models.Event.Event e)
    {
        return new EventResponse
        {
            Guid = e.Guid,
            StartDate = e.StartDate,
            StartTime = e.StartTime,
            StartDay = e.StartDay,
            EntryTime = e.EntryTime,
            Price = e.Price,
            PriceType = e.PriceType,
            EventType = e.EventType,
            Status = e.Status,
            Location = e.ValidLocation.ToResponse(),
            Concert = e.Concert is null ? null : new ConcertResponse
            {
                Organizer = e.Concert.Organizer,
                Bands = e.Concert.Bands.Select(cb => cb.Band.ToResponse()).ToList()
            },
            Festival = e.Festival is null ? null : new FestivalResponse
            {
                EndDate = e.Festival.EndDate,
                EndTime = e.Festival.EndTime,
                EndDay = e.Festival.EndDay,
                Organizer = e.Festival.Organizer,
                FestivalName = e.Festival.FestivalName.Name,
                Bands = e.Festival.Bands.Select(fb => new BandPerformanceResponse
                {
                    Band = fb.Band.ToResponse(),
                    Date = fb.Date,
                    Time = fb.Time,
                    Day = fb.Day
                }).ToList()
            },
            Party = e.Party is null ? null : new PartyResponse
            {
                EndDate = e.Party.EndDate,
                EndTime = e.Party.EndTime,
                EndDay = e.Party.EndDay,
                Organizer = e.Party.Organizer,
                PartyName = e.Party.PartyName.Name,
                Bands = e.Party.Bands.Select(pb => pb.Band.ToResponse()).ToList()
            }
        };
    }

    private static LocationResponse ToResponse(this ValidLocation location)
    {
        return new LocationResponse
        {
            PostalCode = location.PostalCode,
            Street = location.Street,
            HouseNumber = location.HouseNumber,
            City = location.City.Name,
            District = location.District.Name,
            Region = location.Region.Name,
            State = location.State.Name,
            EventLocationName = location.EventLocation.Name
        };
    }

    private static BandResponse ToResponse(this Band band)
    {
        return new BandResponse
        {
            Name = band.Name,
            Musicians = band.Musicians.Select(bm => new MusicianResponse
            {
                ArtistName = bm.Musician.ArtistName,
                FirstName = bm.Musician.FirstName,
                LastName = bm.Musician.LastName
            }).ToList(),
            Genres = band.MusicGenres.Select(bg => bg.MusicGenre.Description).ToList(),
            Songs = band.Songs.Select(bs => bs.Song.Name).ToList()
        };
    }
}
