using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using sqwuakServer.Models;

namespace sqwuakServer.Repositories
{
    public class CommentsRepository
    {
        private readonly IDbConnection _db;
        public CommentsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal IEnumerable<Comment> GetAllCommentsByPostId(int id)
        {
            string sql = @"
            SELECT
            com.*,
            acc.*
            FROM comments com
            JOIN accounts acc ON com.creatorId = acc.id
            WHERE com.postId = @id;
            ";
            return _db.Query<Comment, Profile, Comment>(sql, (comment, profile) =>
            {
                comment.Creator = profile;
                return comment;
            }, new { id }, splitOn: "id");
        }
        internal int Create(Comment newComment)
        {
            string sql = @"
            INSERT INTO comments
            (body, likes, postId, creatorId)
            VALUES
            (@Body, @likes, @PostId, @CreatorId);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newComment);
        }

        internal Comment GetById(int id)
        {
            string sql = @"
            SELECT 
            com.*,
            acc.*
            FROM comments com
            JOIN accounts acc ON com.creatorId = acc.id
            WHERE com.id = @id;";
            return _db.Query<Comment, Profile, Comment>(sql, (comment, profile) =>
            {
                comment.Creator = profile;
                return comment;

            }, new { id }, splitOn: "id").FirstOrDefault();
        }


        internal object Edit(Comment updated)
        {
            string sql = @"
            UPDATE comments
            SET
            body = @Body
            WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }


        internal void Delete(int id)
        {
            string sql = "DELETE FROM comments WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });

        }
    }
}