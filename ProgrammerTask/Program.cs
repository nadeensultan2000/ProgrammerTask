using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.Security.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ProgrammerTask.Data;
using ProgrammerTask.Models;

namespace ProgrammerTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Reports
            builder.Services.AddDevExpressControls();



            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    ));
            //builder.Services.AddControllersWithViews().AddJsonOptions(options=>options.JsonSerializerOptions.PropertyNamingPolicy=null);
            //builder.Services.AddDevExpressControls();
            //builder.Services.ConfigureReportingServices(conf => conf.DisableCheckForCustomControllers());

            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();



            //Reports
            builder.Services.ConfigureReportingServices(configurator =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    configurator.UseDevelopmentMode();
                }
                configurator.ConfigureReportDesigner(designerConfigurator =>
                {
                });
                configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
                {
                    viewerConfigurator.UseCachedReportSourceBuilder();
                });
            });




            var app = builder.Build();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //  FileProvider=new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath,"node_modules")),RequestPath="/node_modules"
            //});
            // Configure the HTTP request pipeline.



            //Reports
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
            }
            var contentDirectoryAllowRule = DirectoryAccessRule.Allow(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "Content")).FullName);
            AccessSettings.ReportingSpecificResources.TrySetRules(contentDirectoryAllowRule, UrlAccessRule.Allow());
            DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.DataBindingMode = DevExpress.XtraReports.UI.DataBindingMode.Expressions;
            app.UseDevExpressControls();
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;




            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            //  app.UseDevExpressControls();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}
