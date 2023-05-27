
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Genre : hérite de Categorie
    /// </summary>
    public class Genre : Categorie
    {
        /// <summary>
        /// Initialisation d'un nouvel objet Genre
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Genre(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
