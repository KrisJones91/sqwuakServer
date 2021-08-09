using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using sqwuakServer.Models;

namespace sqwuakServer.Repositories
{
    public class PostsRepository
    {
        private readonly IDbConnection _db;
        public PostsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal IEnumerable<Post> GetAll()
        {
            string sql = @"
            SELECT
            post.*,
            prof.*
            FROM posts post
            JOIN profiles prof ON post.CreatorId = prof.id;
            ";
            return _db.Query<Post, Account, Post>(sql, (post, profile) =>
            {
                post.Creator = profile;
                return post;
            }, splitOn: "id");
        }

        internal Post GetById(int id)
        {
            string sql = @"
            SELECT 
            post.*,
            prof.*
            FROM posts post
            JOIN profiles prof ON post.creatorId = prof.id
            WHERE post.id = @id;";
            return _db.Query<Post, Account, Post>(sql, (post, profile) =>
            {
                post.Creator = profile;
                return post;

            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal int Create(Post newPost)
        {
            throw new NotImplementedException();
        }

        internal Post Edit(Post updated)
        {
            throw new NotImplementedException();
        }

        internal void Remove(int id)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<Post> GetByOwnerId(string id)
        {
            throw new NotImplementedException();
        }
    }
}