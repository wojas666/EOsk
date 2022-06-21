using EOsk.Infrastructure.EventBus;
using EOsk.Instructor.Api.DbContexts;
using EOsk.Instructor.Api.Features.Handlers.Commands;
using EOsk.Instructor.Api.Repository;
using EOsk.Instructor.Api.Repository.Contract;
using EOsk.Instructor.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using MassTransit;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<>
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<CreateInstructorCommandHandler>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var rabbitmqOption = new RabbitMqOption();
builder.Configuration.GetSection("RabbitMQ").Bind(rabbitmqOption);

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<CreateInstructorCommandHandler>();

    configure.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
        {
            config.Host(new Uri(rabbitmqOption.ConnectionString), hostconfig =>
            {
                hostconfig.Username(rabbitmqOption.UserName);
                hostconfig.Password(rabbitmqOption.Password);
            });

            config.ReceiveEndpoint("create_instructor", endpoint =>
            {
                endpoint.PrefetchCount = 16;
                endpoint.UseMessageRetry(retryConfig =>
                {
                    retryConfig.Interval(2, 100);
                });

                endpoint.ConfigureConsumer<CreateInstructorCommandHandler>(provider);
            });
        }));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

var busControl = app.Services.GetService<IBusControl>();
busControl.Start();

app.Run();
