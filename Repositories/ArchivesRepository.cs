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
            acc.*
            FROM archives archs
            JOIN accounts acc ON archs.creatorId = acc.id
            WHERE archs.id = @id;";
            return _db.Query<Archive, Account, Archive>(sql, (archive, account) =>
            {
                archive.Creator = account;
                return archive;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }
        internal int Create(Archive newArchive)
        {
            string sql = @"
            INSERT INTO archives
            (Name, isPrivate, creatorId)
            VALUES
            (@Name, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, newArchive);
        }

        internal Archive Edit(Archive updated)
        {
            string sql = @"
            UPDATE archives
            SET
            name = @Name,
            isPrivate = @IsPrivate
            WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }
        internal void Remove(int id)
        {
            string sql = "DELETE FROM archives WHERE id = @Id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        //From ACCOUNT Controller
        internal IEnumerable<Archive> GetArchivesByOwnerId(string id)
        {
            string sql = @"
            SELECT 
            arch.*,
            acc.* 
            FROM archives arch
            JOIN account acc ON arch.creatorId = acc.id
            WHERE arch.creatorId = @id;";
            return _db.Query<Archive, Account, Archive>(sql, (archive, account) => { archive.Creator = account; return archive; }, new { id }, splitOn: "id");
        }


        internal IEnumerable<Archive> GetArchivesByProfileId(string id)
        {
            string sql = @"
             SELECT
             archs.*,
             acc.*
             FROM archives archs
             JOIN accounts acc ON archs.creatorId = acc.id
             ";
            return _db.Query<Archive, Account, Archive>(sql, (archives, account) =>
            {
                archives.Creator = account;
                return archives;
            }
                , new { id }, splitOn: "id");
        }




    }
}