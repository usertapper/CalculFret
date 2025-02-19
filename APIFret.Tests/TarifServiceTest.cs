using APIFret;
using APIfret.Controllers;
using APIfret.Data;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIfret.Services;


namespace APIFret.Tests;

public class TarifServiceTest
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
    public async Task GetTarifFret_Doit_Lever_Exception_Si_CodeTarif_Null(string codeTarif)
    {
        // Given
        var controller = this.GetService();

        // When
        int ileDepartId = 3;
        int ileArriveeId = 5;

        // Then
        await Assert.ThrowsAsync<ArgumentNullException>(() => controller.GetTarifFret(codeTarif, ileDepartId, ileArriveeId));
    }

    [Fact]
    public async Task GetTarifFret_Doit_Lever_Exception_Si_IleDepart_Existe_Pas() 
    {
        var controller = this.GetService();

        await Assert.ThrowsAsync<ArgumentException>(() => controller.GetTarifFret(this.TarifFrigo, IleInconnue, Temoe));
    }

    [Fact]
    public async Task GetTarifFret_Doit_Lever_Exception_Si_IleArrivee_Existe_Pas() 
    {
        var controller = this.GetService();

        await Assert.ThrowsAsync<ArgumentException>(() => controller.GetTarifFret(this.TarifFrigo, TahitiId, IleInconnue));
    }

    [Fact]
    public async Task GetTarifFret_Doit_Lever_Exception_Si_Tarif_Existe_Pas() 
    {
        var controller = this.GetService();
        var mooreaId = 59;

        await Assert.ThrowsAsync<Exception>(() => controller.GetTarifFret(this.TarifAutre, TahitiId, mooreaId));
    }

    private TarifService GetService()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite("Data source=fret.db");
        var context = new DataContext(optionsBuilder.Options);
        return new TarifService(context);
    }

}
