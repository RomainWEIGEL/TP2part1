using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP2part1.Controllers;
using TP2part1.Models.EntityFramework;



namespace TP2part1.Tests.Controllers
{
    [TestClass]
    public class SeriesControllerTests
    {
        private SeriesController _controller;
        private Tp2part1Context _context;

        public SeriesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<Tp2part1Context>()
                .UseNpgsql("TP2part1"); // Chaine de connexion à mettre dans les ( )
            _context = new Tp2part1Context(builder.Options);
            _controller = new SeriesController(_context);
        }

        [TestMethod]
        public async Task GetSeriesTest()
        {


            // Act : Appelez la méthode à tester
            var actionResult = await _controller.GetSeries();
            var result = actionResult.Value.Where(s => s.Serieid > 120).ToList();
            // Assert : Vérifiez que le résultat correspond à ce qui est attendu
            CollectionAssert.AllItemsAreNotNull(result);
        }

        [TestMethod]
        public async Task GetSerieSuccessTest()
        {
            // Arrange : ID de série existant
            int existingSerieId = 2;

            // Act : Appelez la méthode à tester
            var result = await _controller.GetSerie(existingSerieId);

            // Assert : Vérifiez que la série est retournée avec succès
            Assert.IsNotNull(result.Value);
        }

        [TestMethod]
        public async Task GetSerieNotFoundTest()
        {
            // Arrange : ID de série inexistant
            int nonExistingSerieId = -1;

            // Act : Appelez la méthode à tester
            var result = await _controller.GetSerie(nonExistingSerieId);

            // Assert : Vérifiez que la série n'est pas trouvée
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task DeleteSerieTest()
        {
            // Arrange : ID de série à supprimer
            int serieIdToDelete = 1;

            // Act : Appelez la méthode à tester
            var result = await _controller.Delete(serieIdToDelete);

            // Assert : Vérifiez que la série est supprimée avec succès
            Assert.IsTrue(result is NoContentResult); // Assuming you're returning NoContent on successful deletion
        }


        // Écrire d'autres tests pour les autres actions du contrôleur comme Details, Create, Edit, Delete, GetSerie, PostSerie, PutSerie
    }
}
