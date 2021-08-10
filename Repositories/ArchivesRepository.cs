using System;
using System.Collections.Generic;
using System.Data;
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
            throw new NotImplementedException();
        }
        internal int Create(Archive newArchive)
        {
            throw new NotImplementedException();
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


        internal object GetArchivesByProfileId(string id)
        {
            throw new NotImplementedException();
        }
    }
}