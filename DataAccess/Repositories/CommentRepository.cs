using AutoMapper;
using DataAccess.DbContexts;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using ObjectModel.Dtos;

namespace DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public CommentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddComment(CommentDTO comment)
        {
            try
            {

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddReply(CommentDTO comment)
        {
            try
            {

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            try
            {
                var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);
                if (comment != null)
                {
                    _context.Remove(comment);
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

        public async Task<bool> EditComment(CommentDTO comment)
        {
            try
            {

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<CommentDTO>> GetComments(int storyId)
        {
            try
            {
                var comments = await _context.Comments.Include(x => x.User).Where(x => x.StoryId == storyId && x.ParentCommentId == null).ToListAsync();
                return _mapper.Map<List<CommentDTO>>(comments);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<CommentDTO>> GetReplies(int storyId, int commentId)
        {
            try
            {
                var comments = await _context.Comments.Include(x => x.User).Where(x => x.StoryId == storyId && x.CommentId == commentId).ToListAsync();
                return _mapper.Map<List<CommentDTO>>(comments);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
