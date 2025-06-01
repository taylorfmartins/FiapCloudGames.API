using FiapCloudGames.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica as configurações de entidade
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Aplica o seed inicial
            SeedInitialData(modelBuilder);
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            // Cria o usuário admin padrão
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@fiap.com.br",
                    PasswordHash = "AQAAAAEAACcQAAAAELbXp1QrHhX5Y0QxX5Y0QxX5Y0QxX5Y0QxX5Y0QxX5Y0QxX5Y0QxX5Y0QxX5Y0Q==", // Senha: Admin@123
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
