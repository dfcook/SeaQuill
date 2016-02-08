namespace SeaQuill.DataAnnotations
{
    public static partial class Sql
    {        
        public static ObjectSelectStatement<T> SelectFor<T>() =>
            new ObjectSelectStatement<T>();
    }
}
