using GripCaseStudy.Repositories.Concretes;
using GripCaseStudy.Repositories.Interfaces;
using GripCaseStudy.Repositories;
using GripCaseStudy.Services.Concretes;
using GripCaseStudy.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddSingleton<GripCaseContext>();
    services.AddCors();

    using (var serviceScope = services.BuildServiceProvider().GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<GripCaseContext>();
        await context.Init();
    }
    
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IImageRepository, ImageRepository>();
    services.AddScoped<IUserService, UserService>();
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Security:JwtKey"));

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
