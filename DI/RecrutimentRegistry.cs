
using DAL.DI;
using Domain.DI;
using Lamar;

namespace DI
{
    public class RecrutimentRegistry : ServiceRegistry
    {
        public RecrutimentRegistry()
        {
            //Register domain DI
            IncludeRegistry<DomainRegistry>();
            IncludeRegistry<RepositoryRegistry>();
        }
    }
}
