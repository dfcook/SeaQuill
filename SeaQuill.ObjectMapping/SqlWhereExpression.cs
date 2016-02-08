using System;
using System.Linq;
using System.Linq.Expressions;

namespace SeaQuill.ObjectMapping
{    
    public class SqlWhereExpression<T> : SqlWhere
    {
        private ObjectTableMapping<T> _mapping;

        private static string GenerateWhereClause(Expression<Predicate<T>> predicate,
            ObjectTableMapping<T> mapping)
        {
            var body = predicate.Body as BinaryExpression;
            if (body == null)
                throw new ArgumentException("The predicate supplied must be a binary expression");

            var lhs = body.Left as MemberExpression;
            if (lhs == null)
                throw new ArgumentException("The LHS of the predicate must be a member expression");
            
            var columnName = mapping.
                PropertyMappings.
                Single(x => x.PropertyName.Equals(lhs.Member.Name, 
                    StringComparison.OrdinalIgnoreCase)).
                ColumnName;

            var rhs = body.Right as ConstantExpression;
            if (rhs == null)
                throw new ArgumentException("The RHS of the predicate must be a constant value");

            var quote = rhs.Type.IsNumericType() ? string.Empty : "'";
            
            return $"{columnName} = {quote}{rhs.Value}{quote}";
        }

        public SqlWhereExpression(Expression<Predicate<T>> predicate, ObjectTableMapping<T> mapping) : 
            base(GenerateWhereClause(predicate, mapping))
        {
            _mapping = mapping;
        }
    }
}
