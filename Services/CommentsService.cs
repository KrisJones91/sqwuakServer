using System;
using System.Collections.Generic;
using sqwuakServer.Models;
using sqwuakServer.Repositories;

namespace sqwuakServer.Services
{
    public class CommentsService
    {
        private readonly CommentsRepository _crepo;
        public CommentsService(CommentsRepository crepo)
        {
            _crepo = crepo;
        }
        public IEnumerable<Comment> GetComments()
        {
            IEnumerable<Comment> comments = _crepo.GetAllComments();
            return comments;
        }
        internal Comment GetCommentsById(int id)
        {
            Comment comment = _crepo.GetById(id);
            if (comment == null)
            {
                throw new Exception("Invalid Id");
            }
            return comment;
        }

        internal Comment Create(Comment newComment)
        {
            newComment.Id = _crepo.Create(newComment);
            return newComment;
        }

        internal object Edit(Comment updated, string id)
        {
            Comment original = GetCommentsById(updated.Id);
            if (original.CreatorId != id) { throw new Exception("Access Denied: You do not have access to modify another users comments."); }
            updated.Body = updated.Body == null ? original.Body : updated.Body;
            return _crepo.Edit(updated);
        }

        internal string Delete(int id, string userId, int PostId)
        {
            Comment comment = _crepo.GetById(id);
            if (comment == null) { throw new Exception("Invalid ID"); }
            if (comment.CreatorId != userId) { throw new Exception("Access Denied: You may not delete another users comment."); }
            _crepo.Delete(id);
            return "Comment Successfully Deleted";
        }

    }
}