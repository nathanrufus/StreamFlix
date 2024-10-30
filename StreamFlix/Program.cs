using Microsoft.EntityFrameworkCore;
using StreamFlix.Data;
using StreamFlix.Services;
using Amazon.S3;
using Amazon.DynamoDBv2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the database context for Entity Framework with SQL Server (RDS)
builder.Services.AddDbContext<StreamFlixDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RDSConnection")));

// Configure AWS services for S3 and DynamoDB
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAWSService<IAmazonDynamoDB>();

// Add custom services for AWS integrations
builder.Services.AddSingleton<S3Service>();
builder.Services.AddSingleton<DynamoDBService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
