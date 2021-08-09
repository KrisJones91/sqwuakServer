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


        internal object GetById(int id1, string id2)
        {
            throw new NotImplementedException();
        }


        internal IEnumerable<Archive> GetArchivesByAccountId(string id)
        {
            return _arepo.GetArchivesByOwnerId(id);
        }


    }
}