using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielCook.Sql
{
    internal class SqlGroupList : List<SqlGroup>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                sb.Append(" group by ");

                foreach (var group in this)
                    sb.Append(group).Append(", ");

                sb.Remove(sb.Length - 2, 2);
            }

            return sb.ToString();
        }
    }

    internal struct SqlGroup
    {
        public string GroupBy { get; }

        public SqlGroup(string groupBy)
        {
            GroupBy = groupBy;
        }

        public override string ToString() => GroupBy;
    }
}
