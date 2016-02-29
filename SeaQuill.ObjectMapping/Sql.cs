namespace SeaQuill.ObjectMapping
{
    public static class Sql
    {        
        public static ObjectSelectStatement<T> SelectFor<T>() =>
            new ObjectSelectStatement<T>();

        public static PagedObjectSelectStatement<T> PagedSelectFor<T>(int start, int rowsPerPage) =>
            new PagedObjectSelectStatement<T>(start, rowsPerPage);

        public static ObjectDeleteStatement<T> DeleteFor<T>() =>
            new ObjectDeleteStatement<T>();
    }
}
