namespace TheNoobs.ValueObjects.InscricoesEstaduais.UnitTests.TestData;

public static class AcreTestData
{
    public static IEnumerable<object[]> ObterInscricoesEstaduaisInvalidas()
    {
        yield return new object[]
        {
            "inscricao", 
            "Inscrição estadual ('inscricao') inválida para a uf 'AC'. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "", 
            "A inscrição estadual não pode ser nula ou vazia. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "01.250.743z716-40",
            "Inscrição estadual ('01.250.743z716-40') inválida para a uf 'AC'. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "01.250.743|716-40",
            "Inscrição estadual ('01.250.743|716-40') inválida para a uf 'AC'. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            @"01.250.743\716-40",
            "Inscrição estadual ('01.250.743\\716-40') inválida para a uf 'AC'. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "01.447.078p688-48",
            "Inscrição estadual ('01.447.078p688-48') inválida para a uf 'AC'. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "01.501.297/822p06",
            "Inscrição estadual ('01.501.297/822p06') inválida para a uf 'AC'. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "01.386p448/860-41",
            "Inscrição estadual ('01.386p448/860-41') inválida para a uf 'AC'. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "01.003.858/650-75",
            "A inscrição estadual do Acre precisa ser possuir '00' na posição 3 e 4. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "05.173.858/650-75",
            "A inscrição estadual do Acre precisa ser inciada com '01'. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "05.173.858/50-75",
            "Inscrição estadual ('05.173.858/50-75') inválida para a uf 'AC'. A inscrição estadual deve ser 13 dígitos. (Parameter 'inscricaoEstadual')"
        };
        yield return new object[]
        {
            "01.173.858/650-66",
            "Inscrição estadual ('01.173.858/650-66') inválida para a uf 'AC'. (Parameter 'inscricaoEstadual')"
        };

    }
}
