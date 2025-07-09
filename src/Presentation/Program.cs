using Microsoft.EntityFrameworkCore;
using Serilog;
using MediatR;
using System.Reflection;
using Presentation.Filters;
using Infrastructure.Persistence.Context;
using Application.Behaviors;
using FluentValidation;
using Domain.Aggregates.Customer.Entities;
using Infrastructure.Persistence.Repositories;
using Application.Features.Customer.Commands.CreateCustomer;
using Application.Features.Customer.Queries.GetCustomersList;
using Infrastructure;
using Infrastructure.Persistence.Interceptors;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddInterceptors(new ConvertDomainEventsToOutboxMessagesInterceptor()));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCustomersListQueryHandler).Assembly)); 
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(CreateCustomerCommandValidator).Assembly);

builder.Services.AddScoped<Domain.Aggregates.Customer.Services.ICustomerUniquenessCheckerService, Infrastructure.Services.CustomerUniquenessCheckerService>();
builder.Services.InfrastructureDI();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();