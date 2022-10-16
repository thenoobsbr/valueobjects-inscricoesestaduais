using System.Text.RegularExpressions;
using TheNoobs.Requirements;
using TheNoobs.ValueObjects.Abstractions;
using TheNoobs.ValueObjects.InscricoesEstaduais.Extensions;
using TheNoobs.ValueObjects.InscricoesEstaduais.Internals;
using TheNoobs.ValueObjects.UnidadesFederativas.Abstractions;

namespace TheNoobs.ValueObjects.InscricoesEstaduais.Abstractions;

public abstract class InscricaoEstadual : ValueObject
{
    private protected InscricaoEstadual(UnidadeFederativa uf, string inscricaoEstadual)
    {
        Uf = uf;
        Value = inscricaoEstadual.ToUpperSeIsento();

        // A validação é feita por último propositalmente.
        Validate(uf, inscricaoEstadual);
    }

    public UnidadeFederativa Uf { get; }
    public string Value { get; }

    public bool EhIsenta()
    {
        return EhIsenta(Value);
    }

    protected static string Sanitizar(string inscricaoEstadual)
    {
        inscricaoEstadual = inscricaoEstadual.Trim();
        return Regex.Replace(inscricaoEstadual, @"[^\dpP]", string.Empty);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Uf;
        yield return Value;
    }

    protected abstract void Validate(string inscricaoEstadual);

    private static bool EhIsenta(string inscricaoEstadual)
    {
        return string.Equals(inscricaoEstadual, Constants.ISENTO, StringComparison.OrdinalIgnoreCase);
    }

    private void Validate(UnidadeFederativa uf, string inscricaoEstadual)
    {
        Requirement.To().NotBeNull(uf, () => new ArgumentNullException(nameof(uf)));
        Requirement.To().NotBeEmpty(inscricaoEstadual,
            () => new ArgumentNullException(
                paramName: nameof(inscricaoEstadual),
                "A inscrição estadual não pode ser nula ou vazia."));

        if (EhIsenta(inscricaoEstadual))
        {
            return;
        }

        Requirement.To().NotMatch(
            inscricaoEstadual,
            @"[^\dpP\/\-\.]",
            () => new ArgumentException(
                $"Inscrição estadual ('{inscricaoEstadual}') inválida para a uf '{uf.Sigla}'.",
                paramName: nameof(inscricaoEstadual)));

        Validate(inscricaoEstadual);
    }
}
