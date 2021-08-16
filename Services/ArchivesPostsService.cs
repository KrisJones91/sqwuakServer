using System;
using sqwuakServer.Models;
using sqwuakServer.Repositories;

namespace sqwuakServer.Services
{
    public class ArchivesPostsService
    {
        private readonly ArchivesPostsRepository _APrepo;
        private readonly ArchivesRepository _arepo;

        public ArchivesPostsService(ArchivesPostsRepository APrepo, ArchivesRepository arepo)
        {
            _APrepo = APrepo;
            _arepo = arepo;
        }
        internal ArchivePost Create(ArchivePost newPA)
        {
            int id = _APrepo.Create(newPA);
            newPA.Id = id;
            return newPA;
        }
    }
}