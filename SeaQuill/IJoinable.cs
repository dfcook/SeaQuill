namespace SeaQuill
{
    public interface IJoinable<T>
    {
        T InnerStatement { get; }

        SqlJoinList Joins { get; }
    }

    public static class JoinableExtensions
    {
        #region "Inner Joins"
        public static T Join<T>(this IJoinable<T> joinable, string tableName)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Inner, tableName));
            return joinable.InnerStatement;
        }

        public static T Join<T>(this IJoinable<T> joinable, string tableName, string alias)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Inner, tableName, alias));
            return joinable.InnerStatement;
        }

        public static T Join<T>(this IJoinable<T> joinable, string tableName, string alias, string criteria)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Inner, tableName, alias, criteria));
            return joinable.InnerStatement;
        }
        #endregion

        #region "Left Joins"
        public static T LeftJoin<T>(this IJoinable<T> joinable, string tableName)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Left, tableName));
            return joinable.InnerStatement;
        }

        public static T LeftJoin<T>(this IJoinable<T> joinable, string tableName, string alias)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Left, tableName, alias));
            return joinable.InnerStatement;
        }

        public static T LeftJoin<T>(this IJoinable<T> joinable, string tableName, 
            string alias, string criteria)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Left, tableName, alias, criteria));
            return joinable.InnerStatement;
        }
        #endregion

        #region "Right Joins"
        public static T RightJoin<T>(this IJoinable<T> joinable, string tableName)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Right, tableName));
            return joinable.InnerStatement;
        }

        public static T RightJoin<T>(this IJoinable<T> joinable, string tableName, string alias)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Right, tableName, alias));
            return joinable.InnerStatement;
        }

        public static T RightJoin<T>(this IJoinable<T> joinable, string tableName, 
            string alias, string criteria)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Right, tableName, alias, criteria));
            return joinable.InnerStatement;
        }
        #endregion

        #region "Cross Joins"
        public static T CrossJoin<T>(this IJoinable<T> joinable, string tableName)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Cross, tableName));
            return joinable.InnerStatement;
        }

        public static T CrossJoin<T>(this IJoinable<T> joinable, string tableName, string alias)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Cross, tableName, alias));
            return joinable.InnerStatement;
        }

        public static T CrossJoin<T>(this IJoinable<T> joinable, string tableName, string alias, string criteria)
        {
            joinable.Joins.Add(new SqlJoin(SqlJoinType.Cross, tableName, alias, criteria));
            return joinable.InnerStatement;
        }
        #endregion
    }
}
