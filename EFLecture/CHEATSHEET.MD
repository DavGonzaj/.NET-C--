# Create our project
    dotnet new mvc --no-https -o ProjectName


# Install packages

    dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.3
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 6.0.1

# Make MyContext.cs in Models Folder with the following
        
     #pragma warning disable CS8618
    // We can disable our warnings safely because we know the framework will assign non-null values 
    // when it constructs this class for us.
    using Microsoft.EntityFrameworkCore;
    namespace EFLecture.Models;
    // the MyContext class represents a session with our MySQL database, allowing us to query for or save data
    // DbContext is a class that comes from EntityFramework, we want to inherit its features
        public class MyContext : DbContext
        {
            // This line will always be here. It is what constructs our context upon initialization  
            public MyContext(DbContextOptions options) : base(options) { }
            // We need to create a new DbSet<Model> for every model in our project that is making a table
            // The name of our table in our database will be based on the name we provide here
            // This is where we provide a plural version of our model to fit table naming standards    
            public DbSet<Dish> Dishes { get; set; }
        }

# appsettings.json
Change database name
    
    {  
        "Logging": {    
            "LogLevel": {      
                "Default": "Information",      
                "Microsoft.AspNetCore": "Warning"    
            }  
        },
        "AllowedHosts": "*",    
        "ConnectionStrings":    
        {        
            "DefaultConnection": "Server=localhost;port=3306;userid=root;password=root;database=dishdb;"    
        }
    }


# Inside our Program.cs

    // Add this using statement
    using Microsoft.EntityFrameworkCore;
    // You will need access to your models for your context file
    using YourProjectName.Models;

    var builder = WebApplication.CreateBuilder(args);
    // Create a variable to hold your connection string
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSession();
    // Make sure this is BEFORE var app = builder.Build()!!
    builder.Services.AddDbContext<MyContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.UseSession();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();


# Migration aka connect and make db in MySQL
        dotnet ef migrations add FirstMigration

        dotnet ef database update //creates schema
        //In MySQL
        SELECT * FROM DBTABLE;
