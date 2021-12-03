using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Class d'exceptions personnalisée pour les exceptions de la DAL.
    /// </summary>
    public class ConnectionException : Exception
    {
        /// <summary>
        /// Détails de l'exception.
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// Constructeur permettant de créer une exception personnalisée.
        /// </summary>
        /// <param name="Message">Message de l'exception de base.</param>
        /// <param name="Details">Détails de l'exception.</param>
        public ConnectionException(string Message, string Details) : base(Message)
        {
            this.Details = Details;
        }
    }
}
