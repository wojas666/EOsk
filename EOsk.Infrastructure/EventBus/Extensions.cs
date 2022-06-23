using EOsk.Infrastructure.Events.Instructors.Requestes.Queries;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.EventBus
{
    public static class Extensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMq = new RabbitMqOption();
            configuration.GetSection("RabbitMQ").Bind(rabbitMq);

            // Establish connection with RabbitMQ...
            services.AddMassTransit(configure =>
            {
                configure.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(rabbitMq.ConnectionString, hostConfig =>
                    {
                        hostConfig.Username(rabbitMq.UserName);
                        hostConfig.Password(rabbitMq.Password);
                    });
                }));

                configure.AddRequestClient<GetInstructorByIdRequest>();
                configure.AddRequestClient<GetInstructorListRequest>();
            });

            return services;
        }
    }
}
