using System;

namespace APIfret.Entities;

public class TarifsRevatua
{
    public required string Code { get; set; }
    public required string Intitule { get; set; }
    public bool Refrigere { get; set; }
    public bool TauxPriseEnCharge { get; set; }

}
