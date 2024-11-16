using Microsoft.Extensions.DependencyInjection;
using TreeManager.Services.Api.Controllers.ErrorJournal;
using TreeManager.Services.Api.Controllers.Tree;

namespace TreeManager.Services.Api
{
    public static class Config
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddSingleton<TreeMapper>();
            services.AddSingleton<ErrorJournalMapper>();
        }
    }
}
