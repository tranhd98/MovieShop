using System.Collections.Immutable;
using System.Net.NetworkInformation;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class MovieShopDbContext: DbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
    {
        
    }
    // think as database
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movie { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<MovieCast> MovieCast { get; set; }
    public DbSet<Cast> Casts { get; set; }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<Trailer>(ConfigureTrailer);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
        modelBuilder.Entity<Cast>(ConfigureCast);
        
        
        modelBuilder.Entity<User>(ConfigureUser);
        modelBuilder.Entity<Review>(ConfigureReview);
        modelBuilder.Entity<Favorite>(ConfigureFavorite);
        modelBuilder.Entity<Purchase>(ConfigurePurchase);
        modelBuilder.Entity<Role>(ConfigureRole);
        modelBuilder.Entity<UserRole>(ConfigureUserRole);
    }

    private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");
        builder.HasKey(ur => new {ur.UserId, ur.RoleId});
        builder.HasOne(ur => ur.User).WithMany(u => u.Roles).HasForeignKey(ur => ur.UserId);
        builder.HasOne(ur => ur.Role).WithMany(ro => ro.Users).HasForeignKey(ur => ur.RoleId);
    }

    private void ConfigureRole(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.HasKey(ro => ro.Id);
        builder.Property(ro => ro.Name).HasMaxLength(20);
    }

    private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchase");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)").HasDefaultValue(9.9m);
        builder.Property(p => p.PurchaseDateTime).HasDefaultValueSql("getdate()");
    }

    private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
    {
        builder.ToTable("Favorite");
        builder.HasKey(f => f.Id);
    }
    

    private void ConfigureReview(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");
        builder.HasKey(r => new {r.MovieId, r.UserId});
        builder.HasOne(r => r.Movie).WithMany(m => m.Reviews).HasForeignKey(r => r.MovieId);
        builder.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);
        builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)").HasDefaultValue(9.9m);
        builder.Property(r => r.ReviewText).HasMaxLength(2084);
    }

    private void ConfigureUser(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName).HasMaxLength(128);
        builder.Property(u => u.LastName).HasMaxLength(128);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.HashedPassword).HasMaxLength(1024);
        builder.Property(u => u.Salt).HasMaxLength(1024);
        builder.Property(u => u.PhoneNumber).HasMaxLength(16);
    }

    private void ConfigureCast(EntityTypeBuilder<Cast> builder)
    {
        builder.ToTable("Cast");
        builder.HasKey(c => c.Id);
        builder.Property("Name").HasMaxLength(128);
        builder.Property("ProfilePath").HasMaxLength(2084);
    }

    private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
    {
        builder.ToTable("MovieCast");
        builder.HasKey(mc => new {mc.MovieId, mc.CastId, mc.Character});
        builder.HasOne(m => m.Movie).WithMany(m => m.MovieCasts).HasForeignKey(m => m.MovieId);
        builder.HasOne(m => m.Cast).WithMany(m => m.MovieCasts).HasForeignKey(m => m.CastId);

    }

    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenre");
        builder.HasKey(mg => new {mg.MovieId, mg.GenreId});
        builder.HasOne(m => m.Movie).WithMany(m => m.Genres).HasForeignKey(m => m.MovieId);
        builder.HasOne(m => m.Genre).WithMany(m => m.Movies).HasForeignKey(m => m.GenreId);
    }

    private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
    {
        builder.ToTable("Trailer");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TrailerUrl).HasMaxLength(2048);
        builder.Property(t => t.Name).HasMaxLength(256);
    }
    
    private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
    {
        // Fluent API WAY will take more preference than Data Annotations.
        // if anything changes in Data Annotations and then anything changes in Fluent API. It will take the change according to Fluent API WAY
        // specify the rules for Movie Entity
        builder.ToTable("Movie");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Title).HasMaxLength(256);
        builder.Property(m => m.Overview).HasMaxLength(4096);
        builder.Property(m => m.Tagline).HasMaxLength(512);
        builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
        builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
        builder.Property(m => m.PosterUrl).HasMaxLength(2084);
        builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
        builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
        
        builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
        builder.Property(m => m.Revenue).HasColumnType("decimal(18, 2)").HasDefaultValue(9.9m);
        builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
        
        
        builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
        builder.Ignore(m => m.Rating);
        

    }
}