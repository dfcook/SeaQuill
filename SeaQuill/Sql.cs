namespace SeaQuill
{
    public static class Sql
    {
        public static SelectStatement Select() =>
            new SelectStatement();

        public static DeleteStatement Delete() =>
            new DeleteStatement();

        public static UpdateStatement Update() =>
            new UpdateStatement();

        public static InsertStatement Insert() =>
            new InsertStatement();
    }
}
