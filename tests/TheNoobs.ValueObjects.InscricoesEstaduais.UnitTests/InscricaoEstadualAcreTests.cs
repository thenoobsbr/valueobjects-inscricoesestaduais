using FluentAssertions;
using TheNoobs.ValueObjects.InscricoesEstaduais.Abstractions;
using TheNoobs.ValueObjects.InscricoesEstaduais.UnitTests.TestData;
using Xunit;
using Ufs = TheNoobs.ValueObjects.UnidadesFederativas.UnidadesFederativas;

namespace TheNoobs.ValueObjects.InscricoesEstaduais.UnitTests;

[Trait("Category", "UnitTests")]
[Trait("Class", nameof(InscricaoEstadualAcre))]
public class InscricaoEstadualAcreTests
{
    [Theory]
    [InlineData("01.447.078/688-48", "01.447.078/688-48")]
    [InlineData("01.501.297/822-06", "01.501.297/822-06")]
    [InlineData("01.386.448/860-41", "01.386.448/860-41")]
    [InlineData("ISENTO", "isento")]
    public void
        Dada_DuasInscricoesEstaduaisComMesmoValor_Quando_AComparacaoEhRealizada_Entao_OResultadoDeveSerVerdadeiro(
            string value1, string value2)
    {
        var inscricaoEstadual1 = InscricaoEstadualFactory.Create(Ufs.Acre, value1);
        var inscricaoEstadual2 = new InscricaoEstadualAcre(value2);

        inscricaoEstadual1.Uf.Should().Be(Ufs.Acre);
        inscricaoEstadual2.Uf.Should().Be(Ufs.Acre);
        inscricaoEstadual1.Should().Be(inscricaoEstadual2);
        inscricaoEstadual1.Uf.Should().Be(Ufs.Acre);
        inscricaoEstadual1.Value.Should().Be(value1.ToUpper());
        inscricaoEstadual2.Uf.Should().Be(Ufs.Acre);
        inscricaoEstadual2.Value.Should().Be(value2.ToUpper());
    }

    [Theory]
    [MemberData(nameof(AcreTestData.ObterInscricoesEstaduaisInvalidas), MemberType = typeof(AcreTestData))]
    public void Dada_UmaInscricaoEstadualComCaracteresInvalidos_Quando_ForConstruida_Entao_UmaExceçãoDeveraSerDisparada(
        string value, string expectedExceptionMessage)
    {
        InscricaoEstadual? inscricaoEstadual = null;
        Action action = () => inscricaoEstadual = new InscricaoEstadualAcre(value);

        action.Should()
            .Throw<Exception>()
            .WithMessage(expectedExceptionMessage);

        inscricaoEstadual.Should().BeNull();
    }

    [Fact]
    public void Dada_UmaInscricaoEstadualIsenta_Quanto_ForConstruida_Entao_DeveraSerCriadaComSucesso()
    {
        var inscricaoEstadual = new InscricaoEstadualAcre("isento");
        inscricaoEstadual.EhIsenta().Should().BeTrue();
        inscricaoEstadual.Value.Should().Be("ISENTO");
    }
}
