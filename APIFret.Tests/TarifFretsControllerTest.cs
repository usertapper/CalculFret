using APIFret;
using APIfret.Controllers;
using APIfret.Data;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace APIFret.Tests;

public class TarifFretsControllerTest
{
    private string TarifFrigo = "FRIGO";
    private string TarifAutre = "AUTRE";

    private int IleInconnue = -5;
    private int TahitiId = 93;
    private int Temoe = 104;
    

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task CalculFret_Doit_Lever_Erreur_Si_CodeTarif_Null(string codeTarif)
    {
        // Given
        var controller = this.GetController();

        // When
        int ileDepartId = 3;
        int ileArriveeId = 5;
        decimal volume = 8;
        decimal poids = 5;

        // Then
        await Assert.ThrowsAnyAsync<Exception>(() => controller.CalculMontantFret(codeTarif, ileDepartId, ileArriveeId, poids, volume, 0));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task GetTarifFret_Doit_Lever_Exception_Si_CodeTarif_Null(string codeTarif)
    {
        // Given
        var controller = this.GetController();

        // When
        int ileDepartId = 3;
        int ileArriveeId = 5;

        // Then
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.GetTarifFret(codeTarif, ileDepartId, ileArriveeId));
    }

    [Fact]
    public async Task GetTarifFret_Doit_Lever_Exception_Si_IleDepart_Existe_Pas() 
    {
        var controller = this.GetController();

        await Assert.ThrowsAsync<ArgumentException>(() => controller.GetTarifFret(this.TarifFrigo, IleInconnue, Temoe));
    }

    [Fact]
    public async Task GetTarifFret_Doit_Lever_Exception_Si_IleArrivee_Existe_Pas() 
    {
        var controller = this.GetController();

        await Assert.ThrowsAsync<ArgumentException>(() => controller.GetTarifFret(this.TarifFrigo, TahitiId, IleInconnue));
    }

    [Fact]
    public async Task GetTarifFret_Doit_Lever_Exception_Si_Tarif_Existe_Pas() 
    {
        var controller = this.GetController();
        var mooreaId = 59;

        await Assert.ThrowsAsync<Exception>(() => controller.GetTarifFret(this.TarifAutre, TahitiId, mooreaId));
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
        var controller = this.GetController();

        // Execute
        var montant = await controller.CalculMontantFret(codeTarif, ileDepartId, ileArriveeId, poids, volume, quantite);

        // Test
        Assert.Equal(resultat, montant);
        
    }


    private TarifFretController GetController()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite("Data source=fret.db");
        var context = new DataContext(optionsBuilder.Options);
        var controller = new TarifFretController(context);

        return controller;
    }

}
