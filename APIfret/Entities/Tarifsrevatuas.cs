using System;

namespace APIfret.Entities;

public class Tarifsrevatuas
{
    public required string Code { get; set; }
    public required string Intitule { get; set; }
    public bool Refrigere { get; set; }
    public bool TauxPriseEnCharge { get; set; }

}
