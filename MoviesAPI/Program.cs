//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();








using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Filters;
using MoviesAPI.Middlwares;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDBContext>(optionsAction=> optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add<LogExceptionFilter>();
//});

//builder.Services.AddCors(option =>
//{
//    var froentendurl = builder.Configuration.GetValue<string>("froentendurl");
//    option.AddDefaultPolicy(builder =>
//    {
//        builder.WithOrigins(froentendurl).AllowAnyMethod().AllowAnyHeader();
//        builder.WithOrigins("http://localhost").AllowAnyMethod().AllowAnyHeader();
//    });
//});

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton<IRepository, InMemoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.UseMiddleware<ExceptionHandlingMiddleware>();

// Use CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();
app.Run();
