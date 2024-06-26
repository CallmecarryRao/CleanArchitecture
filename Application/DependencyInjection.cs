using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicaiton(this IServiceCollection service)
    {
        service.AddMediatR(options => { options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)); });
        return service;
    }
}