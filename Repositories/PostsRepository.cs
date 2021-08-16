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
            return _db.Query<Post, Profile, Post>(sql, (post, profile) =>
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
            return _db.Query<Post, Profile, Post>(sql, (post, profile) =>
            {
                post.Creator = profile;
                return post;

            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal int Create(Post newPost)
        {
            string sql = @"
            INSERT INTO Posts
            (title, description, img, views, shares, saves, creatorId)
            VALUES
            (@Title, @Description, @Img, @Views, @Shares, @Saves, @CreatorId);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newPost);
        }

        internal Post Edit(Post updated)
        {
            string sql = @"
            UPDATE Posts
            SET
            title = @Title,
            description = @Description,
            img = @IMG
            WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
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