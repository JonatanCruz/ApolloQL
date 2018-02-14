using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloQL.Common.Response;

namespace ApolloQL.Common.Exceptions
{
    /// <summary>
    /// Un evento que contenga un <see cref="Apollo.Graphql.Common.Response.ApolloError"/>
    /// </summary>
    public class ApolloException : Exception
    {

        /// <summary>
        /// El Error
        /// </summary>
        public ApolloError ApolloError { get; }

        /// <summary>
        /// Constructor para ApolloException 
        /// </summary>
        /// <param name="apolloError">El error de consulta</param>
        public ApolloException(ApolloError apolloError) : base(apolloError.Message)
        {
            this.ApolloError = apolloError;
        }
    }
}
