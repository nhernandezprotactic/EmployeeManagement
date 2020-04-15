using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;
        /*ANHG 2020-04-12:
         3.-So we need to inject the IConfiguration Service into the startup class
        3.1.- For first we need a constructor. We use dependency injection to make this: */
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //ANHG:  If environment is Development then...
            if (env.IsDevelopment())
            {
                #region First middleware
                app.UseDeveloperExceptionPage(); 
                #endregion
            }

            // Specify foo.html as the default document
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("foo.html");

            #region Default files middleware
            app.UseDefaultFiles(defaultFilesOptions);
            #endregion


            #region Static files middleware
            app.UseStaticFiles();
            #endregion

            
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hola mundo");
            });
        }
    }
}
