using APIfret.Entities;
using APIfret.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Controllers;

public class TarifFretController(TarifService tarifService, CalculMontantFretService calculMontantFretService) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarifFret>>> GetTarifFret()
    {
        return this.Ok(await tarifService.GetTarifFret());
    }

    [HttpGet("montants")]
    public async Task<ActionResult<IEnumerable<decimal>>> GetMontants()
    {
        return this.Ok(await tarifService.GetMontants());
    }

    [HttpGet("code")]
    public async Task<ActionResult<IEnumerable<string>>> GetCodeTarifs()
    {
        return this.Ok(await tarifService.GetCodeTarifs());
    }

    [HttpGet("tarif/ileDepartId/ileArriveeId")]
    public async Task<ActionResult<IEnumerable<TarifFret>>> GetTarifs(int ileDepartId, int ileArriveeId)
    {
        return this.Ok(await tarifService.GetTarifs(ileDepartId, ileArriveeId));

    }

    [HttpGet("tarif")]
    public async Task<TarifFretDto> GetTarifFret(string codeTarif, int ileDepartId, int ileArriveeId)
    {
        return await tarifService.GetTarifFret(codeTarif, ileDepartId, ileArriveeId);
    }

    [HttpGet("calcul")]
    public async Task<decimal> CalculMontantFret(string codeTarif, int ileDepartId, int ileArriveeId, decimal poids, decimal volume, decimal quantite)
    {
        return await calculMontantFretService.CalculerAsync(codeTarif,  ileDepartId, ileArriveeId, poids, volume, quantite);
        
    }

}
