﻿namespace SeaQuill.ObjectMapping
{
    using System;
    using System.Linq.Expressions;
    using System.Text;

    public class ObjectSelectStatement<T> : SelectStatement
    {
        private readonly ObjectTableMapping<T> _mapping;

        public ObjectSelectStatement()
        {
            _mapping = new ObjectTableMapping<T>();
        }

        public ObjectSelectStatement<T> Where(Expression<Predicate<T>> predicate)
        {
            _clauses.Add(new SqlWhereExpression<T>(predicate, _mapping));
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
                Append(_mapping.GetColumnList()).
                AppendFormat(" from {0}", _mapping.TableName).
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
