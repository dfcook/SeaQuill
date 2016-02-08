namespace SeaQuill
{
    public static partial class Sql
    {
        public static SelectStatement Select() =>
            new SelectStatement();

        public static DeleteStatement Delete() =>
            new DeleteStatement();        
    }
}
