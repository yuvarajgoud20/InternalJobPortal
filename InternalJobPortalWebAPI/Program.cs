
using System.Text;
using InternalJobPortalLibrary.Repos;
using JobPostLibrary.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "https://www.snrao.com",
                    ValidAudience = "https://www.snrao.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Johny Johny yes papa....open your laptop HAHAHA!!!"))
                };
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
