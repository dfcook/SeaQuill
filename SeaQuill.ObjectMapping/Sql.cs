namespace SeaQuill.ObjectMapping
{
    public static class Sql
    {
        public static ObjectSelectStatement<T> SelectFor<T>() =>
            new ObjectSelectStatement<T>();

        public static ObjectDeleteStatement<T> DeleteFor<T>() =>
            new ObjectDeleteStatement<T>();
    }
}
