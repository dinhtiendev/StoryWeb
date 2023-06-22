using DataAccess.DbContexts;
using DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ApplicationDbContext _context;

        public ChapterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
