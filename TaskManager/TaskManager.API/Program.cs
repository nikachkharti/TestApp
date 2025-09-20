using TaskManager.API.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddControllers();
        builder.AddSwagger();
        builder.AddRepository();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        if (builder.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}