using EnterpriseBlog.DependencyInjection.Interfaces;
using EnterpriseBlog.Services.Interfaces;
using EnterpriseBlog.Shared;
using Microsoft.Extensions.Options;

namespace EnterpriseBlog.Features.Components
{
    public class BlogListVM : BaseViewModel, ITransient
    {
        private readonly IBlogPostService _svc;
        public string SearchText { get; set; } = "";
        public List<BlogPostDTO> Blogs { get; private set; } = new();
        public BlogListVM(IBlogPostService blogPostService, ILogger<BaseViewModel> logger, IOptions<DevOptions> devOptions) : base(logger, devOptions)
        {
            _svc = blogPostService;
        }

        public async Task LoadBlogsAsync()
        {
            var result = await RunSafeAsync(() => _svc.GetBlogsAsync());

            if (result.Success && result.Data is not null)
            {
                Blogs = result.Data.ToList();
            }
          
        }

        public IEnumerable<BlogPostDTO> FilteredBlogs => Blogs
                .Where(b => string.IsNullOrWhiteSpace(SearchText) || b.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
    }
}
