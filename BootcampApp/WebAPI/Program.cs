using Autofac;
using Autofac.Extensions.DependencyInjection;
using BootcampaApp.Service.Common;
using BootcampApp.Repository;
using BootcampApp.Repository.Common;
using BootcampApp.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Log to console
builder.Logging.AddDebug();   // Log to debug window (VS)

// 1. Replace default ServiceProvider with Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


// Add services to the container.

builder.Services.AddControllers();

// Register the services for dependency injection

//builder.Services.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();
//builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();

//builder.Services.AddScoped<BootcampApp.Service.IMenuCategoryService, BootcampApp.Service.MenuCategoryService>();
//builder.Services.AddScoped<BootcampApp.Service.IMenuItemService, BootcampApp.Service.MenuItemService>();


// 3. Configure Autofac-specific registrations
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<MenuCategoryRepository>()
                    .As<IMenuCategoryRepository>()
                    .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<MenuItemRepository>()
                .As<IMenuItemRepository>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<MenuCategoryService>()
                .As<IMenuCategoryService>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<MenuItemService>()
                .As<IMenuItemService>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<StaffService>()
                .As<IStaffService>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<StaffRepository>()
                .As<IStaffRepository>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<CustomerService>()
                .As<ICustomerService>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<CustomerRepository>()
                .As<ICustomerRepository>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<OrderService>()
                .As<IOrderService>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<AdminService>()
                .As<IAdminService>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
    containerBuilder.RegisterType<AdminRepository>()
                .As<IAdminRepository>()
                .InstancePerLifetimeScope(); // Equivalent to Scoped
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("your-very-long-secret-key-that-is-at-least-33-bytes"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
