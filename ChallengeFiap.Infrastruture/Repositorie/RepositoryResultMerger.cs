namespace challengeFiap.Infrastruture.Repositorie;

//eles sao utilizando mais no get e get all
public static class RepositoryResultMerger
{
    //Como temos dois repositorios presente no nosso sistema, oracle e mongo, 
    // Foi visto como necessario que ambos tivesse uma coneção de dados sendo inseridos em ambos sem repetir.

    //Nessa parte presente é a repsonsavel por fazer o processo de conecção de ambas das parte.
    public static async Task<TEntity> FindByIdAsync<TEntity>(
        Func<Task<TEntity>> oracleLookup,
        Func<Task<TEntity>> mongoLookup
        )
        where TEntity : class
    {
        //Processo dessa parte funcionar como um "try catch" para que caso o oracle não encontre o dado, ele tente buscar no mongo, e vice versa.
        //esse aqui funcionar especificalmente para a buscar dos dados
        TEntity? resultadoOracle = null;
        try
        {
            resultadoOracle = await oracleLookup();
        }
        catch (KeyNotFoundException)
        {

        }

        if (resultadoOracle != null)
        {
            try
            {

                await mongoLookup();

            }
            catch (KeyNotFoundException)
            {
                throw ;
            }

            return resultadoOracle;

        }

        return await mongoLookup();
    }

    //Essa parte é responsavel por fazer a junção de ambos os repositorios, para que não haja duplicidade de dados.
    public static IEnumerable<TEntity> MergeById<TEntity, TId>(

        IEnumerable<TEntity> itensOracle,
        IEnumerable<TEntity> itensMongoDb,
        Func<TEntity, TId> idSelector)
        where TId : notnull
    {
        var result = itensOracle.ToList();

        var ids = result.Select(idSelector).ToHashSet();

        result.AddRange(itensMongoDb.Where(item => ids.Add(idSelector(item))));
        
        return result;
    }
}
