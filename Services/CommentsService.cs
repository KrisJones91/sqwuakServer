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

        internal Comment Create(Comment newComment)
        {
            throw new NotImplementedException();
        }

        internal object Edit(Comment updated, string id)
        {
            throw new NotImplementedException();
        }

        internal object Delete(int id1, string id2)
        {
            throw new NotImplementedException();
        }
    }
}