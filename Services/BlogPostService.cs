using EnterpriseBlog.DependencyInjection.Interfaces;
using EnterpriseBlog.Repositories;
using EnterpriseBlog.Services.Interfaces;
using EnterpriseBlog.Shared;

namespace EnterpriseBlog.Services
{
    public class BlogPostService : IBlogPostService, IScoped 
    {
        private readonly IBlogPostRepository _repo;

        public BlogPostService(IBlogPostRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseEnvelope<BlogPostDTO>> CreateBlogAsync(BlogPostDTO blog)
        {
            var created = await _repo.CreateAsync(blog);
            return ResponseEnvelope<BlogPostDTO>.CreateSuccessEnvelope(created);
        }

        public async Task<ResponseEnvelope<BlogPostDTO>> GetBlogAsync(Guid blogId)
        {
            var blog = await _repo.GetByIdAsync(blogId);
            return blog is null
                ? ResponseEnvelope<BlogPostDTO>.CreateFailureEnvelope("Blog not found.")
                : ResponseEnvelope<BlogPostDTO>.CreateSuccessEnvelope(blog);
        }

        public async Task<ResponseEnvelope<BlogPostDTO>> UpdateBlogAsync(Guid blogId, BlogPostDTO blog)
        {
            blog.Id = blogId;
            var updated = await _repo.UpdateAsync(blog);
            return updated is null
                ? ResponseEnvelope<BlogPostDTO>.CreateFailureEnvelope("Blog not found.")
                : ResponseEnvelope<BlogPostDTO>.CreateSuccessEnvelope(updated);
        }

        public async Task<ResponseEnvelope<BlogPostDTO>> DeleteBlogAsync(Guid blogId)
        {
            var success = await _repo.DeleteAsync(blogId);
            return success
                ? ResponseEnvelope<BlogPostDTO>.CreateSuccessEnvelope(null!)
                : ResponseEnvelope<BlogPostDTO>.CreateFailureEnvelope("Blog not found.");
        }

        public async Task<ResponseEnvelope<BlogPostDTO>> PublishBlogAsync(Guid blogId)
        {
            var blog = await _repo.GetByIdAsync(blogId);
            if (blog is null)
                return ResponseEnvelope<BlogPostDTO>.CreateFailureEnvelope("Blog not found.");

            blog.IsPublished = true;
            var updated = await _repo.UpdateAsync(blog);
            return ResponseEnvelope<BlogPostDTO>.CreateSuccessEnvelope(updated!);
        }

        public async Task<ResponseEnvelope<BlogPostDTO>> UnPublishBlogAsync(Guid blogId)
        {
            var blog = await _repo.GetByIdAsync(blogId);
            if (blog is null)
                return ResponseEnvelope<BlogPostDTO>.CreateFailureEnvelope("Blog not found.");

            blog.IsPublished = false;
            var updated = await _repo.UpdateAsync(blog);
            return ResponseEnvelope<BlogPostDTO>.CreateSuccessEnvelope(updated!);
        }

    }
}
