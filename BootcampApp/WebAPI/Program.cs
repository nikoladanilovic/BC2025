using Autofac;
using Autofac.Extensions.DependencyInjection;
using BootcampApp.Repository;
using BootcampApp.Service;
using BootcampaApp.Service.Common;
using BootcampApp.Repository.Common;


var builder = WebApplication.CreateBuilder(args);

// 1. Replace default ServiceProvider with Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

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
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
