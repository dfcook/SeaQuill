using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaQuill
{
    public class PagedSelectStatement : IJoinable<PagedSelectStatement>
    {
        protected readonly SqlTableList _tables = new SqlTableList();
        protected readonly SqlFieldList _fields = new SqlFieldList();
        protected readonly SqlWhereList _clauses = new SqlWhereList();
        protected SqlWhere _filter;
        protected readonly SqlOrderList _orders = new SqlOrderList();
        protected readonly SqlJoinList _joins = new SqlJoinList();
        protected readonly SqlGroupList _groups = new SqlGroupList();

        protected readonly int _start;
        protected readonly int _end;

        public PagedSelectStatement(int start, int rowsPerPage)
        {
            _start = start;
            _end = start + rowsPerPage - 1;
        }

        public PagedSelectStatement InnerStatement => this;

        public SqlJoinList Joins => _joins;

        #region "Tables"
        public PagedSelectStatement From(SelectStatement subQuery, string alias)
        {
            _tables.Add(new SqlSubQueryTable(subQuery, alias));
            return this;
        }

        public PagedSelectStatement From(string tableName, string alias)
        {
            _tables.Add(new SqlTable(tableName, alias));
            return this;
        }

        public PagedSelectStatement From(string tableName)
        {
            _tables.Add(new SqlTable(tableName));
            return this;
        }
        #endregion

        #region "Fields"
        public PagedSelectStatement Field(string fieldName) =>
            Field(fieldName, string.Empty);

        public PagedSelectStatement Field(string fieldName, string alias)
        {
            _fields.Add(new SqlField(fieldName, alias));
            return this;
        }

        public PagedSelectStatement Fields(IEnumerable<string> fieldNames)
        {
            _fields.AddRange(fieldNames.Select(x => new SqlField(x, string.Empty)));
            return this;
        }
        #endregion

        public PagedSelectStatement Where(string clause)
        {
            _clauses.Add(new SqlWhere(clause));
            return this;
        }

        public PagedSelectStatement Filter(string clause)
        {
            _filter = new SqlWhere(clause);
            return this;
        }

        public PagedSelectStatement GroupBy(string fieldName)
        {
            _groups.Add(new SqlGroup(fieldName));
            return this;
        }

        #region "Order By"
        public PagedSelectStatement OrderBy(string fieldName)
        {
            _orders.Add(new SqlOrder(fieldName));
            return this;
        }

        public PagedSelectStatement OrderBy(string fieldName, bool ascending)
        {
            _orders.Add(new SqlOrder(fieldName, ascending));
            return this;
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder("select count(1)");

            sb.
                Append(_tables.ToString()).
                Append(_joins.ToString()).
                Append(_clauses.ToString());

            sb.Append($"; select * from (select row_number() over ({_orders}) as row_num,");

            sb.
                Append(_fields.ToString()).
                Append(_tables.ToString()).
                Append(_joins.ToString()).
                Append(_clauses.ToString(_filter)).
                Append(_groups.ToString());

            sb.Append($") t where row_num between {_start} and {_end}");

            return sb.ToString();
        }
    }
}