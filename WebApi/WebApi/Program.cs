using WebApi;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string connectionString = "Server=localhost;Port=5432;Database=mobile-app-db;User Id=postgres;Password=qweasd123";
builder.Services.AddNpgsql<ApplicationContext>(connectionString);
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<PostsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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