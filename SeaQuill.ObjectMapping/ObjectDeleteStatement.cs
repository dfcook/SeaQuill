
using System;
using System.Linq.Expressions;
using System.Text;

namespace SeaQuill.ObjectMapping
{
    public class ObjectDeleteStatement<T> : DeleteStatement
    {
        private ObjectTableMapping<T> _mapping;
        
        public ObjectDeleteStatement()
        {
            _mapping = new ObjectTableMapping<T>();
        }

        public ObjectDeleteStatement<T> Where(Expression<Predicate<T>> predicate)
        {
            _clauses.Add(new SqlWhereExpression<T>(predicate, _mapping));
            return this;
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder("delete ");

            if (_top.HasValue)
                sb.AppendFormat("top {0} ", _top.Value);

            return sb.
                AppendFormat("{0}", _mapping.TableName).
                Append(_clauses.ToString()).
                Append(_orders.ToString()).
                ToString();            
        }
    }
}
