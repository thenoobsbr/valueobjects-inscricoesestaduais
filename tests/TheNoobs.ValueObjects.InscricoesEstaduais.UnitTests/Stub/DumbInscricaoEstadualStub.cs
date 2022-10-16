using System.Diagnostics.CodeAnalysis;
using TheNoobs.ValueObjects.InscricoesEstaduais.Abstractions;
using TheNoobs.ValueObjects.UnidadesFederativas.Abstractions;

namespace TheNoobs.ValueObjects.InscricoesEstaduais.UnitTests.Stub;

[ExcludeFromCodeCoverage]
public class DumbInscricaoEstadualStub : InscricaoEstadual
{
    internal DumbInscricaoEstadualStub(UnidadeFederativa uf, string inscricaoEstadual) : base(uf, inscricaoEstadual)
    {
    }

    protected override void Validate(string inscricaoEstadual)
    {
        // Do nothing.
    }
}
