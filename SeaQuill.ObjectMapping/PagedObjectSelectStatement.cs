using System.Linq;
using System.Text;

namespace SeaQuill.ObjectMapping
{
    public class PagedObjectSelectStatement<T> : PagedSelectStatement
    {
        private ObjectTableMapping<T> _mapping;
        
        public PagedObjectSelectStatement(int start, int rowsPerPage) : 
            base(start, rowsPerPage)
        {
            _mapping = new ObjectTableMapping<T>();
        }        
        
        public override string ToString()
        {
            var sb = new StringBuilder("select count(1)");

            var orders = _orders.Any() ? _orders : new SqlOrderList(new[] {
                    new SqlOrder(_mapping.IdField)
                });

            sb.
                AppendFormat(" from {0}", _mapping.TableName).
                Append(_joins.ToString()).
                Append(_clauses.ToString());

            sb.Append($"; select * from (select row_number() over ({orders}) as row_num,");

            sb.
                Append(_mapping.GetColumnList()).
                AppendFormat(" from {0}", _mapping.TableName).
                Append(_joins.ToString()).
                Append(_clauses.ToString(_filter)).
                Append(_groups.ToString());

            sb.Append($") t where row_num between {_start} and {_end}");

            return sb.ToString();
        }
    }
}
