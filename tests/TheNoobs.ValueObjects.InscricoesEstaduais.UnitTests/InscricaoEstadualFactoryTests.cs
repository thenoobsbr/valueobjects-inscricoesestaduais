using FluentAssertions;
using TheNoobs.ValueObjects.InscricoesEstaduais.Abstractions;
using TheNoobs.ValueObjects.InscricoesEstaduais.UnitTests.Stub;
using Xunit;
using Ufs = TheNoobs.ValueObjects.UnidadesFederativas.UnidadesFederativas;

namespace TheNoobs.ValueObjects.InscricoesEstaduais.UnitTests;

[Trait("Category", "UnitTests")]
[Trait("Class", nameof(InscricaoEstadualFactory))]
public class InscricaoEstadualFactoryTests
{
    [Fact]
    public void Dada_ATentativaDeCriacaoSemUF_Quando_SeRealizaACriacao_Entao_UmaExcecaoDeveSerLancada()
    {
        InscricaoEstadual? inscricaoEstadual = null;
        var action = () => inscricaoEstadual = new DumbInscricaoEstadualStub(null!, "ABC123");

        action
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Value cannot be null. (Parameter 'uf')");

        inscricaoEstadual.Should().BeNull();
    }

    [Fact]
    public void
        Dada_UmaUFNaoSuportadaParaInscricaoEstadual_Quando_EhSolicitadaACriacaoAFactory_Entao_UmaExcecaoDeveSerLancada()
    {
        InscricaoEstadual? inscricaoEstadual = null;
        var action = () => inscricaoEstadual = InscricaoEstadualFactory.Create(Ufs.Exterior, "ISENTO");

        action
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Inscrições estaduais não são suportadas para a UF: EX.");

        inscricaoEstadual.Should().BeNull();
    }
}
