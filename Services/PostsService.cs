using System;
using System.Collections.Generic;
using System.Linq;
using sqwuakServer.Models;
using sqwuakServer.Repositories;

namespace sqwuakServer.Services
{
    public class PostsService
    {
        private readonly PostsRepository _prepo;
        private readonly ArchivesRepository _archrepo;
        public PostsService(ProfilesRepository ps, PostsRepository prepo, ArchivesRepository archrepo)
        {
            _prepo = prepo;
            _archrepo = archrepo;
        }
        public IEnumerable<Post> GetAll()
        {
            IEnumerable<Post> posts = _prepo.GetAll();
            return posts;
        }

        internal Post GetById(int id)
        {
            Post post = _prepo.GetById(id);
            if (post == null)
            {
                throw new Exception("Invalid Id");
            }
            return post;
        }

        public Post Create(Post newPost)
        {
            newPost.Id = _prepo.Create(newPost);
            return newPost;
        }

        internal Post Edit(Post updated, string id)
        {
            Post original = GetById(updated.Id);
            if (original.CreatorId != id) { throw new Exception("Access Denied: You can only edit content you have created."); }
            updated.Title = updated.Title == null ? original.Title : updated.Title;
            updated.Description = updated.Description == null ? original.Description : updated.Description;
            updated.Img = updated.Img == null ? original.Img : updated.Img;
            return _prepo.Edit(updated);
        }


        internal string Delete(int id, string userId)
        {
            Post original = _prepo.GetById(id);
            if (original == null) { throw new Exception("Invalid ID"); }
            if (original.CreatorId != userId) { throw new Exception("Access Denied: You can only delete content you have created."); }
            _prepo.Remove(id);
            return "Successfully Deleted";
        }

        // Accounts
        internal IEnumerable<Post> GetPostsByAccountId(string id)
        {
            return _prepo.GetByOwnerId(id);
        }

        // Profiles
        internal IEnumerable<Post> GetPostsByProfileId(string id)
        {
            return _prepo.GetPostsByProfileId(id).ToList().FindAll(p => p.CreatorId == id);
        }

        //get posts inside archives
        internal IEnumerable<ArchPostModel> GetPostsByArchiveId(int id)
        {
            Archive archive = _archrepo.GetArchivesById(id);
            if (archive == null)
            {
                throw new Exception("Invalid Id, or Archive is Private.");
            }
            return _prepo.GetPostsByArchivesId(id);

        }
    }
}