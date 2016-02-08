using SeaQuill.ObjectMapping;

namespace SeaQuill.ObjectMapping
{
    public static class Sql
    {        
        public static ObjectSelectStatement<T> SelectFor<T>() =>
            new ObjectSelectStatement<T>();
    }
}
