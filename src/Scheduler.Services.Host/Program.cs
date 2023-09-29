using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionOrder.Service.Api.Configurations;
using Scheduler.Domain.Settings;
using Scheduler.Infra.CrossCutting.IoC;
using Scheduler.Infra.Data.Context;
using System.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SchedulerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchedulerContext") ?? throw new InvalidOperationException("Connection string 'SchedulerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
            options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors(options =>
options.AddPolicy(name: "MyAllowSpecificOrigins",
policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAutoMapperSetup();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;

EmailSettings emailSettings = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                              .AddJsonFile(path: "appsettings.json",
                                                                           optional: true)
                                                              .AddJsonFile(path: $"appsettings.{_environment}.json",
                                                                           optional: true,
                                                                           reloadOnChange: true)
                                                              .Build()
                                                              .GetSection("EmailSettings")
                                                              .Get<EmailSettings>();

RegisterServices(builder.Services, emailSettings);

WebApplication app = builder.Build();

app.UseCors("MyAllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterServices(IServiceCollection services, EmailSettings emailSettings)
{
    services.AddLogging(l => l.AddConsole());

    // IoC
    NativeInjectorBootStrapper.RegisterServices(services, emailSettings);
}