using TheNoobs.ValueObjects.InscricoesEstaduais.Internals;

namespace TheNoobs.ValueObjects.InscricoesEstaduais.Extensions;

internal static class StringExtensions
{
    internal static string ToUpperSeIsento(this string inscricaoEstadual)
    {
        return string.Equals(inscricaoEstadual, Constants.ISENTO, StringComparison.OrdinalIgnoreCase)
            ? Constants.ISENTO
            : inscricaoEstadual;
    }
}
