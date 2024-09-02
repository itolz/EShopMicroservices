var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate; //create the schema in the database in the runtime
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    //.AddJsonFile("appsettings.Docker.json", optional: true)
    .AddEnvironmentVariables();

//add the services to container

var app = builder.Build();

//configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
