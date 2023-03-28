using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model.Tests
{
    /// <summary>
    /// Classe de test unitaire pour la classe métier Utilisateur
    /// </summary>
    [TestClass()]
    public class UtilisateurTests
    {
        private const int idUser = 47;
        private const int idService = 4;
        private const string login = "login";
        private const string password = "password123";
        private static readonly Utilisateur utilisateur = new Utilisateur(idUser, idService, login, password);

        /// <summary>
        /// Test sur le constructeur de la classe Utilisateur
        /// </summary>
        [TestMethod()]
        public void UtilisateurTest()
        {
            Assert.AreEqual(idUser, utilisateur.IdUser, "devrait réussir : idUser valorisé");
            Assert.AreEqual(idService, utilisateur.IdService, "devrait réussir : idService valorisé");
            Assert.AreEqual(login, utilisateur.Login, "devrait réussir : login valorisé");
            Assert.AreEqual(password, utilisateur.Password, "devrait réussir : password valorisé");
        }
    }
}