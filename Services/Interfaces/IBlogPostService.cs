using EnterpriseBlog.Shared;

namespace EnterpriseBlog.Services.Interfaces
{
    public interface IBlogPostService
    {
        Task<ResponseEnvelope<List<BlogPostDTO>>> GetBlogsAsync();
        Task<ResponseEnvelope<BlogPostDTO>> CreateBlogAsync(BlogPostDTO blog);
        Task<ResponseEnvelope<BlogPostDTO>> GetBlogAsync(Guid blogId);
        Task<ResponseEnvelope<BlogPostDTO>> UpdateBlogAsync(Guid blogId, BlogPostDTO blog);
        Task<ResponseEnvelope<BlogPostDTO>> DeleteBlogAsync(Guid blogId);
        Task<ResponseEnvelope<BlogPostDTO>> PublishBlogAsync(Guid blogId);
        Task<ResponseEnvelope<BlogPostDTO>> UnPublishBlogAsync(Guid blogId);
    }
}
