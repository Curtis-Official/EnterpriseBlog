using EnterpriseBlog.Shared;

namespace EnterpriseBlog.Repositories
{

    public class MockBlogPostRepository : IBlogPostRepository
    {
        private readonly List<BlogPostDTO> _blogs = new()
    {
        new BlogPostDTO
        {
            Id = Guid.NewGuid(),
            Title = "First Mock Blog",
            Subtitle = "A mock subtitle",
            BlogContent = "This is a mock blog post content for UI testing.",
            IsPublished = true,
            Comments = new List<Comment>
            {
                new Comment { /* set properties as needed */ }
            }
        },
        new BlogPostDTO
        {
            Id = Guid.NewGuid(),
            Title = "Second Mock Blog",
            Subtitle = "Another subtitle",
            BlogContent = "Another mock blog post for testing.",
            IsPublished = false,
            Comments = new List<Comment>()
        }
    };

        public Task<List<BlogPostDTO>> GetAllAsync() => Task.FromResult(_blogs);

        public Task<BlogPostDTO?> GetByIdAsync(Guid blogId) =>
            Task.FromResult(_blogs.FirstOrDefault(b => b.Id == blogId));

        public Task<BlogPostDTO> CreateAsync(BlogPostDTO blog)
        {
            blog.Id = Guid.NewGuid();
            _blogs.Add(blog);
            return Task.FromResult(blog);
        }

        public Task<BlogPostDTO?> UpdateAsync(BlogPostDTO blog)
        {
            var existing = _blogs.FirstOrDefault(b => b.Id == blog.Id);
            if (existing == null) return Task.FromResult<BlogPostDTO?>(null);
            _blogs.Remove(existing);
            _blogs.Add(blog);
            return Task.FromResult<BlogPostDTO?>(blog);
        }

        public Task<bool> DeleteAsync(Guid blogId)
        {
            var blog = _blogs.FirstOrDefault(b => b.Id == blogId);
            if (blog == null) return Task.FromResult(false);
            _blogs.Remove(blog);
            return Task.FromResult(true);
        }
    }
}
