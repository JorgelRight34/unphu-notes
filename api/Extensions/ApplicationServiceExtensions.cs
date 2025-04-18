using System;
using api.Clients;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Repositories;
using api.Services;
using Microsoft.EntityFrameworkCore;

namespace api.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                config?.GetConnectionString("DefaultConnection"),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
            );
        });
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ISubjectGroupRepository, SubjectGroupRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IFileUploadService, FileUploadService>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddHttpClient("UNPHU", client =>
        {
            client.BaseAddress = new Uri("https://client-api-gateway.unphusist.unphu.edu.do/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        services.AddScoped<IUNPHUClient, UNPHUClient>();
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
