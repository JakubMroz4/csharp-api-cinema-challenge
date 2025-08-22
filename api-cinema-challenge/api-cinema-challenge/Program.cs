using api_cinema_challenge.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Dependency injection
//builder.Services.AddDbContext<CinemaContext>();
builder.Services.AddDbContext<CinemaContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"))
    .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    options.LogTo(message => Debug.WriteLine(message));
    options.EnableSensitiveDataLogging();
});

// security
builder.Services.AddAuthentication().AddJwtBearer();
//builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Demo API");
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// endpoints configuration

app.Run();
