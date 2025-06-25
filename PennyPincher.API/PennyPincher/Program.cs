
using PennyPincher.Repositories;

namespace PennyPincher
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Load env variables from .env
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            //connection string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add configuration for connection string

            

            // Add services to the container.
            builder.Services.AddControllers()
                .AddNewtonsoftJson() // This replaces the default JSON input and output formatters with JSON.NET
                .AddXmlDataContractSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add custom repository services 
            //builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
            builder.Services.AddScoped<IDbService, DbService>();
            builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
            builder.Services.AddScoped<IAnalysisRepository, AnalysisRepository>();



            var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!.Split(",");
            
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy((policy =>
                {
                    policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
                }));
            });
            
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            app.UseCors();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
