﻿using AutoMapper;
using DataAccess.DbContexts;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using ObjectModel.Dtos;
using StoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public FavouriteRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddFavourite(FavouriteDTO favourite)
        {
            try
            {
                _context.Favourites.Add(_mapper.Map<Favourite>(favourite));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<FavouriteDTO>> GetFavourites(int userId)
        {
            var result = await _context.Favourites.Include(x => x.User).Include(x => x.Story)
                .Where(x => x.UserId == userId).ToListAsync();
            return _mapper.Map<List<FavouriteDTO>>(result);
        }

        public async Task<bool> RemoveFavourite(FavouriteDTO favourite)
        {
            try
            {
                var f = await _context.Favourites.Where(x => x.StoryId == favourite.StoryId && x.UserId == favourite.UserId).FirstOrDefaultAsync();
                if (f != null)
                {
                    _context.Favourites.Remove(f);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
