using System;
using APIfret.Data;
using APIfret.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Controllers;

public class TarifFretController(DataContext context) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarifFrets>>> GetTarifFret()
    {
        var tariffret = await context.TarifFrets.ToListAsync();

        return tariffret;

    }
        [HttpGet("montants")]
    public async Task<ActionResult<IEnumerable<decimal>>> GetMontants()
    {
        var montants = await context
        .TarifFrets
        .Select(x => x.Montant)
        .ToListAsync();

        return Ok(montants);

    }

        [HttpGet("montant")]
    public async Task<ActionResult<decimal>> GetMontantFret(string codeTarif,int ileDepartId,int ileArriveeId)
    {
        var tariffret = await context
            .TarifFrets.Where(x => 
            x.Code == codeTarif && 
            x.IleDepartId == ileDepartId && 
            x.IleArriveeId == ileArriveeId)
            .Select(x => x.Montant).FirstOrDefaultAsync();

        return tariffret;

    }
}
