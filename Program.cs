
using QuickBookApi.Repository.Interfaces;
using QuickBookApi.Repository.Services;

namespace QuickBookApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IQuickBookService, QuickBookService>();
        builder.Services.AddScoped<ITokenService, TokenService>();

        var devCorsPolicy = "devCorsPolicy";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(devCorsPolicy, builder =>
            {
                builder.WithOrigins("https://localhost:7178").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                //builder.SetIsOriginAllowed(origin => true);
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(devCorsPolicy);
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
