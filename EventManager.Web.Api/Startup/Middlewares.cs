namespace EventManager.Web.Api.Startup
{
    public static class Middlewares
    {
        public static void Use(IApplicationBuilder app, IHostEnvironment env, IConfiguration configuration)
        {// Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

          

        }
    }
}
