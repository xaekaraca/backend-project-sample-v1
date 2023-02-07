using Microsoft.EntityFrameworkCore;
using NLayer.Entity;

namespace NLayer.Repository.Context
{
    public partial class BaseContext : DbContext
    {
        public BaseContext() { }
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
                entity =>
                {
                    entity.ToTable("Users");
                    entity.HasKey(x => x.Id);
                    entity.Property(x => x.Id).UseIdentityColumn();
                    entity.HasQueryFilter(x => x.IsDeleted == false);
                    entity.Property(x => x.CreatedAt).HasColumnType("datetime");
                    entity.Property(x => x.UpdatedAt).HasColumnType("datetime");
                });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
