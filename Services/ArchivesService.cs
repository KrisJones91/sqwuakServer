using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        internal IEnumerable<Archive> GetArchivesByAccountId(string id)
        {
            return _arepo.GetArchivesByOwnerId(id);
        }
    }
}