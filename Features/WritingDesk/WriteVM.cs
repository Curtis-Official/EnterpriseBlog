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

        public WriteVM(IBlogPostService blogPostService, ILogger<BaseViewModel> logger, IOptions<DevOptions> devOptions) : base(logger, devOptions)
        {
            _svc = blogPostService;
        }

        public async Task LoadBlogAsync(Guid blogId)
        {
            var result = await RunSafeAsync(() => _svc.GetBlogAsync(blogId));
            if (result?.Success == true && result.Data != null)
            {
                Title = result.Data.Title;
                Subtitle = result.Data.Subtitle;
                BlogContent = result.Data.BlogContent;
            }
        }

        public async Task<ResponseEnvelope<BlogPostDTO>> SaveAsDraftAsync(Guid blogId = default)
        {
            var request = ToBlogPostDraftRequest();

            if (blogId == Guid.Empty)
            {
                return await RunSafeAsync(() => _svc.CreateBlogAsync(request));
            }
            else
            {
                return await RunSafeAsync(() => _svc.UpdateBlogAsync(blogId, request));
            }
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
