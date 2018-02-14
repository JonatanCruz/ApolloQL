using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloQL.Common.Response
{
    public class ApolloLocation : IEquatable<ApolloLocation>
    {
        #region

        /// <summary>
        /// La Columna
        /// </summary>
        public uint Column { get; set; }

        /// <summary>
        /// La Linea
        /// </summary>
        public uint Line { get; set; }

        #endregion

        #region IEquatable

        /// <summary>
        /// Regresa el codigo hash de la instancia
        /// </summary>
        /// <returns>The HashCode</returns>
        public override int GetHashCode() =>
            this.Column.GetHashCode() ^ this.Line.GetHashCode();

        /// <summary>
        /// Regresa el valor que indica que si una instancia es igual a un objeto en especifico
        /// </summary>
        /// <param name="obj">El Objeto para comparar con esta instancia</param>
        /// <returns>Cierto si obj es una instancia de <see cref="ApolloLocation"/> e iguala el valor de la instancia, de otra manera, falso</returns>
        public override bool Equals(object obj)
        {
            if (obj is ApolloLocation)
            {
                return Equals(obj as ApolloLocation);
            }
            return false;
        }

        /// <summary>
        /// Regresa el valor que indica si la instancia es igual a un objeto en especifico
        /// </summary>
        /// <param name="other">El Objeto a para comparar con esta instancia</param>
        /// <returns>Cierto si obj es una instancia de <see cref="ApolloLocation"/> e iguala el valor de la instancia, de otra manera, falso</returns>
        public bool Equals(ApolloLocation other) =>
            Equals(this.Column, other.Column) && Equals(this.Line, other.Line);

        /// <summary>
        /// Prueba si dos <see cref="ApolloLocation"/> instancias son equivalentes
        /// </summary>
        /// <param name="left"> La <see cref="ApolloLocation"/> instancia que está a la izquierda del operador de igualdad</param>
        /// <param name="rigth">La <see cref="ApolloLocation"/> instancia que está a la derecha del operador de igualdad</param>
        /// <returns>verdadero si izquierda y derecha son iguales; de lo contrario, falso</returns>
        public static bool operator ==(ApolloLocation left, ApolloLocation rigth) =>
            left.Equals(rigth);

        /// <summary>
        /// Prueba si dos <see cref="ApolloLocation"/> instancias no son equivalentes
        /// </summary>
        /// <param name="left">La <see cref="ApolloLocation"/> instancia que no está a la izquierda del operador de igualdad</param>
        /// <param name="rigth">La <see cref="ApolloLocation"/> instancia que no está a la derecha del operador de igualdad</param>
        /// <returns>verdadero si la izquierda y la derecha son desiguales; de lo contrario, falso</returns>
        public static bool operator !=(ApolloLocation left, ApolloLocation rigth) =>
            !(left == rigth);

        #endregion
    }
}
