using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielCook.Sql
{
    internal class SqlWhereList : List<SqlWhere>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                sb.Append(" where ");

                foreach (var clause in this)
                    sb.Append(clause.WhereClause).Append(" and ");

                sb.Remove(sb.Length - 5, 5);
            }

            return sb.ToString();
        }
    }

    internal struct SqlWhere
    {
        public string WhereClause { get; }

        public SqlWhere(string clause)
        {
            WhereClause = clause;
        }
    }
}
