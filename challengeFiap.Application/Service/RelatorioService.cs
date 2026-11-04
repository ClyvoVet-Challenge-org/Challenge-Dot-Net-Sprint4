using challengeFiap.Domain.Interfaces.IRelatorio;
using challengeFiap.Domain.Interfaces.IService;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace challengeFiap.Application.Service
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepository _repository;
        private readonly ILogger<RelatorioService> _logger;

        public RelatorioService(IRelatorioRepository repository, ILogger<RelatorioService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<string>> ConsultasClinicasAsync()
        {
            _logger.LogInformation("Gerando relatório de consultas das clínicas.");
            return await _repository.ConsultasClinicasAsync();

        }

        public async Task<List<string>> ConsultasRealizadasAsync()
        {
            _logger.LogInformation("Gerando relatório de consultas realizadas.");
            return await _repository.ConsultasRealizadasAsync();
        }

        public async Task<List<string>> ProcurarInformacaoAnimalAsync(string nomeAnimal)
        {
            if (string.IsNullOrWhiteSpace(nomeAnimal))
            {
                throw new ArgumentException("O nome do animal não pode ser nulo ou vazio.", nameof(nomeAnimal));
            }

            _logger.LogInformation("Procurando informações do animal.");
            return await _repository.ProcurarInformacaoAnimalAsync(nomeAnimal);
        }
       

        public async Task<List<string>> ProcurarVetConsultasAsync(string nomeVet)
        {
            if (string.IsNullOrWhiteSpace(nomeVet))
            {
                throw new ArgumentException("O nome do veterinário não pode ser nulo ou vazio.", nameof(nomeVet));
            }
            _logger.LogInformation("Procurando consultas do veterinário.");
            return await _repository.ProcurarVetConsultasAsync(nomeVet);
        }

        public async Task<List<string>> RelatorioEspecieAsync()
        {
            _logger.LogInformation("Gerando relatório por espécie.");
            return await _repository.RelatorioEspecieAsync();
        }
        

        public async Task<List<string>> RelatorioOcorrenciasAsync()
        {
            _logger.LogInformation("Gerando relatório de ocorrências.");
            return await _repository.RelatorioOcorrenciasAsync();
        }

        public async Task<List<string>> RelatorioVetConsultaAsync(int idVet)
        {
            if (idVet < 0)
            {
                throw new ArgumentException("O ID do veterinário não pode ser negativo.", nameof(idVet));
            }

            _logger.LogInformation("Gerando relatório de consultas do veterinário.");
            return await _repository.RelatorioVetConsultaAsync(idVet);
        }   
        

        public async Task<List<string>> ResumoConsultasVetAsync()
        {
            _logger.LogInformation("Gerando resumo das consultas do veterinário.");
            return await _repository.ResumoConsultasVetAsync();
        }

        public async Task<List<string>> VerificarMoradiaAnimalTutorAsync()
        {
            _logger.LogInformation("Verificando moradia do animal e do tutor.");
            return await _repository.VerificarMoradiaAnimalTutorAsync();
        }
       
    }
}
