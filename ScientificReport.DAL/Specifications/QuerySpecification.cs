using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScientificReport.DAL.Specifications
{
    public class QuerySpecification<TEntity, TOrder>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public Expression<Func<TEntity, bool>> PredicateExpression { get; set; }
        public Expression<Func<TEntity, TOrder>> OrderExpression { get; set; }
        public bool Asc { get; set; }
    }
}
