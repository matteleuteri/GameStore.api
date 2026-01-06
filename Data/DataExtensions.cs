using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;
public static class DataExtensions
{
    public static void MigrateDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        db.Database.Migrate();
    }
}