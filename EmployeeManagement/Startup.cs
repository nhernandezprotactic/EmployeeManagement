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

            /*
             * UseFileServer combines the functionality of UseStaticFiles, UseDefaultFiles and UseDirectoryBrowser middleware. DirectoryBrowser middleware, enables directory browsing and allows users to see files within a specified directory. We could replace UseStaticFiles and UseDefaultFiles middlewares with UseFileServer Middleware.
             */

            // Use UseFileServer instead of UseDefaultFiles & UseStaticFiles
            FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            #region File server Middleware
            app.UseFileServer(fileServerOptions); 
            #endregion

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hola mundo");
            });
        }
    }
}
