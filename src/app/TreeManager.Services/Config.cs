using Microsoft.Extensions.DependencyInjection;
using TreeManager.Contracts.ErrorJournal;
using TreeManager.Contracts.Tree;
using TreeManager.Services.Application.ErrorJournal;
using TreeManager.Services.Application.Tree;

namespace TreeManager.Services
{
    public static class Config
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IErrorJournalAppService, ErrorJournalAppService>();
            services.AddTransient<IErrorJournalEventIdGenerator, ErrorJournalEventIdGenerator>();
            services.AddSingleton<ErrorJournalMapper>();

            services.AddTransient<ITreeAppService, TreeAppService>();
            services.AddSingleton<TreeMapper>();
        }
    }
}
