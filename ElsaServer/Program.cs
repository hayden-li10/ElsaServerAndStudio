using Elsa.Extensions;
using Elsa.Persistence.EFCore.Extensions;
using Elsa.Persistence.EFCore.Modules.Management;
using Elsa.Persistence.EFCore.Modules.Runtime;
using Elsa.Persistence.MongoDb;
using Elsa.Persistence.MongoDb.Extensions;
using Elsa.Persistence.MongoDb.Modules.Management;
using Elsa.Persistence.MongoDb.Modules.Runtime;
using Elsa.Workflows;
using Elsa.Workflows.Runtime.Distributed.Extensions;
using ElsaServer.SchedulingEngine.Workflows;
using Microsoft.AspNetCore.Mvc;
using SchedulingEngine.BusinessEngine.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStaticWebAssets();

var services = builder.Services;
var configuration = builder.Configuration;
var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=ElsaQuartzLocal;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=True;";
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb")
    ?? throw new InvalidOperationException("Connection string 'MongoDb' not found.");

services
    .AddElsa(elsa => elsa
        .UseIdentity(identity =>
        {
            identity.TokenOptions = options => options.SigningKey = "large-signing-key-for-signing-JWT-tokens";
            identity.UseAdminUserProvider();
        })
        .UseDefaultAuthentication()
        //.UseWorkflowRuntime(runtime => runtime.UseDistributedRuntime()) // Enable distributed runtime
        //.UseScheduling(scheduling => scheduling.UseQuartzScheduler()) // Comment out to Disable Quartz scheduling
        //.UseQuartz(quartz => quartz.UseSqlServer(connectionString)) // Comment out to Disable Quartz scheduling
        .UseMongoDb(mongoConnectionString)
        .UseWorkflowManagement(management => management.UseMongoDb())// Configure workflow management (definitions, instances)
        .UseWorkflowRuntime(runtime => runtime.UseMongoDb())// Configure workflow runtime (bookmarks, inbox, execution logs)
        .UseJavaScript()
        .UseLiquid()
        .UseCSharp()
        .UseHttp(http => http.ConfigureHttpOptions = options => configuration.GetSection("Http").Bind(options))
        .UseWorkflowsApi()
        .AddActivitiesFrom<Program>()
        .AddWorkflowsFrom<Program>()
    );
services.AddCors(cors => cors.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*")));
services.AddRazorPages(options => options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseRouting();
app.UseCors();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseWorkflowsApi();
app.UseWorkflows();
app.MapFallbackToPage("/_Host");

app.Run();