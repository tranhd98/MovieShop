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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<Trailer>(ConfigureTrailer);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
        modelBuilder.Entity<Cast>(ConfigureCast);
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