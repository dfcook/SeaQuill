namespace SeaQuill.ObjectMapping
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class SqlWhereExpression<T> : SqlWhere
    {
        private class WhereExpressionVisitor : ExpressionVisitor
        {
            protected override Expression VisitMember(MemberExpression node)
            {
                switch (node.Expression.NodeType)
                {
                    case ExpressionType.Constant:
                    case ExpressionType.MemberAccess:
                        {
                            return GetMemberConstant(node);
                        }
                    default:
                        {
                            return base.VisitMember(node);
                        }
                }
            }

            private static ConstantExpression GetMemberConstant(MemberExpression node)
            {
                object value;

                if (node.Member.MemberType == MemberTypes.Field)
                {
                    value = GetFieldValue(node);
                }
                else if (node.Member.MemberType == MemberTypes.Property)
                {
                    value = GetPropertyValue(node);
                }
                else
                {
                    throw new NotSupportedException();
                }

                return Expression.Constant(value, node.Type);
            }
            private static object GetFieldValue(MemberExpression node)
            {
                var fieldInfo = (FieldInfo)node.Member;

                var instance = (node.Expression == null) ? null : 
                    TryEvaluate(node.Expression).Value;

                return fieldInfo.GetValue(instance);
            }

            private static object GetPropertyValue(MemberExpression node)
            {
                var propertyInfo = (PropertyInfo)node.Member;

                var instance = (node.Expression == null) ? null : 
                    TryEvaluate(node.Expression).Value;

                return propertyInfo.GetValue(instance, null);
            }

            private static ConstantExpression TryEvaluate(Expression expression)
            {

                if (expression.NodeType == ExpressionType.MemberAccess)
                {
                    return GetMemberConstant((MemberExpression)expression);
                }

                if (expression.NodeType == ExpressionType.Constant)
                {
                    return (ConstantExpression)expression;
                }

                throw new NotSupportedException();

            }
        }

        private readonly ObjectTableMapping<T> _mapping;

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

            var rhs = body.Right;
            var value = new WhereExpressionVisitor().Visit(rhs);

            return $"{columnName} {GetExpressionType(body.NodeType)} {value}";
        }

        private static string GetExpressionType(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                    return "=";

                case ExpressionType.NotEqual:
                    return "!=";

                case ExpressionType.GreaterThan:
                    return ">";

                case ExpressionType.GreaterThanOrEqual:
                    return ">=";

                case ExpressionType.LessThan:
                    return "<";

                case ExpressionType.LessThanOrEqual:
                    return "<=";
            }

            throw new ArgumentException($"Unrecognized expression type: {type}");
        }

        public SqlWhereExpression(Expression<Predicate<T>> predicate, ObjectTableMapping<T> mapping) :
            base(GenerateWhereClause(predicate, mapping))
        {
            _mapping = mapping;
        }
    }
}
