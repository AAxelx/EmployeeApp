using EmployeeApp.API.AutoMapperProfiles;
using EmployeeApp.DAL.Repositories;
using EmployeeApp.DAL.Repositories.Abstractions;
using EmployeeApp.SAL.Services;
using EmployeeApp.SAL.Services.Abstractions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = configurationBuilder.Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddSingleton(connectionString);

        builder.Services.AddAutoMapper(typeof(MapperProfiles));

        builder.Services.AddScoped<IEmployeesService, EmployeesService>();

        builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();

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
    }
}