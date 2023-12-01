
using TrabajoPracticoP3.DBContext;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoP3.Services.Interfaces;
using TrabajoPracticoP3.Services.Implementations;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("TrabajoPracticoP3BearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "TrabajoPracticoP3BearerAuth" }
                }, new List<string>() }
    });
}); ;

builder.Services.AddDbContext<ECommerceContext>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["DB:ConnectionString"]));

#region Inyecciones de dependencias
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IClientServices, ClientServices>();
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<ISaleOrderLineServices, SaleOrderLineServices>();


#endregion

builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticación que tenemos que elegir después en PostMan para pasarle el token
    .AddJwtBearer(options => //Acá definimos la configuración de la autenticación. le decimos qué cosas queremos comprobar. La fecha de expiración se valida por defecto.
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();