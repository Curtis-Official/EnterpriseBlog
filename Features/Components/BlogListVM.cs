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
        private List<BlogPostDTO> _blogs = new();
        public IEnumerable<BlogPostDTO> FilteredBlogs => _blogs.Where(b => string.IsNullOrWhiteSpace(SearchText) ||
                             b.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        public BlogListVM(IBlogPostService blogPostService, ILogger<BaseViewModel> logger, IOptions<DevOptions> devOptions) : base(logger, devOptions)
        {
            _svc = blogPostService;
        }

      

        public void SetBlogs(IEnumerable<BlogPostDTO> blogs)
        {
            _blogs = blogs?.ToList() ?? new List<BlogPostDTO>();
        }

    }
}
