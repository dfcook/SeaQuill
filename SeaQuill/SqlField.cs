using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class SqlFieldList : List<SqlField>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                foreach (var field in this)
                {
                    sb.Append(field.FieldName);
                    if (!string.IsNullOrEmpty(field.Alias))
                        sb.AppendFormat(" as {0}", field.Alias);
                    sb.Append(", ");
                }

                sb.Remove(sb.Length - 2, 2);
            }
            else
            {
                sb.Append("*");
            }

            return sb.ToString();
        }
    }

    public class SqlField
    {
        public string FieldName { get; }
        public string Alias { get; }

        public SqlField(string fieldName, string alias)
        {
            FieldName = fieldName;
            Alias = alias;
        }
    }
}
