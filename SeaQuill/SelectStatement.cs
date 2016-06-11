using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class SelectStatement : IJoinable<SelectStatement>
    {
        protected readonly SqlTableList _tables = new SqlTableList();
        protected readonly SqlFieldList _fields = new SqlFieldList();
        protected readonly SqlWhereList _clauses = new SqlWhereList();
        protected readonly SqlOrderList _orders = new SqlOrderList();
        protected readonly SqlJoinList _joins = new SqlJoinList();
        protected readonly SqlGroupList _groups = new SqlGroupList();

        protected SelectStatement _union;
        protected bool _unionAll;
        protected bool _distinct;
        protected int? _top;

        public SelectStatement InnerStatement => this;

        public SqlJoinList Joins => _joins;

        public SelectStatement Top(int top)
        {
            _top = top;
            return this;
        }

        public SelectStatement Distinct()
        {
            _distinct = true;
            return this;
        }

        #region "Tables"
        public SelectStatement From(SelectStatement subQuery, string alias)
        {
            _tables.Add(new SqlSubQueryTable(subQuery, alias));
            return this;
        }

        public SelectStatement From(string tableName, string alias)
        {
            _tables.Add(new SqlTable(tableName, alias));
            return this;
        }

        public SelectStatement From(string tableName)
        {
            _tables.Add(new SqlTable(tableName));
            return this;
        }
        #endregion

        #region "Fields"
        public SelectStatement Field(string fieldName) =>
            Field(fieldName, string.Empty);        

        public SelectStatement Field(string fieldName, string alias)
        {
            _fields.Add(new SqlField(fieldName, alias));
            return this;
        }

        public SelectStatement Fields(IEnumerable<string> fieldNames)
        {
            _fields.AddRange(fieldNames.Select(x => new SqlField(x, string.Empty)));
            return this;
        }

        #endregion
        public SelectStatement Where(string clause)
        {
            _clauses.Add(new SqlWhere(clause));
            return this;
        }

        public SelectStatement GroupBy(string fieldName)
        {
            _groups.Add(new SqlGroup(fieldName));
            return this;
        }

        #region "Order By"
        public SelectStatement OrderBy(string fieldName)
        {
            _orders.Add(new SqlOrder(fieldName));
            return this;
        }

        public SelectStatement OrderBy(string fieldName, bool ascending)
        {
            _orders.Add(new SqlOrder(fieldName, ascending));
            return this;
        }
        #endregion

        public SelectStatement Union(SelectStatement select)
        {
            _union = select;
            return this;
        }

        public SelectStatement UnionAll(SelectStatement select)
        {
            _union = select;
            _unionAll = true;
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("select ");

            if (_top.HasValue)
                sb.AppendFormat("top {0} ", _top.Value);

            if (_distinct)
                sb.Append("distinct ");

            sb.
                Append(_fields.ToString()).
                Append(_tables.ToString()).
                Append(_joins.ToString()).
                Append(_clauses.ToString()).
                Append(_groups.ToString()).
                Append(_orders.ToString());

            if (_union != null)
                sb.
                    Append(_unionAll ? " union all " : " union ").
                    Append(_union);

            return sb.ToString();
        }
    }
}
