using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GloboTicket.TicketManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            try
            {
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                services.AddMediatR(Assembly.GetExecutingAssembly());
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (Exception inner in ex.LoaderExceptions)
                {
                    throw new Exception(inner.Message);
                }
            }
            return services;
        }
    }
}
