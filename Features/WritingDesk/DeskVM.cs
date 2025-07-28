using EnterpriseBlog.DependencyInjection.Interfaces;
using EnterpriseBlog.Services.Interfaces;
using EnterpriseBlog.Shared;
using Microsoft.Extensions.Options;

namespace EnterpriseBlog.Features.WritingDesk
{
    public class DeskVM : BaseViewModel, ITransient
    {
        private readonly IBlogPostService _svc;
        protected DeskVM(IBlogPostService blogPostService,ILogger logger, IOptions<DevOptions> options) : base(logger, options)
        {
            _svc = blogPostService;
        }


    }
}
