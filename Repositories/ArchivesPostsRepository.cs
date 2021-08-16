using System;
using System.Data;
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
            throw new NotImplementedException();
        }
    }
}