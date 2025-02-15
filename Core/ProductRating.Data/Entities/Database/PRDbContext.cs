using Microsoft.EntityFrameworkCore;

namespace ProductRating.Data.Entities.Database
{
    public class PRDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserHistory> UserHistory { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewHistory> ReviewHistory { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentHistory> CommentHistory { get; set; }
        public DbSet<RecognitionHistory> RecognitionHistory { get; set; }


        public PRDbContext(DbContextOptions<PRDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(u => u.Phone)
                    .IsRequired();
                entity.Property(u => u.Email)
                    .HasMaxLength(60);
                entity.Property(u => u.Password)
                    .IsRequired();

                entity.HasIndex(u => u.Phone)
                    .IsUnique();
                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });

            modelBuilder.Entity<UserHistory>(entity =>
            {
                entity.ToTable("UserHistory");
                entity.HasKey(uh => uh.Id);

                entity.Property(uh => uh.User)
                    .IsRequired();
                entity.Property(uh => uh.Operation)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(uh => uh.Date)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(uh => uh.User);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Type)
                    .IsRequired();
                entity.Property(p => p.Brand)
                    .IsRequired();

                entity.HasOne<ProductType>()
                    .WithMany()
                    .HasForeignKey(p => p.Type);
                entity.HasOne<ProductBrand>()
                    .WithMany()
                    .HasForeignKey(p => p.Brand);

                entity.HasIndex(p => p.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductTypes");
                entity.HasKey(pt => pt.Id);

                entity.Property(pt => pt.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(pt => pt.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<ProductBrand>(entity =>
            {
                entity.ToTable("ProductBrands");
                entity.HasKey(pb => pb.Id);

                entity.Property(pb => pb.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(pb => pb.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<ProductRating>(entity =>
            {
                entity.ToTable("ProductRatings");
                entity.HasKey(pr => pr.Id);

                entity.Property(pr => pr.Product)
                    .IsRequired();
                entity.Property(pr => pr.OverallRating)
                    .IsRequired();
                entity.Property(pr => pr.YearlyRating)
                    .IsRequired();
                entity.Property(pr => pr.MonthlyRating)
                    .IsRequired();

                entity.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(pr => pr.Product);

                entity.HasIndex(pr => pr.Product)
                    .IsUnique();
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Product)
                    .IsRequired();
                entity.Property(r => r.User)
                    .IsRequired();
                entity.Property(r => r.Rating)
                    .IsRequired();
                entity.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(r => r.Product);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(r => r.User);
            });

            modelBuilder.Entity<ReviewHistory>(entity =>
            {
                entity.ToTable("ReviewHistory");
                entity.HasKey(rh => rh.Id);

                entity.Property(rh => rh.Review)
                    .IsRequired();
                entity.Property(rh => rh.Operation)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(rh => rh.Date)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<Review>()
                    .WithMany()
                    .HasForeignKey(rh => rh.Review);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Review)
                    .IsRequired();
                entity.Property(c => c.User)
                    .IsRequired();
                entity.Property(c => c.Vote)
                    .IsRequired();
                entity.Property(c => c.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne<Review>()
                    .WithMany()
                    .HasForeignKey(c => c.Review);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(c => c.User);
            });

            modelBuilder.Entity<CommentHistory>(entity =>
            {
                entity.ToTable("CommentHistory");
                entity.HasKey(ch => ch.Id);

                entity.Property(ch => ch.Comment)
                    .IsRequired();
                entity.Property(ch => ch.Operation)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(ch => ch.Date)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<Comment>()
                    .WithMany()
                    .HasForeignKey(ch => ch.Comment);
            });

            modelBuilder.Entity<RecognitionHistory>(entity =>
            {
                entity.ToTable("RecognitionHistory");
                entity.HasKey(rh => rh.Id);

                entity.Property(rh => rh.Product)
                    .IsRequired();
                entity.Property(rh => rh.User)
                    .IsRequired();
                entity.Property(rh => rh.Confidence)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(rh => rh.Date)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(rh => rh.Product);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(rh => rh.User);
            });
        }
    }
}