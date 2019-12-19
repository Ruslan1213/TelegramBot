using Introspect.DAL.Repositories;
using Introspekt.DAL;
using Introspekt.DAL.Models;
using Introspekt.DAL.Repositories;
using Introspekt_bot.Abstraction;
using Introspekt_bot.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Introspekt_bot.Dependency
{
    public class InjectDependency
    {
        private static InjectDependency injectDependency;

        private ServiceProvider provider;

        public InjectDependency()
        {
            provider = new ServiceCollection()
                .AddSingleton(x => new CourseStoreContext())
                .AddSingleton<IRepository<User>, RepositoryUser>()
                .AddSingleton<IRepository<OrderDetails>, OrderDetailsRepository>()
                .AddSingleton<IRepository<Course>, CourseRepository>()
                .AddSingleton<IUserRepository, RepositoryUser>()
                .AddSingleton<IOrderRepository, OrderRepository>()
                .AddSingleton<ICourseService, CourseService>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IOrderDetailsService, OrderDetailsService>()
                .AddSingleton<IOrderService, OrderService>()
                .BuildServiceProvider();
        }

        public static InjectDependency GetInstance()
        {
            if (injectDependency == null)
            {
                injectDependency = new InjectDependency();
            }

            return injectDependency;
        }

        public IUserService GetUserService()
        {
            return provider.GetService<IUserService>();
        }

        public IOrderRepository GetOrderRepository()
        {
            return provider.GetService<IOrderRepository>();
        }

        public IOrderDetailsService GetOrderDetailsService()
        {
            return provider.GetService<IOrderDetailsService>();
        }

        public ICourseService GetCourseService()
        {
            return provider.GetService<ICourseService>();
        }

        public IOrderService GetOrderService()
        {
            return provider.GetService<IOrderService>();
        }
    }
}
