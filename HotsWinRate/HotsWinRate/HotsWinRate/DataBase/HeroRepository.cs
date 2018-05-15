using HotsWinRate.DataBase.Abstract;
using HotsWinRate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotsWinRate.DataBase
{
    public class HeroRepository : IHeroRepository
    {
        private readonly DataBaseContext _databaseContext;

        public HeroRepository(DataBaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> AddHeroAsync(Hero hero)
        {
            try
            {
                var tracking = await _databaseContext.Heroes.AddAsync(hero);
                var isAdded = tracking.State == EntityState.Added;

                await _databaseContext.SaveChangesAsync();
                return isAdded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteHeroAsync(int id)
        {
            try
            {
                var hero = await _databaseContext.Heroes.FindAsync(id);

                var tracking = _databaseContext.Heroes.Remove(hero);
                var isDeleted = tracking.State == EntityState.Deleted;

                await _databaseContext.SaveChangesAsync();
                return isDeleted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Hero> GetHeroByIdAsync(int id)
        {
            try
            {
                var hero = await _databaseContext.Heroes.SingleOrDefaultAsync(x => x.Id == id);
                return hero;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Hero>> GetHeroesAsync()
        {
            try
            {
                var heroes = _databaseContext.Heroes.ToListAsync();
                return await heroes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateHeroAsync(Hero hero)
        {
            try
            {
                var tracking = _databaseContext.Heroes.Update(hero);
                var isUpdated = tracking.State == EntityState.Modified;

                await _databaseContext.SaveChangesAsync();
                return isUpdated;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
