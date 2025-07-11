using EnterpriseBlog.Shared;

namespace EnterpriseBlog.Repositories
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPostDTO>> GetAllAsync();
        Task<BlogPostDTO?> GetByIdAsync(Guid blogId);
        Task<BlogPostDTO> CreateAsync(BlogPostDTO blog);
        Task<BlogPostDTO?> UpdateAsync(BlogPostDTO blog);
        Task<bool> DeleteAsync(Guid blogId);
    }
}
