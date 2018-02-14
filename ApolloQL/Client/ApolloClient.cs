using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApolloQL.Client.Exceptions;
using ApolloQL.Common.Request;
using ApolloQL.Common.Response;
using Newtonsoft.Json;

namespace ApolloQL.Client
{

    /// <summary>
    /// El Cliente con el acceden los EndPoints de GraphQL
    /// </summary>
    public partial class ApolloClient : IDisposable
    {
        #region Properties

        /// <summary>
        /// Obtiene los Headers que van a ser enviados con cada peticion
        /// </summary>
        public HttpRequestHeaders DefaultRequestHeaders =>
            this._httpClient.DefaultRequestHeaders;

        /// <summary>
        /// El EndPoint que se va a usar
        /// </summary>
        public Uri EndPoint
        {
            get => this.Options.EndPoint;
            set => this.Options.EndPoint = value;
        }

        /// <summary>
        /// Las opciones que van a ser usadas
        /// </summary>
        public ApolloClientOptions Options { get; set; }

        #endregion

        private readonly HttpClient _httpClient;

        #region Constructors

        /// <summary>
        /// Inicializa una nueva instancia
        /// </summary>
        /// <param name="endPoint">El EndPoint que se utilizara</param>
        public ApolloClient(string endPoint) : this(new Uri(endPoint)) { }

        /// <summary>
        /// Inicializa una nueva instancia
        /// </summary>
        /// <param name="endPoint">El EndPoint que se utilizara</param>
        /// <inheritdoc />
        public ApolloClient(Uri endPoint) : this(new ApolloClientOptions { EndPoint = endPoint }) { }

        /// <summary>
        /// Inicializa una nueva instancia
        /// </summary>
        /// <param name="endPoint">El EndPoint que se utilizara</param>
        /// <param name="options">Las Opciones que se utilizaran</param>
        /// <inheritdoc />
        public ApolloClient(string endPoint, ApolloClientOptions options) : this(new Uri(endPoint), options) { }

        /// <summary>
        /// Inicializa una nueva instancia
        /// </summary>
        /// <param name="endPoint">El EndPoint que se utilizara</param>
        /// <param name="options">Las Opciones que se utilizaran</param>
        public ApolloClient(Uri endPoint, ApolloClientOptions options)
        {
            this.Options = options ?? throw new ArgumentNullException(nameof(options));
            this.Options.EndPoint = endPoint ?? throw new ArgumentNullException(nameof(endPoint));

            if (this.Options.JsonSerializerSettings == null) { throw new ArgumentNullException(nameof(this.Options.JsonSerializerSettings)); }
            if (this.Options.HttpMessageHandler == null) { throw new ArgumentNullException(nameof(this.Options.HttpMessageHandler)); }
            if (this.Options.MediaType == null) { throw new ArgumentNullException(nameof(this.Options.MediaType)); }

            this._httpClient = new HttpClient(this.Options.HttpMessageHandler);
        }

        /// <summary>
        /// Inicializa una nueva instancia
        /// </summary>
        /// <param name="options">Las Opciones que se utilizaran</param>
        public ApolloClient(ApolloClientOptions options)
        {
            this.Options = options ?? throw new ArgumentNullException(nameof(options));

            if (this.Options.EndPoint == null) { throw new ArgumentNullException(nameof(this.Options.EndPoint)); }
            if (this.Options.JsonSerializerSettings == null) { throw new ArgumentNullException(nameof(this.Options.JsonSerializerSettings)); }
            if (this.Options.HttpMessageHandler == null) { throw new ArgumentNullException(nameof(this.Options.HttpMessageHandler)); }
            if (this.Options.MediaType == null) { throw new ArgumentNullException(nameof(this.Options.MediaType)); }

            this._httpClient = new HttpClient(this.Options.HttpMessageHandler);
        }

        #endregion

        /// <summary>
        /// Envia una Consulta via Get
        /// </summary>
        /// <param name="query">The Request</param>
        /// <returns>The Response</returns>
        public async Task<ApolloResponse> GetQueryAsync(string query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }

            return await this.GetAsync(new ApolloRequest { Query = query }).ConfigureAwait(false);
        }

        /// <summary>
        /// Envia un <see cref="ApolloRequest"/> via GET
        /// </summary>
        /// <param name="request">The Request</param>
        /// <returns>The Response</returns>
        public async Task<ApolloResponse> GetAsync(ApolloRequest request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Query == null) { throw new ArgumentNullException(nameof(request.Query)); }

            var queryParamsBuilder = new StringBuilder($"query={request.Query}", 3);
            if (request.OperationName != null) { queryParamsBuilder.Append($"&operationName={request.OperationName}"); }
            if (request.Variables != null) { queryParamsBuilder.Append($"&variables={JsonConvert.SerializeObject(request.Variables)}"); }
            using (var httpResponseMessage = await this._httpClient.GetAsync($"{this.Options.EndPoint}?{queryParamsBuilder.ToString()}").ConfigureAwait(false))
            {
                return await this.ReadHttpResponseMessageAsync(httpResponseMessage).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Envia una Consulta via POST
        /// </summary>
        /// <param name="query">The Request</param>
        /// <returns>The Response</returns>
        public async Task<ApolloResponse> PostQueryAsync(string query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }

            return await this.PostAsync(new ApolloRequest { Query = query }).ConfigureAwait(false);
        }

        /// <summary>
        /// Envia un <see cref="ApolloRequest"/> via POST
        /// </summary>
        /// <param name="request">The Request</param>
        /// <returns>The Response</returns>
        public async Task<ApolloResponse> PostAsync(ApolloRequest request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Query == null) { throw new ArgumentNullException(nameof(request.Query)); }

            var apolloString = JsonConvert.SerializeObject(request, this.Options.JsonSerializerSettings);
            using (var httpContent = new StringContent(apolloString, Encoding.UTF8, this.Options.MediaType.MediaType))
            using (var httpResponseMessage = await this._httpClient.PostAsync(this.EndPoint, httpContent).ConfigureAwait(false))
            {
                return await this.ReadHttpResponseMessageAsync(httpResponseMessage).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Libera recursos no utilizados
        /// </summary>
        /// <inheritdoc/>
        public void Dispose() =>
            this._httpClient.Dispose();

        /// <summary>
        /// Lee los <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="httpResponseMessage">The Response</param>
        /// <returns>The ApolloResponse</returns>
        private async Task<ApolloResponse> ReadHttpResponseMessageAsync(HttpResponseMessage httpResponseMessage)
        {
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var jsonSerializer = new JsonSerializer
                {
                    ContractResolver = this.Options.JsonSerializerSettings.ContractResolver
                };
                try
                {
                    return jsonSerializer.Deserialize<ApolloResponse>(jsonTextReader);
                }
                catch (JsonReaderException)
                {
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw;
                    }
                    throw new ApolloHttpException(httpResponseMessage);
                }
            }
        }
    }
}
