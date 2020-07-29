using Computer_EPAM_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Computer_EPAM_Task.DataRepositories.DBRepositories.EF_Core.Context
{
    internal class VideosContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }

        public VideosContext() => Database.EnsureCreated();

        public VideosContext(DbContextOptions<VideosContext> options) : base(options) => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Videos;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().HasData(
                data: new Video[]
                {
                    new Video {Id = 1, Name = "Устройство компьютера", URL = "https://www.youtube.com/embed/OAx_6-wdslM" },
                    new Video {Id = 2, Name = "Работа центрального процессора", URL = "https://www.youtube.com/embed/blvQBwxSWCI" },
                    new Video {Id = 3, Name = "Работа видеокарты", URL = "https://www.youtube.com/embed/Kgcfj_KV-mo" },
                    new Video {Id = 4, Name = "Работа ОЗУ", URL = "https://www.youtube.com/embed/p3q5zWCw8J4" },
                    new Video {Id = 5, Name = "Работа операционной системы", URL = "https://www.youtube.com/embed/26QPDBe-NB8" }
                }
            );
        }
    }
}