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
            newArchive.Id = _arepo.Create(newArchive);
            return newArchive;
        }

        internal Archive Edit(Archive updated, string id)
        {
            Archive original = _arepo.GetArchivesById(updated.Id);
            if (original.CreatorId != id) { throw new Exception("Access Denied: You cannot edit content that is not yours."); }
            updated.Name = updated.Name == null ? original.Name : updated.Name;
            updated.isPrivate = updated.isPrivate == false ? original.isPrivate : updated.isPrivate;
            return _arepo.Edit(updated);
        }


        internal object Delete(int id, string userId)
        {
            // might need to revise GetArchivesById to GetById
            Archive original = _arepo.GetArchivesById(id);
            if (original == null) { throw new Exception("Invalid ID"); }
            if (original.CreatorId != userId) { throw new Exception("Access Denied: This is not your content."); }
            _arepo.Remove(id);
            return "Successfully Deleted";
        }

        internal IEnumerable<Archive> GetArchivesByAccountId(string id)
        {
            return _arepo.GetArchivesByOwnerId(id);
        }

    }
}