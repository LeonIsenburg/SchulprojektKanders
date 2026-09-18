using Backend.Models.Event;
using Backend.Models.Location;
using Backend.Models.Member;
using Backend.Models.Music;
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
    public DbSet<Concert> Concert => Set<Concert>();
    public DbSet<Festival> Festival => Set<Festival>();
    public DbSet<Party> Party => Set<Party>();
}
