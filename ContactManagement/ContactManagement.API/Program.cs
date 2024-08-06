using ContactManagement.API.Middleware;
using ContactManagement.API.Services.FileService;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var MyAllowSpecificOrigins = "_AllowLocalhost";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder => builder
            .WithOrigins("http://localhost:4200") // Add your frontend URL here
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();

app.Run();
