using FullStackDeveloperTask.Api.Extensions;
using FullStackDeveloperTask.Api.Options;
using FullStackDeveloperTask.Core.Extensions;
using FullStackDeveloperTask.Data.Extensions;
using FullStackDeveloperTask.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReaders();
builder.Services.AddCore();
builder.Services.AddData(builder.Configuration);
builder.Services.AddServices();

var cors = builder.Configuration.GetSection(CorsOptions.Cors).Get<CorsOptions>();

builder.Services.AddCors(o =>
{
    o.AddPolicy(cors!.Name, p => p
        .WithOrigins(cors.Origin)
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(cors!.Name);

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RoboticsContext>();
    db.Database.Migrate();
}

app.Run();
