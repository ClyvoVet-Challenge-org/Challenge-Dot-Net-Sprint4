using System;
using System.Collections.Generic;
using System.Text;

namespace challengeFiap.Domain.Interfaces.IService
{
    public interface IRelatorioService
    {
        Task<List<string>> ProcurarVetConsultasAsync(string nomeVet);
        Task<List<string>> ProcurarInformacaoAnimalAsync(string nomeAnimal);
        Task<List<string>> ResumoConsultasVetAsync();
        Task<List<string>> RelatorioEspecieAsync();
        Task<List<string>> ConsultasClinicasAsync();
        Task<List<string>> ConsultasRealizadasAsync();
        Task<List<string>> VerificarMoradiaAnimalTutorAsync();
        Task<List<string>> RelatorioOcorrenciasAsync();
        Task<List<string>> RelatorioVetConsultaAsync(int idVet);
    }
}
