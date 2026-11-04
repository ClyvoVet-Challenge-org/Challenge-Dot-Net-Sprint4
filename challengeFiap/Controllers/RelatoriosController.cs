using challengeFiap.Domain.Interfaces.IRelatorio;
using challengeFiap.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace challengeFiap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Relatórios (Oracle)")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _service;
        private readonly ILogger<RelatoriosController> _logger;

        public RelatoriosController(
            IRelatorioService service,
            ILogger<RelatoriosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// resumo de consultas realizadas por veterinarios
        /// </summary>
        [HttpGet("resumo-veterinarios")]
        public async Task<IActionResult> ResumoVeterinarios()
        {
            _logger.LogInformation("Iniciando relatório de resumo das consultas dos veterinários");

            try
            {
                var resultado = await _service.ResumoConsultasVetAsync();
                _logger.LogInformation("Relatório de resumo das consultas dos veterinários concluído com sucesso");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de resumo das consultas dos veterinários");
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }

        /// <summary>
        /// relatorio sobre as especies de animais cadastrados
        /// </summary>
        [HttpGet("especies")]
        public async Task<IActionResult> RelatorioEspecies()
        {
            _logger.LogInformation("Iniciando relatório de espécies");

            try
            {
                var resultado = await _service.RelatorioEspecieAsync();
                _logger.LogInformation("Relatório de espécies concluído com sucesso");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de espécies");
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }

        /// <summary>
        /// relatorio sobre a clinica e suas consultas
        /// </summary>
        [HttpGet("clinicas")]
        public async Task<IActionResult> RelatorioClinicas()
        {
            _logger.LogInformation("Iniciando relatório de consultas das clínicas");

            try
            {
                var resultado = await _service.ConsultasClinicasAsync();
                _logger.LogInformation("Relatório de consultas das clínicas concluído com sucesso");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de consultas das clínicas");
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }
        /// <summary>
        /// Relatório sobre frequência dos animais
        /// </summary>
        [HttpGet("consultas-realizadas")]
        public async Task<IActionResult> ConsultasRealizadas()
        {
            _logger.LogInformation("Iniciando relatório de consultas realizadas");

            try
            {
                var resultado = await _service.ConsultasRealizadasAsync();
                _logger.LogInformation("Relatório de consultas realizadas concluído com sucesso");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de consultas realizadas");
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }

        /// <summary>
        /// relatorio de Ocorrencias presente no sistema
        /// </summary>
        [HttpGet("ocorrencias")]
        public async Task<IActionResult> RelatorioOcorrencias()
        {
            _logger.LogInformation("Iniciando relatório de ocorrências");

            try
            {
                var resultado = await _service.RelatorioOcorrenciasAsync();
                _logger.LogInformation("Relatório de ocorrências concluído com sucesso");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de ocorrências");
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }

        /// <summary>
        /// relatorio de caso se tutor morar na mesma moradia do animal
        /// </summary>
        [HttpGet("moradia-animal-tutor")]
        public async Task<IActionResult> MoradiaAnimalTutor()
        {
            _logger.LogInformation("Iniciando relatório de moradia do animal e do tutor");

            try
            {
                var resultado = await _service.VerificarMoradiaAnimalTutorAsync();
                _logger.LogInformation("Relatório de moradia do animal e do tutor concluído com sucesso");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de moradia do animal e do tutor");
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }

        /// <summary>
        /// relatorio de consultas realizadas por um veterinario
        /// </summary>
        [HttpGet("veterinario/{nomeVet}")]
        public async Task<IActionResult> ConsultasVeterinario(string nomeVet)
        {
            _logger.LogInformation("Iniciando relatório de consultas do veterinário {NomeVeterinario}", nomeVet);

            try
            {
                var resultado = await _service.ProcurarVetConsultasAsync(nomeVet);
                _logger.LogInformation("Relatório de consultas do veterinário {NomeVeterinario} concluído com sucesso", nomeVet);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de consultas do veterinário {NomeVeterinario}", nomeVet);
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }
        /// <summary>
        /// relatorio sobre a informacao de um animal
        /// </summary>
        [HttpGet("animal/{nomeAnimal}")]
        public async Task<IActionResult> InformacoesAnimal(string nomeAnimal)
        {
            _logger.LogInformation("Iniciando relatório de informações do animal {NomeAnimal}", nomeAnimal);

            try
            {
                var resultado = await _service.ProcurarInformacaoAnimalAsync(nomeAnimal);
                _logger.LogInformation("Relatório de informações do animal {NomeAnimal} concluído com sucesso", nomeAnimal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de informações do animal {NomeAnimal}", nomeAnimal);
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }
        /// <summary>
        /// Relatorio pessoal do veterinario sobre suas consultas realizadas
        /// </summary>
        [HttpGet("veterinario/consultas/{idVet:int}")]
        public async Task<IActionResult> RelatorioVetConsulta(int idVet)
        {
            _logger.LogInformation("Iniciando relatório de consultas do veterinário {IdVeterinario}", idVet);

            try
            {
                var resultado = await _service.RelatorioVetConsultaAsync(idVet);
                _logger.LogInformation("Relatório de consultas do veterinário {IdVeterinario} concluído com sucesso", idVet);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório de consultas do veterinário {IdVeterinario}", idVet);
                return BadRequest($"Erro ao gerar relatório: {ex.Message}");
            }
        }
    }
}
