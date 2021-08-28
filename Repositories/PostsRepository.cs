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
            acc.*
            FROM posts post
            JOIN accounts acc ON post.CreatorId = acc.id;
            ";
            return _db.Query<Post, Profile, Post>(sql, (post, profile) =>
            {
                post.Creator = profile;
                return post;
            }, splitOn: "id");
        }

        //             UPDATE posts post
        // SET post.views = post.views + 1
        // SET post.views = post.views - 1
        internal Post GetById(int id)
        {
            string sql = @"
            UPDATE posts post
            SET post.views = post.views + 1
            WHERE post.id = @id;
            SELECT 
            post.*,
            acc.*
            FROM posts post
            JOIN accounts acc ON post.creatorId = acc.id
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
            INSERT INTO posts
            (title, description, img, views, shares, saves, creatorId)
            VALUES
            (@Title, @Description, @Img, @Views, @Shares, @Saves, @CreatorId);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newPost);
        }

        internal Post Edit(Post updated)
        {
            string sql = @"
            UPDATE posts
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
            string sql = "DELETE FROM posts WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        // Accounts Controller
        internal IEnumerable<Post> GetByOwnerId(string id)
        {
            string sql = @"
            SELECT
            post.*,
            acc.*
            FROM posts post
            JOIN accounts acc ON post.creatorId = acc.id
            WHERE post.creatorId = @id;";
            return _db.Query<Post, Account, Post>(sql, (post, account) =>
            {
                post.Creator = account;
                return post;
            }
            , new { id }, splitOn: "id");
        }

        //Profiles Controller
        internal IEnumerable<Post> GetPostsByProfileId(string id)
        {
            string sql = @"
             SELECT
             post.*,
             acc.*
             FROM posts post
             JOIN accounts acc ON post.creatorId = acc.id
             ";
            return _db.Query<Post, Account, Post>(sql, (posts, account) =>
            {
                posts.Creator = account;
                return posts;
            }
              , new { id }, splitOn: "id");
        }

        internal IEnumerable<ArchPostModel> GetPostsByArchivesId(int id)
        {
            string sql = @"
            SELECT
            post.*,
            ap.id as ArchPostId,
            acc.*
            FROM archposts ap
            JOIN posts post ON post.id = ap.postId
            JOIN accounts acc ON post.creatorId = acc.id
            WHERE archiveId = @id
            ";
            return _db.Query<ArchPostModel, Account, ArchPostModel>(sql, (post, account) =>
            {
                post.Creator = account;
                return post;
            }, new { id }, splitOn: "id");
        }
    }
}