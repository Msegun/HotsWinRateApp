using HotsWinRate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotsWinRate.DataBase.Abstract
{
    public interface IHeroRepository
    {
        Task<IEnumerable<Hero>> GetHeroesAsync();
        Task<Hero> GetHeroByIdAsync(int id);
        Task<bool> AddHeroAsync(Hero hero);
        Task<bool> UpdateHeroAsync(Hero hero);
        Task<bool> DeleteHeroAsync(int id);
        //Task<IEnumerable<Hero>> QueryPlayersAsync(Func<Hero, bool> predicate);
    }
}
