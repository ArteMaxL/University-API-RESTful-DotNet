// 1. Usings to work with Entity Framework
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2. Connection with SQL Server Express
const string CONNECTION_NAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTION_NAME);

// 3. Add Context to Services of builder
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// 7. Add Service oj JWT Authorization
//TODO:
// builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// 4. Add Custom Services (folder Services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
// TODO: Add the rest of services


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 8. TODO: Config Swagger to take care of Authorization of JWT
builder.Services.AddSwaggerGen();

// 5. CORS Configuration
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

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

// 6. Tell app to use CORS
app.UseCors("CorsPolicy");

app.Run();
