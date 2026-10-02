using Backend.Models.Event;
using Backend.Models.Location;
using Backend.Models.Music;
using Backend.Models.Relations;

namespace Backend.Models.Responses;

public static class EventMapper
{
    public static EventResponse ToResponse(this Backend.Models.Event.Event e)
    {
        return new EventResponse
        {
            Name = e.Name,
            Type = e.Type,
            Status = e.Status,
            Dates = e.Dates.OrderBy(d => d.Position).Select(d => d.ToResponse()).ToList(),
            Artists = e.Artists.OrderBy(a => a.Position).Select(a => a.ToResponse()).ToList(),
            Organizer = new OrganizerResponse
            {
                Name = e.Organizer?.Name,
                Website = e.Organizer?.Website
            },
            AgeRestriction = e.AgeRestriction,
            Description = e.Description,
            Website = e.Website,
            Socials = new SocialsResponse
            {
                Instagram = e.Instagram,
                Facebook = e.Facebook,
                Tiktok = e.TikTok
            },
            ExtraInfo = e.ExtraInfo
        };
    }

    private static EventDateResponse ToResponse(this EventDate date)
    {
        return new EventDateResponse
        {
            Start = date.Start,
            End = date.End,
            DoorsOpen = date.DoorsOpen,
            Location = date.ValidLocation.ToResponse(),
            Tickets = new TicketsResponse
            {
                Price = date.Price,
                Currency = date.Currency,
                Presale = date.Presale,
                Url = date.TicketUrl
            }
        };
    }

    private static LocationResponse ToResponse(this ValidLocation? location)
    {
        return new LocationResponse
        {
            Venue = location?.EventLocation?.Name,
            Street = location?.Street,
            HouseNumber = location?.HouseNumber,
            PostalCode = location?.PostalCode,
            City = location?.City?.Name,
            District = location?.District?.Name,
            Region = location?.Region?.Name,
            State = location?.State?.Name,
            Country = location?.Country
        };
    }

    private static ArtistResponse ToResponse(this EventArtist artist)
    {
        var band = artist.Band;

        return new ArtistResponse
        {
            Name = band.Name,
            Genres = band.MusicGenres.OrderBy(bg => bg.Position).Select(bg => bg.MusicGenre.Description).ToList(),
            Members = band.Musicians.OrderBy(bm => bm.Position).Select(bm => new MemberResponse
            {
                ArtistName = bm.Musician.ArtistName,
                FirstName = bm.Musician.FirstName,
                LastName = bm.Musician.LastName
            }).ToList(),
            Songs = band.Songs.OrderBy(bs => bs.Position).Select(bs => bs.Song.Name).ToList(),
            Performance = artist.Performance
        };
    }
}
