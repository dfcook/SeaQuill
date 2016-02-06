namespace DanielCook.Sql
{
    public static class SeaQuell
    {
        public static SelectStatement Select() =>
            new SelectStatement();

        public static ObjectSelectStatement<T> SelectFor<T>() =>
            new ObjectSelectStatement<T>();
    }
}
