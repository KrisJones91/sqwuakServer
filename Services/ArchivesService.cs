using System;
using System.Collections.Generic;
using sqwuakServer.Models;
using sqwuakServer.Repositories;

namespace sqwuakServer.Services
{
    public class ArchivesService
    {
        private readonly ArchivesRepository _arepo;

        public ArchivesService(ArchivesRepository arepo)
        {
            _arepo = arepo;
        }


        internal object GetById(int id, string userId)
        {
            Archive archive = _arepo.GetArchivesById(id);
            if (archive.isPrivate == true && archive.CreatorId != userId || archive == null)
            {
                throw new Exception("Invalid Id -or- ARCHIVE is Private.");
            }
            return archive;
        }
        internal Archive Create(Archive newArchive)
        {
            throw new NotImplementedException();
        }


        internal IEnumerable<Archive> GetArchivesByAccountId(string id)
        {
            return _arepo.GetArchivesByOwnerId(id);
        }

    }
}