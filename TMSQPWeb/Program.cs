using Microsoft.AspNetCore.Identity;

//using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;
using Serilog.Sinks;
using Serilog.Extensions;


//using Microsoft.Extensions.Configuration;
//using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/init-log-.log",
        rollingInterval: RollingInterval.Hour,  //每一小時重新產新新的檔案
        retainedFileCountLimit: 720             //Log保留時間(24 hr * 30 Day=720)
    )
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<SmtpSettingsModel>(configuration.GetSection("SmtpSettings"));


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<FileManagementService>();
builder.Services.AddScoped<PDFExportService>();
builder.Services.AddScoped<SendMailService>();
builder.Services.AddScoped<WebSystemService>();


builder.Services.AddScoped<BaseWebPageRepository>();
builder.Services.AddScoped<BaseWebPageService>();

builder.Services.AddScoped<BaseWebMenuitemRepository>();
builder.Services.AddScoped<BaseWebMenuitemService>();


builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeBindingService>();

builder.Services.AddScoped<PayslipImportHeadRepository>();
builder.Services.AddScoped<PayslipImportRepository>();
builder.Services.AddScoped<PayslipImportService>();
builder.Services.AddScoped<PayslipImportBindingService>();


builder.Services.AddScoped<NotificationHeadRepository>();
builder.Services.AddScoped<NotificationDetailRepository>();
builder.Services.AddScoped<NotificationRecipientRepository>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<NotificationBindingService>();


builder.Services.AddScoped<SysConfigRepository>();
builder.Services.AddScoped<SysConfigService>();





var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TMSQPDbContext>(options =>
    options.UseSqlServer(connectionString)
    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddSerilog()))
    );
    

// Add services to the container.
//var connectionStringId = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<TMSQPWeb.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TMSQPWeb.Data.ApplicationDbContext>();
builder.Services.AddRazorPages();



builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
