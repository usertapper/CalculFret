using System;
using APIfret.Data;
using APIfret.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Controllers;

public class IlesController(DataContext context) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IleDto>>> GetIles()
    {
        var iles = await context.Iles.Select(i => new IleDto {
            Id = i.Id,
            Intitule = i.Intitule
        })
        .ToListAsync();

        return iles;
    }

//endpoint : [HttpGet] (point de terminaison/extrémitée)
    [HttpGet("{id:int}")] // /api/ile
    public async Task<ActionResult<IleDto>> GetIle(int id)
    {
        var ile = await context.Iles.FindAsync(id);

        if (ile == null) return NotFound();

        return new IleDto { Id = ile.Id, Intitule = ile.Intitule };
    }
}
