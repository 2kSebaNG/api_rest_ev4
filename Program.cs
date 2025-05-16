using ApiRest_Ev4.Data;
using ApiRest_Ev4.Repositories;
using ApiRest_Ev4.Services;
using ApiRest_Ev4.Services.Implements;
using ApiRest_Ev4.Services.Interfaces;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySQL(Env.GetString("DB_CONNECTION"));
});

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Env.GetString("JWT_SECRET"))),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Initialize seeder data
using (var scope = app.Services.CreateScope())
{
    await DataSeeder.InitializeData(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();