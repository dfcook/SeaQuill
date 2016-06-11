using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class SqlSetList : List<SqlSet>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Any())
            {
                sb.Append(" set ");

                foreach (var set in this)
                    sb.Append(set).Append(", ");

                sb.Remove(sb.Length - 2, 2);
            }

            return sb.ToString();
        }

        public string GetFieldList() =>
            string.Join(",", this.Select(x => x.FieldName));

        public string GetValueList() =>
            string.Join(",", this.Select(x => x.FormattedValue));
    }

    public class SqlSet
    {
        public string FieldName { get; }
        public object Value { get; }
        public bool DoNotQuote { get; }

        public SqlSet(string fieldName, object value) :
            this(fieldName, value, false)
        {
        }

        public string FormattedValue => DoNotQuote ? Value.ToString() : $"'{Value}'";

        public SqlSet(string fieldName, object value, bool doNotQuote)
        {
            FieldName = fieldName;
            Value = value;
            DoNotQuote = doNotQuote;

            if (!DoNotQuote)
                DoNotQuote = value.GetType().IsNumericType();
        }

        public override string ToString() =>
            $"{FieldName} = {FormattedValue}";
    }
}
