using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        /*private readonly ApplicationContext _DBcontext;

        public PostService(ApplicationContext context)
        {
            _DBcontext = context;
        }
        public async Task<bool> CreatePostAsync(Post post)
        {
            await _DBcontext.Posts.AddAsync(post);
            var createdCount = await _DBcontext.SaveChangesAsync();
            return createdCount > 0;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post = await GetPostByIdAsync(postId);
            _DBcontext.Posts.Remove(post);
            var deletedCount = await _DBcontext.SaveChangesAsync();
            return deletedCount > 0;
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _DBcontext.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _DBcontext.Posts.ToListAsync();
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            _DBcontext.Posts.Update(post);
            var updatedCount = await _DBcontext.SaveChangesAsync();
            return updatedCount > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post = await _DBcontext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);
            if (post == null)
                return false;
            if (post.UserId != userId)
            {
                return false;
            }
            return true;
        }*/
    }
}
