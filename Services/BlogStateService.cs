using Blazored.LocalStorage;
using EnterpriseBlog.DependencyInjection.Interfaces;
using EnterpriseBlog.Shared;

namespace EnterpriseBlog.Services
{
    public class BlogStateService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "blogs";

        public List<BlogPostDTO> Blogs { get; private set; } = new();

        public event Action? OnChange;

        public BlogStateService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public async Task LoadAsync()
        {
            Blogs = await _localStorage.GetItemAsync<List<BlogPostDTO>>(StorageKey) ?? new();
            NotifyStateChanged();
        }

        public async Task SaveAsync()
        {
            await _localStorage.SetItemAsync(StorageKey, Blogs);
            NotifyStateChanged();
        }

        public async Task AddAsync(BlogPostDTO blog)
        {
            blog.Id = Guid.NewGuid();
            Blogs.Add(blog);
            await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var blog = Blogs.FirstOrDefault(b => b.Id == id);
            if (blog is null) return false;

            Blogs.Remove(blog);
            await SaveAsync();
            return true;
        }

        public async Task<BlogPostDTO?> UpdateAsync(BlogPostDTO blog)
        {
            var existing = Blogs.FirstOrDefault(b => b.Id == blog.Id);
            if (existing is null) return null;

            existing.Title = blog.Title;
            existing.Subtitle = blog.Subtitle;
            existing.BlogContent = blog.BlogContent;

            await SaveAsync();
            return existing;
        }
    }
}
