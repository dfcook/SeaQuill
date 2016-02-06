using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielCook.Sql
{
    internal class SqlFieldList : List<SqlField>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                foreach (var field in this)
                    sb.Append(field.FieldName).Append(", ");

                sb.Remove(sb.Length - 2, 2);
            }
            else
            {
                sb.Append("*");
            }

            return sb.ToString();
        }
    }

    internal struct SqlField
    {
        public string FieldName { get; }

        public SqlField(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
