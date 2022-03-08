
using Microsoft.AspNetCore.Mvc.Formatters;
using Orleans;
using SearchModels.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileSearchEngine;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddRazorPages();
        //services.AddServerSideBlazor();
        //services.AddSingleton<WeatherForecastService>();
        //services.AddSingleton<TodoService>();
        services.AddCors();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddControllers(options => {
            options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
            options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            }));
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            
        });
       

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .SetIsOriginAllowed(origin => true) // allow any origin
                 .AllowCredentials()); // allow credentials
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            //endpoints.MapBlazorHub();
            //endpoints.MapFallbackToPage("/_Host");

            endpoints.MapGet("/getinfo", async () =>
            {
                //var grainFactory = app.ApplicationServices.GetRequiredService<IClusterClient>();
                var grainFactory = app.ApplicationServices.GetRequiredService<IGrainFactory>();
                // Get a reference to the HelloGrain grain with the key "friend".
                var friend = grainFactory.GetGrain<IDeviceInfoGrain>(new Guid().ToString());

                // Call the grain and print the result to the console
                var result = await friend.GetInfo();
                return result;
                //Console.WriteLine("\n\n{0}\n\n", result.ToString());
            })
       .WithName("GetInfo");
            
            endpoints.MapGet("/search/{keyword}", async (string keyword) =>
            {
                //var grainFactory = app.ApplicationServices.GetRequiredService<IClusterClient>();
                var grainFactory = app.ApplicationServices.GetRequiredService<IGrainFactory>();
                // Get a reference to the HelloGrain grain with the key "friend".
                var friend = grainFactory.GetGrain<IFileSearchGrain>(new Guid().ToString());

                // Call the grain and print the result to the console
                var result = await friend.SearchFile(keyword);
                return result;
                //Console.WriteLine("\n\n{0}\n\n", result.ToString());
            })
       .WithName("Search");

        });


    }
}