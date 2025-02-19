using APIFret;
using APIfret.Controllers;
using APIfret.Data;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIfret.Services;


namespace APIFret.Tests;

public class CalculMontantFretTest
{   

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task CalculFret_Doit_Lever_Erreur_Si_CodeTarif_Null(string codeTarif)
    {
        // Given
        var service = this.GetService();

        // When
        int ileDepartId = 3;
        int ileArriveeId = 5;
        decimal volume = 8;
        decimal poids = 5;

        // Then
        await Assert.ThrowsAnyAsync<Exception>(() => service.CalculerAsync(codeTarif, ileDepartId, ileArriveeId, poids, volume, 0));
    }


    [Theory]
    [InlineData("MC", 93, 59, 0.1, 0.2, 1, 609)]
    [InlineData("COPRAH", 93, 59, 0.1, 0.2, 1, 609)]
    [InlineData("HYDROCITERNE", 93, 59, 0.1, 0.2, 1, 609)]
    [InlineData("MC", 93, 59, 1, 2, 1, 1496*2)]
    [InlineData("MC", 93, 59, 2, 1, 1, 1496*2)]
    [InlineData("COPRAH", 93, 59, 10, 0.2, 1, 10*1620)]
    [InlineData("HYDROCITERNE", 93, 59, 0.1, 10, 1, 10*1150)]
    [InlineData("FUTVIDE", 93, 59, 0.1, 0.2, 50, 50*137)]
    public async Task CalculFret_Verification_Valeurs(string codeTarif, int ileDepartId, int ileArriveeId, decimal poids, decimal volume, decimal quantite, decimal resultat)
    {

        // Arrange
        var service = this.GetService();

        // Execute
        var montant = await service.CalculerAsync(codeTarif, ileDepartId, ileArriveeId, poids, volume, quantite);

        // Test
        Assert.Equal(resultat, montant);
        
    }


    private CalculMontantFretService GetService()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite("Data source=fret.db");
        var context = new DataContext(optionsBuilder.Options);
        var tarifService = new TarifService(context);
        return new CalculMontantFretService(tarifService);
    }

}
