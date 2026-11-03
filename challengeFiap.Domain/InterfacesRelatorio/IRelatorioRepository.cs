
namespace challengeFiap.Domain.Interfaces
{
    public interface IRelatorioRepository
    {
        Task<List<string>> ProcurarVetConsultasAsync(string nomeVet);

        Task<List<string>> ProcurarInformacaoAnimalAsync(string nomeAnimal);

        Task<List<string>> ResumoConsultasVetAsync();

        Task<List<string>> RelatorioEspecieAsync();

        Task<List<string>> ConsultasClinicasAsync();

        Task<List<string>> ConsultasRealizadasAsync();

        Task<List<string>> VerificarMoradiaAnimalTutorAsync();

        Task<List<string>> RelatorioOcorrenciasAsync();

        Task<List<string>> ProRelatorioMedicamentoAnimalAsync(int idVet);
    }
}