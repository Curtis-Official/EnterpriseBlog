using EnterpriseBlog.DependencyInjection.Interfaces;
using EnterpriseBlog.Features.Interfaces;
using EnterpriseBlog.Services.Interfaces;
using EnterpriseBlog.Shared;
using Microsoft.Extensions.Options;

namespace EnterpriseBlog.Features.WritingDesk
{
    public class WriteVM : BaseViewModel, ITransient, IBlogFormFields
    {
        private readonly IBlogPostService _svc;

        public string Title { get; set; } = string.Empty; 
        public string Subtitle { get; set; } = string.Empty; 
        public string BlogContent { get; set; } = string.Empty;

        protected WriteVM(IBlogPostService blogPostService, ILogger logger, IOptions<DevOptions> options) : base(logger, options)
        {
            _svc = blogPostService;
        }

        public async Task SaveAsDraftAsync()
        {
            var request = ToBlogPostDraftRequest();

            var result = await RunSafeAsync(() => _svc.CreateBlogAsync(request));
        }

        public BlogPostDTO ToBlogPostDraftRequest()
        {
            return new BlogPostDTO
            {
                Title = Title,
                Subtitle = Subtitle,
                BlogContent = BlogContent,
                IsPublished = false
            };
        }

        public void Cancel()
        {
            Title = string.Empty;
            Subtitle = string.Empty;
            BlogContent = string.Empty;
        }
    }
}
