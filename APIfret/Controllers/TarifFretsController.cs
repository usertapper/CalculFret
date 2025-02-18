using System;
using System.Diagnostics;
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

    [HttpGet("code")]
    public async Task<ActionResult<IEnumerable<string>>> GetCodeTarifs()
    {
        var codetarifs = await context
        .TarifFrets
        .Select(x => x.Code)
        .ToListAsync();

        return Ok(codetarifs);
    }

    [HttpGet("tarif/ileDepart/ileArrivee")]
    public async Task<ActionResult<IEnumerable<TarifFrets>>> GetTarifs(int ileDepart, int ileArrivee)
    {
        var result = await context.TarifFrets.Where(t => t.IleArriveeId == ileArrivee && t.IleDepartId == ileDepart)
                            .ToListAsync();

        return this.Ok(result);

    }

    [HttpGet("tarif")]
    public async Task<TarifFretDto> GetTarifFret(string codeTarif, int ileDepartId, int ileArriveeId)
    {
        // Vérification des arguments
        if (string.IsNullOrWhiteSpace(codeTarif)) throw new ArgumentNullException(nameof(codeTarif), "Le code tarif est obligatoire");

        // // Vérification ile de départ
        var ileDepart = await context.Iles.FirstOrDefaultAsync(i => i.Id == ileDepartId);
        if (ileDepart == null) throw new ArgumentException($"L'ile {ileDepartId} est introuvable");

        // // Vérification Ile d'arrivée
        var ileArrivee = await context.Iles.FirstOrDefaultAsync(i => i.Id == ileArriveeId);
        if (ileArrivee == null) throw new ArgumentException($"L'ile {ileArriveeId} est introuvable");

        // Recherche du fret
        var tariffret = await context
            .TarifFrets.Where(x =>
            x.Code == codeTarif &&
            x.IleDepartId == ileDepartId &&
            x.IleArriveeId == ileArriveeId)
            .FirstOrDefaultAsync();

        // Si pas de résultat => Erreur !!
        if (tariffret == null)
        {
            throw new Exception($"Tarif introuvable pour le code {codeTarif} le départ {ileDepart.Intitule} et l'arrivée {ileArrivee.Intitule}");
        }

        // Retour du résultat
        var result = new TarifFretDto { Code = tariffret.Code, Methode = tariffret.Case, Montant = tariffret.Montant };

        return result;

    }
    
    [HttpGet("calcul")]

        public async Task<decimal> CalculMontantFret(string codeTarif, int ileDepartId, int ileArriveeId, decimal poids, decimal volume, decimal quantite)
    {
        // Vérification des arguments
        if (string.IsNullOrWhiteSpace(codeTarif)) throw new ArgumentNullException(nameof(codeTarif), "Le code tarif est obligatoire");
        var tarif = await this.GetTarifFret(codeTarif, ileDepartId, ileArriveeId);

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
