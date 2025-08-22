using EnterpriseBlog.DependencyInjection.Interfaces;
using EnterpriseBlog.Services.Interfaces;
using EnterpriseBlog.Shared;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;

namespace EnterpriseBlog.Features.Home
{
    public class IndexVM : BaseViewModel, ITransient
    {
        private readonly IBlogPostService _svc;

        public List<BlogPostDTO> Blogs = new();

        public IndexVM(IBlogPostService blogPostService, ILogger<BaseViewModel> logger, IOptions<DevOptions> devOptions) : base(logger, devOptions)
        {
            _svc = blogPostService;
        }

        public async Task LoadBlogsAsync()
        {
            var result = await _svc.GetBlogsAsync();

            if (result.Success && result.Data is not null)
            {
                Blogs = result.Data.Where(b => b.IsPublished).ToList();
            }
        }
    }
}
