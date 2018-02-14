using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApolloQL.Client
{
    /// <summary>
    /// Las Opciones que <see cref="ApolloClient"/> puede usar
    /// </summary>
    public class ApolloClientOptions
    {

        /// <summary>
        /// El EndPoint o URL que va a usar
        /// </summary>
        public Uri EndPoint { get; set; }

        /// <summary>
        /// El <see cref="Newtonsoft.Json.JsonSerializerSettings"/> que va a ser usado
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { set; get; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        /// El <see cref="System.Net.Http.HttpMessageHandler"/> que va a ser usado
        /// </summary>
        public HttpMessageHandler HttpMessageHandler { get; set; } = new HttpClientHandler();

        /// <summary>
        /// El <see cref="MediaTypeHeaderValue"/> que va a ser mandado en el POST
        /// </summary>
        public MediaTypeHeaderValue MediaType { get; set; } = new MediaTypeHeaderValue("application/json"); // "application/graphql" y "application/x-www-form-urlencoded" tambien puede
    }
}
