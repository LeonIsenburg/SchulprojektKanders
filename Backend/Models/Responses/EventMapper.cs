using Backend.Models.Location;
using Backend.Models.Music;

namespace Backend.Models.Responses;

public static class EventMapper
{
    public static EventResponse ToResponse(this Backend.Models.Event.Event e)
    {
        return new EventResponse
        {
            StartDate = e.StartDate,
            StartTime = e.StartTime,
            EntryTime = e.EntryTime,
            Price = e.Price,
            PriceType = e.PriceType,
            EventType = e.EventType,
            Location = e.ValidLocation.ToResponse(),
            Concert = e.Concert is null ? null : new ConcertResponse
            {
                Organizer = e.Concert.Organizer,
                Bands = e.Concert.Bands.OrderBy(cb => cb.Position).Select(cb => cb.Band.ToResponse()).ToList()
            },
            Party = e.Party is null ? null : new PartyResponse
            {
                EndDate = e.Party.EndDate,
                EndTime = e.Party.EndTime,
                Organizer = e.Party.Organizer,
                PartyName = e.Party.PartyName.Name,
                Bands = e.Party.Bands.OrderBy(pb => pb.Position).Select(pb => pb.Band.ToResponse()).ToList()
            },
            Festival = e.Festival is null ? null : new FestivalResponse
            {
                EndDate = e.Festival.EndDate,
                EndTime = e.Festival.EndTime,
                Organizer = e.Festival.Organizer,
                FestivalName = e.Festival.FestivalName.Name,
                Bands = e.Festival.Bands.OrderBy(fb => fb.Position).Select(fb => new BandPerformanceResponse
                {
                    Band = fb.Band.ToResponse(),
                    Date = fb.Date,
                    Time = fb.Time,
                    Day = fb.Day
                }).ToList()
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
        var genres = band.MusicGenres.OrderBy(bg => bg.Position).Select(bg => bg.MusicGenre.Description).ToList();
        var songs = band.Songs.OrderBy(bs => bs.Position).Select(bs => bs.Song.Name).ToList();

        return new BandResponse
        {
            Name = band.Name,
            Musicians = band.Musicians.OrderBy(bm => bm.Position).Select(bm => bm.Musician.ToResponse()).ToList(),
            // leere Listen wie im Request als null ausgeben
            Genres = genres.Count > 0 ? genres : null,
            Songs = songs.Count > 0 ? songs : null
        };
    }

    private static MusicianResponse ToResponse(this Musician musician)
    {
        // Beim Anlegen wird ein fehlender Vor-/Nachname mit dem Künstlernamen gefüllt
        // (siehe RequestRepository) – das hier wieder als null ausgeben
        return new MusicianResponse
        {
            ArtistName = musician.ArtistName,
            FirstName = musician.FirstName == musician.ArtistName ? null : musician.FirstName,
            LastName = musician.LastName == musician.ArtistName ? null : musician.LastName
        };
    }
}
