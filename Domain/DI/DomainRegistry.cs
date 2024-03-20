using DAL.DI;
using Domain.Concrete;
using Domain.Contracts;
using Entities.Models;
using Lamar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DI
{
    public class DomainRegistry : ServiceRegistry
    {
        public DomainRegistry()
        {
            IncludeRegistry<DomainUnitOfWorkRegistry>();

            For<IUserDomain>().Use<UserDomain>();


            For<UserManager<User>>().Use(ctx => ctx.GetInstance<IHttpContextAccessor>().HttpContext.RequestServices
            .GetRequiredService<UserManager<User>>());
            For<SignInManager<User>>().Use(ctx => ctx.GetInstance<IHttpContextAccessor>().HttpContext.RequestServices
            .GetRequiredService<SignInManager<User>>());


            AddRepositoryRegistries();
            AddHttpContextRegistries();
        }

        private void AddRepositoryRegistries()
        {
            IncludeRegistry<RepositoryRegistry>();
        }

        private void AddHttpContextRegistries()
        {
            For<IHttpContextAccessor>().Use<HttpContextAccessor>();
        }
    }
}