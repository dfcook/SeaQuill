using System.Text;

namespace SeaQuill
{
    public class InsertStatement
    {
        protected readonly SqlSetList _sets = new SqlSetList();

        protected string _targetTable;
        protected SelectStatement _fromQuery;

        public InsertStatement Target(string targetTable)
        {
            _targetTable = targetTable;
            return this;
        }

        public InsertStatement Set(string fieldName, object value)
        {
            _sets.Add(new SqlSet(fieldName, value));
            return this;
        }

        public InsertStatement Set(string fieldName, object value, bool doNotQuote)
        {
            _sets.Add(new SqlSet(fieldName, value, doNotQuote));
            return this;
        }

        public InsertStatement FromQuery(SelectStatement select)
        {
            _fromQuery = select;
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("insert ");

            sb.
                AppendFormat("{0} ", _targetTable);

            if (_fromQuery != null)
            {
                sb.Append(_fromQuery);
            }
            else
            {
                sb.AppendFormat("({0}) values ({1})", _sets.GetFieldList(), _sets.GetValueList());
            }

            return sb.ToString();
        }
    }
}
