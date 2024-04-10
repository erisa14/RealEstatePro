using DAL.Concrete;
using DAL.Contracts;
using Lamar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DI
{
    public class RepositoryRegistry : ServiceRegistry
    {
        public RepositoryRegistry()
        {
            IncludeRegistry<UnitOfWorkRegistry>();

            For<IUserRepository>().Use<UserRepository>();
            For<IRoleRepository>().Use<RoleRepository>();
            For<IPropertyRepository>().Use<PropertyRepository>();
            For<IPhotoRepository>().Use<PhotoRepository>();
        }


    }
}