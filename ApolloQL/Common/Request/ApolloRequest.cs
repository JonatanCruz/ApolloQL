using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloQL.Common.Request
{
    /// <summary>
    /// Representa un Query que se puede obtener de un servidor
    /// Para mas informacion <see cref="http://graphql.org/learn/serving-over-http/#post-request"/>
    /// </summary>
    public class ApolloRequest
    {

        /// <summary>
        /// La Consulta
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Si el Query <see cref="Query"/> Contiene multiples nombres de operaciones, esto especifica que operacion puede ser ejecutada
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// Las Variables
        /// </summary>
        public dynamic Variables { get; set; }
    }
}
