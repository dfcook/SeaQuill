using System.Text;

namespace SeaQuill
{
    public class DeleteStatement : IJoinable<DeleteStatement>
    {
        protected readonly SqlTableList _tables = new SqlTableList();
        protected readonly SqlWhereList _clauses = new SqlWhereList();
        protected readonly SqlOrderList _orders = new SqlOrderList();
        protected readonly SqlJoinList _joins = new SqlJoinList();

        protected int? _top = null;
        protected string _targetTable;

        public DeleteStatement InnerStatement => this;

        public SqlJoinList Joins => _joins;

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

        public DeleteStatement From(string tableName, string alias)
        {
            _tables.Add(new SqlTable(tableName, alias));
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

            return sb.
                AppendFormat("{0}", _targetTable).
                Append(_tables.ToString()).
                Append(_joins.ToString()).
                Append(_clauses.ToString()).
                Append(_orders.ToString()).
                ToString();
        }
    }
}
