using CatalogRestApi.Services.Products;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddEndpointsApiExplorer();
}

var app = builder.Build();

{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseCors(options =>
    options.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

