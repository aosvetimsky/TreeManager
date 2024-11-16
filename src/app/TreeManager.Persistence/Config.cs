using Microsoft.Extensions.DependencyInjection;
using TreeManager.Domain.Trees;
using TreeManager.Persistence.Repositories;

namespace TreeManager.Persistence
{
    public static class Config
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddTransient<IErrorEntryRepository, ErrorEntryRepository>();
            services.AddTransient<IErrorEntryReadOnlyRepository, ErrorEntryRepository>();

            services.AddTransient<ITreeNodeRepository, TreeNodeRepository>();
            services.AddTransient<ITreeRepository, TreeRepository>();
        }
    }
}
