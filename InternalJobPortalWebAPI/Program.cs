
using InternalJobPortalLibrary.Repos;
using JobPostLibrary.Repos;

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
            builder.Services.AddScoped<IJobRepoAsync, EFJobRepoAsync>();
            builder.Services.AddScoped<ISkillRepo, EFSkillRepo>();
            builder.Services.AddScoped<IJobSkillRepoAsync, EFJobSkillRepoAsync>();
            builder.Services.AddScoped<IJobPostRepoAsync, EFJobPostRepoAsync>();
            builder.Services.AddScoped<IEmployeeRepoAsync, EFEmployeeRepoAsync>();
            builder.Services.AddScoped<IEmployeeSkillRepoAsync, EFEmployeeSkillRepoAsync>();
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
