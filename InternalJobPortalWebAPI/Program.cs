
using InternalJobPortalLibrary.Repos;

namespace InternalJobPortalWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IApplyJobRepo, EFApplyJobRepo>();
<<<<<<< HEAD
            builder.Services.AddScoped<IJobRepoAsync, EFJobRepoAsync>();
            builder.Services.AddScoped<ISkillRepo, EFSkillRepo>();
=======
            builder.Services.AddScoped<IJobSkillRepoAsync, EFJobSkillRepoAsync>();
>>>>>>> 6ba7ef51b512ac46b773ed39499fc9f57ffb173a
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
