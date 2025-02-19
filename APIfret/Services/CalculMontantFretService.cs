using System;
using APIfret.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APIfret.Services;

public class CalculMontantFretService (TarifService tarifService)
{
    public async Task<decimal> CalculerAsync(string codeTarif, int ileDepartId, int ileArriveeId, decimal poids, decimal volume, decimal quantite)
    {
       // Vérification des arguments
        if (string.IsNullOrWhiteSpace(codeTarif)) throw new ArgumentNullException(nameof(codeTarif), "Le code tarif est obligatoire");
        var tarif = await tarifService.GetTarifFret(codeTarif, ileDepartId, ileArriveeId);

        if(tarif == null) throw new Exception($"Le tarif {codeTarif} est introuvable");

        var t = tarif;

        decimal baseCalcule;

        switch(t.Methode) {
            case "Poids":
                baseCalcule = poids;
                break;
            case "Volume":
                baseCalcule = volume;
                break;
            case "PoidsVolume":
                baseCalcule = Math.Max(poids, volume);
                break;
            case "Quantité":
                baseCalcule = quantite;
                break;
            default:
                throw new NotImplementedException($"Calcul du fret par {t.Methode} non traité");
        }

        var result = Math.Round((t.Montant * baseCalcule), 0);

        // Je commence par dire que c'est pas encore implémenté.
        return result < 609 ? 609 : result;
    }
}
