using EnterpriseBlog.DependencyInjection.Interfaces;
using EnterpriseBlog.Services.Interfaces;
using EnterpriseBlog.Shared;
using Microsoft.Extensions.Options;

namespace EnterpriseBlog.Features.WritingDesk
{
    public class DeskVM : BaseViewModel, ITransient
    {
        private readonly IBlogPostService _svc;

        public List<BlogPostDTO> Drafts { get; private set; } = new();
        public List<BlogPostDTO> Published { get; private set; } = new();
        public DeskVM(IBlogPostService blogPostService, ILogger<BaseViewModel> logger, IOptions<DevOptions> devOptions) : base(logger, devOptions)
        {
            _svc = blogPostService;
        }

        public async Task LoadBlogsAsync()
        {
            var result = await RunSafeAsync(() => _svc.GetBlogsAsync());

            if (result?.Success == true && result.Data != null)
            {
                Drafts = result.Data.Where(b => !b.IsPublished).ToList();
                Published = result.Data.Where(b => b.IsPublished).ToList();
            }
        }


    }
}
