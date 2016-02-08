using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class SqlOrderList : List<SqlOrder>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                sb.Append(" order by ");

                foreach (var order in this)
                    sb.Append(order).Append(", ");

                sb.Remove(sb.Length - 2, 2);
            }

            return sb.ToString();
        }
    }

    public class SqlOrder
    {
        public bool Ascending { get; }

        public string OrderBy { get; }

        public SqlOrder(string orderBy) : this(orderBy, true)
        {
        }

        public SqlOrder(string orderBy, bool ascending)
        {
            OrderBy = orderBy;
            Ascending = ascending;
        }

        public override string ToString()
        {
            var asc = Ascending ? "asc" : "desc";
            return $"{OrderBy} {asc}";
        }
    }
}
