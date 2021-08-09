using System;
using System.Collections.Generic;
using sqwuakServer.Models;

namespace sqwuakServer.Repositories
{
    public class ArchivesRepository
    {
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
        //From ACCOUNT Controller
        internal IEnumerable<Archive> GetArchivesByOwnerId(string id)
        {
            throw new NotImplementedException();
        }

    }
}