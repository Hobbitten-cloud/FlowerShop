
namespace FlowerShopWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // add services to the container.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            // Routing
            app.MapGet("/flower", () =>
            {
                return "Reading all flowers";
            });

            app.MapPost("/flower", () =>
            {
                return "Creating a new flower";
            });

            app.MapGet("/flower/{id}", (int id) =>
            {
                return $"Reading flower with ID: {id}";
            });

            app.MapPut("/flower/{id}", (int id) =>
            {
                return $"Updating flower with ID: {id}";
            });

            app.MapDelete("/flower/{id}", (int id) =>
            {
                return $"Deleting flower with ID: {id}";
            });

            app.Run();
        }
    }
}