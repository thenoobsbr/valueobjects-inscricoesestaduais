using TheNoobs.ValueObjects.InscricoesEstaduais.Abstractions;
using TheNoobs.ValueObjects.UnidadesFederativas.Abstractions;
using Ufs = TheNoobs.ValueObjects.UnidadesFederativas.UnidadesFederativas;

namespace TheNoobs.ValueObjects.InscricoesEstaduais;

public static class InscricaoEstadualFactory
{
    private const int QUANTIDADE_VALIDADORES = 27;

    private static readonly IDictionary<UnidadeFederativa, Func<string, InscricaoEstadual>> _factoryPool
        = new Dictionary<UnidadeFederativa, Func<string, InscricaoEstadual>>(QUANTIDADE_VALIDADORES)
        {
            {Ufs.Acre, inscricaoEstadual => new InscricaoEstadualAcre(inscricaoEstadual)}
        };

    public static InscricaoEstadual Create(UnidadeFederativa uf, string inscricaoEstadual)
    {
        if (_factoryPool.TryGetValue(uf, out var factory))
        {
            return factory(inscricaoEstadual);
        }

        throw new ArgumentException(
            $"Inscrições estaduais não são suportadas para a UF: {uf.Sigla}.",
            new KeyNotFoundException(uf.Sigla));
    }
}
