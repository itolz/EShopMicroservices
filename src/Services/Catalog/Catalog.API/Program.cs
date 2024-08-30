using Marten;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    //opts.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate; //create the schema in the database in the runtime
}).UseLightweightSessions();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    //.AddJsonFile("appsettings.Docker.json", optional: true)
    .AddEnvironmentVariables();

//add the services to container

var app = builder.Build();

//configure the HTTP request pipeline
app.MapCarter();

app.Run();
