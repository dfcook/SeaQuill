using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class DeleteStatement
    {
        private SqlTableList _tables = new SqlTableList();
        private SqlWhereList _clauses = new SqlWhereList();
        private SqlOrderList _orders = new SqlOrderList();

        private int? _top = null;
        private string _targetTable;

        public DeleteStatement Target(string targetTable)
        {
            _targetTable = targetTable;
            return this;
        }

        public DeleteStatement From(string tableName)
        {
            _tables.Add(new SqlTable(tableName));
            return this;
        }

        public DeleteStatement Top(int top)
        {
            _top = top;
            return this;
        }

        public DeleteStatement OrderBy(string fieldName)
        {
            _orders.Add(new SqlOrder(fieldName));
            return this;
        }

        public DeleteStatement OrderBy(string fieldName, bool ascending)
        {
            _orders.Add(new SqlOrder(fieldName, ascending));
            return this;
        }

        public DeleteStatement Where(string clause)
        {
            _clauses.Add(new SqlWhere(clause));
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("delete ");

            if (_top.HasValue)
                sb.AppendFormat("top {0} ", _top.Value);

            sb.AppendFormat("{0}", _targetTable);

            if (_tables.Any())
            {
                sb.Append(" from ");
                sb.Append(_tables.ToString());
            }

            return sb.
                Append(_clauses.ToString()).
                Append(_orders.ToString()).
                ToString();            
        }
    }
}
