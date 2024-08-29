var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

//add the services to container

var app = builder.Build();

//configure the HTTP request pipeline
app.MapCarter();

app.Run();
