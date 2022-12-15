namespace EventManager.Web.App.Startup
{
    public static class Middlewares
    {
        public static void Use(IApplicationBuilder app, IHostEnvironment env, IConfiguration configuration)
        {// Configure the HTTP request pipeline.

            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

        }
    }
}
