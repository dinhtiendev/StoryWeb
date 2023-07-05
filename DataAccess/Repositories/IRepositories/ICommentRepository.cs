using ObjectModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        public Task<bool> AddComment(ReplyDTO comment);

        public Task<bool> AddReply(int commentId, ReplyDTO comment);

        public Task<bool> EditComment(ReplyDTO comment);

        public Task<bool> DeleteComment(int commentId);

        public Task<List<CommentDTO>> GetComments(int storyId);
    }
}
