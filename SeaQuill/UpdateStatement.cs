using System.Text;

namespace SeaQuill
{
    public class UpdateStatement : IJoinable<UpdateStatement>
    {
        protected readonly SqlTableList _tables = new SqlTableList();
        protected readonly SqlSetList _sets = new SqlSetList();
        protected readonly SqlWhereList _clauses = new SqlWhereList();        
        protected readonly SqlJoinList _joins = new SqlJoinList();
        
        protected string _targetTable;

        public UpdateStatement InnerStatement => this;

        public SqlJoinList Joins => _joins;

        public UpdateStatement Target(string targetTable)
        {
            _targetTable = targetTable;
            return this;
        }

        public UpdateStatement Set(string fieldName, object value)
        {
            _sets.Add(new SqlSet(fieldName, value));
            return this;
        }

        public UpdateStatement Set(string fieldName, object value, bool doNotQuote)
        {
            _sets.Add(new SqlSet(fieldName, value, doNotQuote));
            return this;
        }

        public UpdateStatement From(string tableName)
        {
            _tables.Add(new SqlTable(tableName));
            return this;
        }

        public UpdateStatement From(string tableName, string alias)
        {
            _tables.Add(new SqlTable(tableName, alias));
            return this;
        }               

        public UpdateStatement Where(string clause)
        {
            _clauses.Add(new SqlWhere(clause));
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("update ");

            return sb.
                AppendFormat("{0}", _targetTable).
                Append(_sets.ToString()).
                Append(_tables.ToString()).
                Append(_joins.ToString()).
                Append(_clauses.ToString()).                
                ToString();
        }
    }
}
