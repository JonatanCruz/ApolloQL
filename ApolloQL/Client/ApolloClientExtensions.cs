using System.Threading.Tasks;
using ApolloQL.Common.Request;
using ApolloQL.Common.Response;

namespace ApolloQL.Client
{
    /// <summary>
    /// Metodos extras para <see cref="ApolloClient"/>
    /// </summary>
    public static class ApolloClientExtensions
    {
        private static readonly ApolloRequest IntrospectionQuery = new ApolloRequest
        {
            Query = @"
				query IntrospectionQuery {
					__schema {
						queryType {
							name
						},
						mutationType {
							name
						},
						subscriptionType {
							name
						},
						types {
							...FullType
						},
						directives {
							name,
							description,
							args {
								...InputValue
							},
							onOperation,
							onFragment,
							onField
						}
					}
				}
				fragment FullType on __Type {
					kind,
					name,
					description,
					fields(includeDeprecated: true) {
						name,
						description,
						args {
							...InputValue
						},
						type {
							...TypeRef
						},
						isDeprecated,
						deprecationReason
					},
					inputFields {
						...InputValue
					},
					interfaces {
						...TypeRef
					},
					enumValues(includeDeprecated: true) {
						name,
						description,
						isDeprecated,
						deprecationReason
					},
					possibleTypes {
						...TypeRef
					}
				}
				fragment InputValue on __InputValue {
					name,
					description,
					type {
						...TypeRef
					},
					defaultValue
				}
				fragment TypeRef on __Type {
					kind,
					name,
					ofType {
						kind,
						name,
						ofType {
							kind,
							name,
							ofType {
								kind,
								name
							}
						}
					}
				}".Replace("\t", "").Replace("\n", "").Replace("\r", ""),
            Variables = null
        };


        /// <summary>
        /// Envia un IntrospectionQuery via Get
        /// </summary>
        /// <param name="apolloClient"> El Cliente</param>
        /// <returns>ApolloResponse</returns>
        public static async Task<ApolloResponse> GetIntrospectionQueryAsync(this ApolloClient apolloClient)
        {
            return await apolloClient.PostAsync(IntrospectionQuery).ConfigureAwait(false);
        }

        /// <summary>
        /// Envia un IntrospectionQuery via Post
        /// </summary>
        /// <param name="ApolloClient">El Cliente</param>
        /// <returns>ApolloResponse</returns>
        public static async Task<ApolloResponse> PostIntrospectionQueryAsync(this ApolloClient ApolloClient) =>
            await ApolloClient.PostAsync(IntrospectionQuery).ConfigureAwait(false);
    }
}
