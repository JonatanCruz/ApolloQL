using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloQL.Common.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class ApolloResponse
    {

        /// <summary>
        /// Los datos de la respuesta
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Errores si ocurren
        /// </summary>
        public ApolloError[] Errors { get; set; }

        /// <summary>
        /// Obten un campo de <see cref="Data"/> como Type
        /// </summary>
        /// <typeparam name="Type">El Type esperado</typeparam>
        /// <param name="fieldName">El nombre del campo</param>
        /// <returns>El campo de dato como objeto</returns>
        public Type GetDataFieldAs<Type>(string filename)
        {
            var value = this.Data.GetValue(filename);
            return value.ToObject<Type>();
        }
    }
}
