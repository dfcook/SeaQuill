using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielCook.Sql
{
    internal class SqlTableList : List<SqlTable>
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

    internal struct SqlTable
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

        public override string ToString()
        {
            return TableName + (string.IsNullOrEmpty(Alias) ? string.Empty : $" {Alias}");
        }
    }
}
