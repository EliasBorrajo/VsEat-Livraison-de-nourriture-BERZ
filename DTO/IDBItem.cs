namespace DTO
{
    /// <summary>
    /// Interface implémentée par tous les objets stockés dans la base de données.
    /// Utilisé pour instancier un object stocké dans la base de données et faire du polymorphisme.
    /// </summary>
    public interface IDBItem
    {
        /// <summary>
        /// Identifiant unique de l'enregistrement.
        /// </summary>
        int ID { get; }
    }
}
