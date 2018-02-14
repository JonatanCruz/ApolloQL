using System;
using System.Net.Http;

namespace ApolloQL.Client.Exceptions
{
    /// <summary>
    /// Un Error lanza un evento <see cref="System.Net.Http.HttpResponseMessage"/>
    /// </summary>
    public class ApolloHttpException : Exception
    {

        /// <summary>
        /// El <see cref="System.Net.Http.HttpResponseMessage"/>
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Crea una nueva instancia de <see cref="ApolloHttpException"/>
        /// </summary>
        /// <param name="httpResponseMessage">Lo Inesperado <see cref="System.Net.Http.HttpResponseMessage"/></param>
        public ApolloHttpException(HttpResponseMessage httpResponseMessage) : base(
            $"Unexpected {nameof(System.Net.Http.HttpResponseMessage)} with code: {httpResponseMessage.StatusCode}")
        {
            this.HttpResponseMessage = httpResponseMessage ?? throw new ArgumentNullException(nameof(httpResponseMessage));
        }
    }
}
