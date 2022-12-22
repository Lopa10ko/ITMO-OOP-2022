using Microsoft.EntityFrameworkCore;

namespace ApplicationTier.Extensions;

public static class DbSetExtensions
{
    public static async Task<T> GetEntityAsync<T>(this DbSet<T> set, Guid id, CancellationToken cancellationToken)
        where T : class
    {
        T? entity = await set.FindAsync(new object[] { id }, cancellationToken);

        if (entity is null)
            throw new Exception("shit.................");
            /*throw EntityNotFoundException<T>.Create(id);*/

        return entity;
    }
}