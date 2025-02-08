using System;

namespace APIfret.Entities;

public class TarifFrets
{
    public required string Code { get; set; }
    public required string IleDepart { get; set; }
    public required string Case { get; set; }
    public required decimal Montant { get; set; }
    public int IleDepartId { get; set; }
    public int IleArriveeId { get; set; }

}
