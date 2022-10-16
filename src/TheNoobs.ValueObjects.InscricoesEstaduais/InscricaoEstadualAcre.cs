using TheNoobs.Requirements;
using TheNoobs.ValueObjects.InscricoesEstaduais.Abstractions;
using Ufs = TheNoobs.ValueObjects.UnidadesFederativas.UnidadesFederativas;

namespace TheNoobs.ValueObjects.InscricoesEstaduais;

/// <summary>
///     Inscrição estadual do ACRE
/// </summary>
/// <remarks>http://www.sintegra.gov.br/Cad_Estados/cad_AC.html</remarks>
public class InscricaoEstadualAcre : InscricaoEstadual
{
    private const int TAMANHO_DIGITO_VERIFICADOR = 2;

    private const int TAMANHO_INSCRICAO_ESTADUAL = 13;

    public InscricaoEstadualAcre(string inscricaoEstadual)
        : base(Ufs.Acre, inscricaoEstadual)
    {
    }

    protected override void Validate(string inscricaoEstadual)
    {
        var inscricao = Sanitizar(inscricaoEstadual);

        Requirement.To().Match(
            inscricao,
            @"^[0-9]*$",
            () => new ArgumentException($"Inscrição estadual ('{inscricaoEstadual}') inválida para a uf '{Uf.Sigla}'.",
                nameof(inscricaoEstadual)));

        Requirement.To().BeTrue(
            inscricao.Length == TAMANHO_INSCRICAO_ESTADUAL,
            () => new ArgumentException(
                $"Inscrição estadual ('{inscricaoEstadual}') inválida para a uf '{Uf.Sigla}'. A inscrição estadual deve ser {TAMANHO_INSCRICAO_ESTADUAL} dígitos.",
                nameof(inscricaoEstadual)));

        Requirement.To().BeTrue(
            InscricaoEstadualIniciadaCom01(inscricao),
            () => new ArgumentException("A inscrição estadual do Acre precisa ser inciada com '01'.",
                nameof(inscricaoEstadual)));

        Requirement.To().BeTrue(
            InscricaoEstadualComPosicao3E4Valida(inscricao),
            () => new ArgumentException("A inscrição estadual do Acre precisa ser possuir '00' na posição 3 e 4.",
                nameof(inscricaoEstadual)));

        Requirement.To().BeTrue(
            DigitoVerificadorEstaValido(inscricao),
            () => new ArgumentException(
                $"Inscrição estadual ('{inscricaoEstadual}') inválida para a uf '{Uf.Sigla}'.",
                nameof(inscricaoEstadual)));
    }

    private static string CalcularDigitoVerificador(string inscricaoEstadualBase)
    {
        var soma = 0;
        var peso = inscricaoEstadualBase.Length - 7;
        for (var i = 0; i < inscricaoEstadualBase.Length; i++)
        {
            var digito = int.Parse(inscricaoEstadualBase.Substring(i, 1));
            if (peso == 1)
            {
                peso = 9;
            }

            soma += digito * peso;
            peso--;
        }

        var resto = soma % 11;
        return resto < 2 ? "0" : Convert.ToString(11 - resto);
    }

    private static bool DigitoVerificadorEstaValido(string inscricaoEstadual)
    {
        var inscricaoEstadualCalculada = inscricaoEstadual
            .Trim()
            .Substring(0, TAMANHO_INSCRICAO_ESTADUAL - TAMANHO_DIGITO_VERIFICADOR);

        inscricaoEstadualCalculada += CalcularDigitoVerificador(inscricaoEstadualCalculada);
        inscricaoEstadualCalculada += CalcularDigitoVerificador(inscricaoEstadualCalculada);

        return inscricaoEstadualCalculada == inscricaoEstadual;
    }

    private static bool InscricaoEstadualComPosicao3E4Valida(string inscricaoEstadual)
    {
        return inscricaoEstadual.Substring(2, 2) != "00";
    }

    private static bool InscricaoEstadualIniciadaCom01(string inscricaoEstadual)
    {
        return inscricaoEstadual.StartsWith("01");
    }
}
