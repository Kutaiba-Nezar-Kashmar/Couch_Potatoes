using User.API.Installers;
using User.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InstallMiddlewares();
builder.Services.InstallMappings();
builder.Services.InstallUserServiceDependencies();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.MapControllers();

app.Run();
