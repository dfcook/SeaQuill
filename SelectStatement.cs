using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielCook.Sql
{
    public class SelectStatement
    {
        private SqlTableList _tables = new SqlTableList();
        private SqlFieldList _fields = new SqlFieldList();
        private SqlWhereList _clauses = new SqlWhereList();
        private SqlOrderList _orders = new SqlOrderList();
        private SqlGroupList _groups = new SqlGroupList();
        private bool _distinct = false;
        private int? _top = null;

        public SelectStatement From(string tableName)
        {
            _tables.Add(new SqlTable(tableName));
            return this;
        }

        public SelectStatement Top(int top)
        {
            _top = top;
            return this;
        }

        public SelectStatement From(string tableName, string alias)
        {
            _tables.Add(new SqlTable(tableName, alias));
            return this;
        }

        public SelectStatement Field(string fieldName)
        {
            _fields.Add(new SqlField(fieldName));
            return this;
        }

        public SelectStatement OrderBy(string fieldName)
        {
            _orders.Add(new SqlOrder(fieldName));
            return this;
        }

        public SelectStatement GroupBy(string fieldName)
        {
            _groups.Add(new SqlGroup(fieldName));
            return this;
        }

        public SelectStatement OrderBy(string fieldName, bool ascending)
        {
            _orders.Add(new SqlOrder(fieldName, ascending));
            return this;
        }

        public SelectStatement Where(string clause)
        {
            _clauses.Add(new SqlWhere(clause));
            return this;
        }

        public SelectStatement Distinct()
        {
            _distinct = true;
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("select ");

            if (_top.HasValue)
                sb.AppendFormat("top {0} ", _top.Value);

            if (_distinct)
                sb.Append("distinct ");

            sb.Append(_fields.ToString());
            sb.Append(_tables.ToString());
            sb.Append(_clauses.ToString());
            sb.Append(_groups.ToString());
            sb.Append(_orders.ToString());

            return sb.ToString();
        }
    }
}
