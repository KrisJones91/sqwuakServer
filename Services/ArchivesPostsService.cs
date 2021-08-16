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

        internal object GetById(int id)
        {
            return _APrepo.GetById(id);
        }
        internal object Delete(int id, string userId)
        {
            ArchivePost archivePost = _APrepo.GetById(id);
            if (archivePost == null)
            {
                throw new Exception("You cannot Delete content that is not yours.");
            }
            if (archivePost.CreatorId != userId)
            {
                throw new Exception("Access Denied: You can only delete content you have created.");
            }
            _APrepo.Delete(id);
            return "Deleted ArchivePost.";
        }

    }
}