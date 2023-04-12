using MovieInformation.API.Installers;

var builder = WebApplication.CreateBuilder(args);
ApiServiceInstaller.Install(builder.Services);

var app = builder.Build();

// app.MapGet("/movies", () => "Hello World!");

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(b=> b.
    AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// }
app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

// Add services here:

app.Run();