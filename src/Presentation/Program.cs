using Microsoft.EntityFrameworkCore;
using Serilog;
using MediatR;
using System.Reflection;
using Presentation.Filters;
using Infrastructure.Persistence.Context;
using Application.Behaviors;
using Application.Features.Customer.Commands.Validators;
using FluentValidation;
using Domain.Aggregates.Customer.Entities;
using Infrastructure.Persistence.Repositories;


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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(CreateCustomerCommandValidator).Assembly);

builder.Services.AddScoped<Domain.Aggregates.Customer.Services.ICustomerUniquenessCheckerService, Infrastructure.Services.CustomerUniquenessCheckerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

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