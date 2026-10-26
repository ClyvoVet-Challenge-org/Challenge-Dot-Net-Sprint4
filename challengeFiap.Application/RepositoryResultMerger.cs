namespace challengeFiap.Application;

internal static class RepositoryResultMerger
{
    //esse aqui foi feito
    public static async Task<TEntity> FindByIdAsync<TEntity>(
        Func<Task<TEntity>> oracleLookup,
        Func<Task<TEntity>> mongoLookup)
        where TEntity : class
    {
        TEntity? oracleItem = null;
        try
        {
            oracleItem = await oracleLookup();
        }

        catch (KeyNotFoundException)
        {
        }

        if (oracleItem is not null)
        {
            try
            {
                await mongoLookup();
            }
            catch (KeyNotFoundException)
            {
                throw ;
            }

            return oracleItem;
        }

        return await mongoLookup();
    }

    public static IEnumerable<TEntity> MergeById<TEntity, TId>(
        IEnumerable<TEntity> oracleItems,
        IEnumerable<TEntity> mongoItems,
        Func<TEntity, TId> getId)
        where TId : notnull
    {
        var result = oracleItems.ToList();
        var ids = result.Select(getId).ToHashSet();
        result.AddRange(mongoItems.Where(item => ids.Add(getId(item))));
        return result;
    }
}
