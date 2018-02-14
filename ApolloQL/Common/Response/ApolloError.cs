using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloQL.Common.Response
{
    /// <summary>
    /// Muestra el error de <see cref="ApolloResponse"/>
    /// </summary>
    public class ApolloError
    {
        /// <summary>
        /// Mensaje de error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// La ubicacion del error
        /// </summary>
        public ApolloLocation[] Locations { get; set; }
    }
}
