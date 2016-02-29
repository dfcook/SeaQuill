using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class SqlTableList : List<SqlTable>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                sb.Append(" from ");

                foreach (var table in this)
                    sb.Append(table).Append(", ");

                sb.Remove(sb.Length - 2, 2);
            }

            return sb.ToString();
        }
    }

    public class SqlSubQueryTable : SqlTable
    {
        public SelectStatement SubQuery { get; }        

        public SqlSubQueryTable(SelectStatement subQuery, string alias) : 
            base(string.Empty, alias)
        {
            SubQuery = subQuery;
        }

        public override string ToString() =>
            $"({SubQuery}) {Alias}";        
    }

    public class SqlTable
    {
        public string TableName { get; }
        public string Alias { get; }

        public SqlTable(string tableName) : this(tableName, string.Empty)
        {
        }

        public SqlTable(string tableName, string alias)
        {
            TableName = tableName;
            Alias = alias;
        }

        public override string ToString() =>
            TableName + (string.IsNullOrEmpty(Alias) ? string.Empty : $" {Alias}");        
    }
}
