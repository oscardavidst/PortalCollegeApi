using Application;
using Application.Exceptions;
using Identity;
using Identity.Models;
using Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Persistence;
using Shared;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

try
{
    #region Cors configuration
    var MyAllowSpecificOrigins = "MyCors";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
            policy =>
            {
                //policy.WithOrigins("*");
                policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                .AllowAnyHeader().AllowAnyMethod();
            });
    });
    #endregion

    // Add services to the container.
    builder.Services.AddApplicationLayer(builder.Configuration);
    builder.Services.AddIdentityInfraestructure(builder.Configuration);
    builder.Services.AddSharedInfraestructure(builder.Configuration);
    builder.Services.AddPersistenceInfraestructure(builder.Configuration);

    builder.Services.AddControllers();

    #region Swagger configuration
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(optionsSwagger =>
    {
        optionsSwagger.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "PortalCollegeApi",
            Description = "ASP.NET Core Web API para prueba técnica",
            Contact = new OpenApiContact
            {
                Name = "Oscar David Soto Tellez",
                Url = new Uri("https://www.linkedin.com/in/oscar-david-soto/")
            }
        });
        optionsSwagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Porfavor ingrese el token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        optionsSwagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{ new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }}, new string [] {} }});
    });
    #endregion

    var app = builder.Build();

    #region Seeds Roles
    var services = app.Services.CreateAsyncScope();
    var userManager = services.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DefaultRoles.SeedAsync(userManager, roleManager);
    await DefaultAdministratorUser.SeedAsync(userManager, roleManager);
    #endregion

    app.UseAuthentication();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(MyAllowSpecificOrigins);
    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UserErrorHandlingMiddleware();
    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    throw new ApiException(ex.ToString());
}
