using PageBuilder.Core.Contracts;
using PageBuilder.Core.Services;

namespace PageBuilder.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddHttpClient();
            

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IEngineFactory, EngineFactory>();
            builder.Services.AddScoped<IOpenAiService, OpenAiService>();
            builder.Services.AddScoped<IRetryPolicyService, RetryPolicyService>();

            builder.Services.AddScoped<DefaultEngineService>();
            builder.Services.AddScoped<EngineV2Service>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}
