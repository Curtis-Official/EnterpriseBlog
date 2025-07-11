using EnterpriseBlog.DependencyInjection.Interfaces;
using EnterpriseBlog.Services;
using EnterpriseBlog.Shared;

namespace EnterpriseBlog.Repositories
{
    public class BlogPostRepository : IBlogPostRepository, IScoped
    {
        private readonly BlogStateService _state;

        public BlogPostRepository(BlogStateService state)
        {
            _state = state;
        }

        public Task<List<BlogPostDTO>> GetAllAsync()
            => Task.FromResult(_state.Blogs);

        public Task<BlogPostDTO?> GetByIdAsync(Guid id)
            => Task.FromResult(_state.Blogs.FirstOrDefault(b => b.Id == id));

        public async Task<BlogPostDTO> CreateAsync(BlogPostDTO blog)
        {
            await _state.AddAsync(blog);
            return blog;
        }

        public Task<BlogPostDTO?> UpdateAsync(BlogPostDTO blog)
            => _state.UpdateAsync(blog);

        public Task<bool> DeleteAsync(Guid id)
            => _state.DeleteAsync(id);
    }
}
