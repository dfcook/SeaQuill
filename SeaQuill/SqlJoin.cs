using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class SqlJoinList : List<SqlJoin>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                foreach (var join in this)
                    sb.Append(join);
            }

            return sb.ToString();
        }
    }

    public enum SqlJoinType
    {
        Inner = 0,
        Left = 1,
        Right = 2,
        Cross = 3
    }

    public class SqlJoin
    {
        private readonly SqlTable _table;


        public SqlJoinType JoinType { get; }
        public string TableName => _table.TableName;
        public string Alias => _table.Alias;
        public string Criteria { get; }

        public SqlJoin(SqlJoinType joinType, string tableName) :
            this(joinType, tableName, string.Empty)
        {
        }

        public SqlJoin(SqlJoinType joinType, string tableName, string alias) :
            this(joinType, tableName, alias, string.Empty)
        {
        }

        public SqlJoin(SqlJoinType joinType, string tableName, string alias, string criteria)
        {
            JoinType = joinType;
            _table = new SqlTable(tableName, alias);
            Criteria = criteria;
        }

        public override string ToString()
        {
            var joinType = "inner";

            switch (JoinType)
            {
                case SqlJoinType.Left:
                    joinType = "left outer";
                    break;

                case SqlJoinType.Right:
                    joinType = "right outer";
                    break;

                case SqlJoinType.Cross:
                    joinType = "cross";
                    break;
            }

            var sql = $" {joinType} join {_table}";
            if (!string.IsNullOrEmpty(Criteria))
                sql = $"{sql} on {Criteria}";

            return sql;
        }
    }
}
