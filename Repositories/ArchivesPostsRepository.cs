using System;
using System.Data;
using Dapper;
using sqwuakServer.Models;

namespace sqwuakServer.Repositories
{
    public class ArchivesPostsRepository
    {
        private readonly IDbConnection _db;
        public ArchivesPostsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal int Create(ArchivePost newPA)
        {
            string sql = @"
            INSERT INTO archivePosts
            (creatorId, archiveId, postId)
            VALUES
            (@CreatorId, @ArchiveId, @PostId);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newPA);
        }

        internal ArchivePost GetById(int id)
        {
            string sql = "SELECT * FROM archiveposts WHERE id = @id;";
            return _db.QueryFirstOrDefault<ArchivePost>(sql, new { id });
        }

        internal void Delete(int id)
        {

        }
    }
}