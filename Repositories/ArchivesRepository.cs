using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using sqwuakServer.Models;

namespace sqwuakServer.Repositories
{
    public class ArchivesRepository
    {
        private readonly IDbConnection _db;
        public ArchivesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Archive GetArchivesById(int id)
        {
            string sql = @"
            SELECT
            archs.*,
            prof.*
            FROM archives archs
            JOIN profiles prof ON archs.creatorId = prof.id
            WHERE archs.id = @Id;";
            return _db.Query<Archive, Profile, Archive>(sql, (archive, profile) =>
            {
                archive.Creator = profile;
                return archive;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }
        internal int Create(Archive newArchive)
        {
            string sql = @"
            INSERT INTO Archives
            (Name, isPrivate, creatorId)
            VALUES
            (@Name, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newArchive);
        }

        internal Archive Edit(Archive updated)
        {
            throw new NotImplementedException();
        }
        internal void Remove(int id)
        {
            throw new NotImplementedException();
        }

        //From ACCOUNT Controller
        internal IEnumerable<Archive> GetArchivesByOwnerId(string id)
        {
            string sql = @"
            SELECT 
            arch.*,
            pro.* 
            FROM archives arch
            JOIN profiles pro ON arch.creatorId = pro.id
            WHERE arch.creatorId = @id;";
            return _db.Query<Archive, Profile, Archive>(sql, (archive, profile) => { archive.Creator = profile; return archive; }, new { id }, splitOn: "id");
        }


        internal IEnumerable<Archive> GetArchivesByProfileId(string id)
        {
            string sql = @"
             SELECT
             archs.*,
             pro.*
             FROM archives archs
             JOIN profiles pro ON archs.creatorId = pro.id
             ";
            return _db.Query<Archive, Profile, Archive>(sql, (archives, profile) =>
            {
                archives.Creator = profile;
                return archives;
            }
                , new { id }, splitOn: "id");
        }
    }
}