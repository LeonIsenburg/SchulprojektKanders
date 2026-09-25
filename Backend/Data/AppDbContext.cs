using Backend.Models.Event;
using Backend.Models.Location;
using Backend.Models.Member;
using Backend.Models.Music;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<City> Cities => Set<City>();
    public DbSet<District> District => Set<District>();
    public DbSet<EventLocation> EventLocation => Set<EventLocation>();
    public DbSet<FestivalName> FestivalName => Set<FestivalName>();
    public DbSet<Instrument> Instrument => Set<Instrument>();
    public DbSet<Member> Member => Set<Member>();
    public DbSet<MusicGenre> MusicGenre => Set<MusicGenre>();
    public DbSet<Musician> Musician => Set<Musician>();
    public DbSet<PartyName> Partyname => Set<PartyName>();
    public DbSet<Region> Region => Set<Region>();
    public DbSet<Song> Song => Set<Song>();
    public DbSet<State> State => Set<State>();
    public DbSet<ValidLocation> ValidLocation => Set<ValidLocation>();
    public DbSet<Event> Event => Set<Event>();
    public DbSet<Concert> Concert => Set<Concert>();
    public DbSet<Festival> Festival => Set<Festival>();
    public DbSet<Party> Party => Set<Party>();
    public DbSet<Band> Band => Set<Band>();
    public DbSet<MusicianInstrument> MusicianInstrument => Set<MusicianInstrument>();
    public DbSet<BandMusician> BandMusician => Set<BandMusician>();
    public DbSet<BandSong> BandSong => Set<BandSong>();
    public DbSet<BandMusicGenre> BandMusicGenre => Set<BandMusicGenre>();
    public DbSet<ConcertBand> ConcertBand => Set<ConcertBand>();
    public DbSet<FestivalBand> FestivalBand => Set<FestivalBand>();
    public DbSet<PartyBand> PartyBand => Set<PartyBand>();

    // Test-Mitglied, das als Ersteller aller Events eingetragen wird, solange es keine Anmeldung gibt
    public static readonly Guid DevMemberId = new("00000000-0000-0000-0000-000000000001");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Member>().HasData(new Member
        {
            Guid = DevMemberId,
            MemberID = 1,
            Username = "dev",
            Password = "dev",
            Nickname = "Dev",
            FirstName = "Test",
            LastName = "Mitglied",
            EMail = "dev@example.com",
            Birthday = new DateOnly(2000, 1, 1),
            Gender = "divers",
            PhoneNumber = "0000000000",
            Street = "Teststraße",
            HouseNumber = "1",
            PostalCode = "00000",
            Location = "Teststadt",
            Role = Role.Admin
        });
    }
}
