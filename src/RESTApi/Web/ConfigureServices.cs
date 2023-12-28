namespace Web
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, string SpecificOrigins) 
        {
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: SpecificOrigins,
                     policy =>
                     {
                         policy.WithOrigins("http://localhost:4200", "http://localhost:4200/registration/user-register")
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials()
                         .WithExposedHeaders("X-Pagination");

                     });
            });

            return services;
        }
    }
}
