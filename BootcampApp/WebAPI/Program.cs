using BootcampApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register the services for dependency injection
builder.Services.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();

builder.Services.AddScoped<BootcampApp.Service.IMenuCategoryService, BootcampApp.Service.MenuCategoryService>();
builder.Services.AddScoped<BootcampApp.Service.IMenuItemService, BootcampApp.Service.MenuItemService>();



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
