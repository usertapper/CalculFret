using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIfret.Entities;

public class TarifFret
{
    public required string Code { get; set; }
    public required string Case { get; set; }
    public required decimal Montant { get; set; }

    public Ile IleDepart { get; set; }
    [ForeignKey(nameof(IleDepart))]
    public int IleDepartId { get; set; }

    public Ile IleArrivee { get; set; }
    [ForeignKey(nameof(IleArrivee))]
    public int IleArriveeId { get; set; }

}
