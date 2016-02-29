using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class SqlWhereList : List<SqlWhere>
    {
        public string ToString(SqlWhere filter)
        {
            var sb = new StringBuilder();

            if (this.Any() || filter != null)
            {
                sb.Append(" where ");

                foreach (var clause in this)
                    sb.Append(clause).Append(" and ");

                if (filter != null)
                    sb.Append(filter).Append(" and ");

                sb.Remove(sb.Length - 5, 5);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                sb.Append(" where ");

                foreach (var clause in this)
                    sb.Append(clause).Append(" and ");

                sb.Remove(sb.Length - 5, 5);
            }

            return sb.ToString();
        }
    }

    public class SqlWhere
    {
        public string WhereClause { get; }

        public SqlWhere(string clause)
        {
            WhereClause = clause;
        }

        public override string ToString() => WhereClause;
    }
}
