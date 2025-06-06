using System;
using APIfret.Data;
using APIfret.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Controllers;

public class TarifsRevatuasController(DataContext context) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarifsRevatua>>> GetTarifsrevatuas()
    {
        var tarifsrevatuas = await context.Tarifsrevatuas.ToListAsync();

        return tarifsrevatuas;
    }
}
