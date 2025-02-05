using System;
using APIfret.Data;
using APIfret.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Controllers;

public class IlesController(DataContext context) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Iles>>> GetIles()
    {
        var iles = await context.Iles.ToListAsync();

        return iles;
    }

//endpoint : [HttpGet] (point de terminaison/extrémitée)
    [HttpGet("{id:int}")] // /api/ile
    public async Task<ActionResult<Iles>> GetIle(int id)
    {
        var ile = await context.Iles.FindAsync(id);

        if (ile == null) return NotFound();

        return ile;
    }
}
