using Computer_EPAM_Task.DataRepositories.DBRepositories.EF_Core.Context;
using Computer_EPAM_Task.Interfaces;
using Computer_EPAM_Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Computer_EPAM_Task.DataRepositories.DBRepositories.EF_Core.Repositories
{
    internal class VideoRepository : IRepository<Video>
    {
        private readonly DbContextOptionsBuilder<VideosContext> _optionsBuilder = new DbContextOptionsBuilder<VideosContext>();
        private readonly DbContextOptions<VideosContext> _options;
        private VideosContext _db;

        public VideoRepository()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            _options = _optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection")).Options;
        }

        public VideoRepository(DbContextOptions<VideosContext> options) => _options = options;

        public async Task<bool> TryCreateAsync(Video item)
        {
            using (_db = new VideosContext(_options))
            {
                if(!_db.Videos.Any(v => v.Name == item.Name))
                {
                    _db.Add(item);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> TryDeleteAsync(Video item)
        {
            using (_db = new VideosContext(_options))
            {
                if(_db.Videos.Any(v => v.Name == v.Name))
                {
                    _db.Remove(item);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> TryUpdateAsync(Video item)
        {
            using (_db = new VideosContext(_options))
            {
                if (_db.Videos.Any(v => v.Name == item.Name))
                {
                    Video video = _db.Videos.FirstOrDefault(v => v.Id == item.Id);
                    video.Name = item.Name;
                    video.URL = item.URL;
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Video> TryGetAsync(int id)
        {
            using (_db = new VideosContext(_options))
            {
                if (_db.Videos.AsNoTracking().Any(v => v.Id == id))
                {
                    return await _db.Videos.AsNoTracking().FirstAsync(v => v.Id == id);
                }
                else
                {
                    throw new Exception("По указанному индексу не нашлось видео");
                }
            }
        }

        public async Task<IEnumerable<Video>> TryGetAllAsync()
        {
            using (_db = new VideosContext(_options))
            {
                if(_db.Videos.Count() > 0)
                {
                    return await _db.Videos.AsNoTracking().ToListAsync();
                }
                throw new Exception("Не удалось найти видео");
            }
        }
    }
}