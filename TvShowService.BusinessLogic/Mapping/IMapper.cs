namespace TvShowService.BusinessLogic.Mapping
{
    public interface IMapper<TSource, TResult>
    {
        /// <summary>
        /// Maps the given soure to the given result
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        TResult Map(TSource source);
    }
}