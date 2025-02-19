

using APIfret.Data;
using APIfret.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Services;

public class TarifService(DataContext context)
{
    public async Task<IEnumerable<TarifFret>> GetTarifFret()
    {
        var tariffret = await context.TarifFrets.ToListAsync();

        return tariffret;

    }
    
    public async Task<IEnumerable<decimal>> GetMontants()
    {
        var montants = await context
        .TarifFrets
        .Select(x => x.Montant)
        .ToListAsync();

        return montants;

    }

    public async Task<IEnumerable<string>> GetCodeTarifs()
    {
        var codetarifs = await context
        .TarifFrets
        .Select(x => x.Code)
        .ToListAsync();

        return codetarifs;
    }

    public async Task<IEnumerable<TarifFret>> GetTarifs(int ileDepart, int ileArrivee)
    {
        var result = await context.TarifFrets.Where(t => t.IleArriveeId == ileArrivee && t.IleDepartId == ileDepart)
                            .ToListAsync();

        return result;

    }

    public async Task<TarifFretDto> GetTarifFret(string codeTarif, int ileDepartId, int ileArriveeId)
    {
        
        // Vérification des arguments
        if (string.IsNullOrWhiteSpace(codeTarif)) throw new ArgumentNullException(nameof(codeTarif), "Le code tarif est obligatoire");

        // // Vérification Ile de départ
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
}