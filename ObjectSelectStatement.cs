
using DanielCook.Sql.DataAnnotations;
using System.Text;

namespace DanielCook.Sql
{
    public class ObjectSelectStatement<T>
    {
        private ObjectTableMapping<T> _mapping;
        private SqlWhereList _clauses = new SqlWhereList();
        private SqlOrderList _orders = new SqlOrderList();
        private SqlGroupList _groups = new SqlGroupList();

        private bool _distinct = false;
        private int? _top = null;
        
        public ObjectSelectStatement()
        {
            _mapping = new ObjectTableMapping<T>();
        }

        public ObjectSelectStatement<T> Top(int top)
        {
            _top = top;
            return this;
        }        

        public ObjectSelectStatement<T> OrderBy(string fieldName)
        {
            _orders.Add(new SqlOrder(fieldName));
            return this;
        }

        public ObjectSelectStatement<T> GroupBy(string fieldName)
        {
            _groups.Add(new SqlGroup(fieldName));
            return this;
        }

        public ObjectSelectStatement<T> OrderBy(string fieldName, bool ascending)
        {
            _orders.Add(new SqlOrder(fieldName, ascending));
            return this;
        }

        public ObjectSelectStatement<T> Where(string clause)
        {
            _clauses.Add(new SqlWhere(clause));
            return this;
        }

        public ObjectSelectStatement<T> Distinct()
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

            return sb.      
                Append(_mapping.GetColumnList()).
                AppendFormat(" from {0}", _mapping.TableName).         
                Append(_clauses.ToString()).
                Append(_groups.ToString()).
                Append(_orders.ToString()).
                ToString();            
        }
    }
}
