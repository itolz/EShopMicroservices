var builder = WebApplication.CreateBuilder(args);

//add the services to container

var app = builder.Build();

//configure the HTTP request pipeline

app.Run();
