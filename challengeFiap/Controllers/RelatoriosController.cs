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

        public RelatoriosController(IRelatorioService service)
        {
            _service = service;
        }

        /// <summary>
        /// resumo de consultas realizadas por veterinarios
        /// </summary>
        [HttpGet("resumo-veterinarios")]
        public async Task<IActionResult> ResumoVeterinarios()
        {
            var resultado = await _service.ResumoConsultasVetAsync();
            return Ok(resultado);
        }

        /// <summary>
        /// relatorio sobre as especies de animais cadastrados
        /// </summary>
        [HttpGet("especies")]
        public async Task<IActionResult> RelatorioEspecies()
        {
            var resultado = await _service.RelatorioEspecieAsync();
            return Ok(resultado);
        }

        /// <summary>
        /// relatorio sobre a clinica e suas consultas
        /// </summary>
        [HttpGet("clinicas")]
        public async Task<IActionResult> RelatorioClinicas()
        {
            var resultado = await _service.ConsultasClinicasAsync();
            return Ok(resultado);
        }
        /// <summary>
        /// Relatório sobre frequência dos animais
        /// </summary>
        [HttpGet("consultas-realizadas")]
        public async Task<IActionResult> ConsultasRealizadas()
        {
            var resultado = await _service.ConsultasRealizadasAsync();
            return Ok(resultado);
        }

        /// <summary>
        /// relatorio de Ocorrencias presente no sistema
        /// </summary>
        [HttpGet("ocorrencias")]
        public async Task<IActionResult> RelatorioOcorrencias()
        {
            var resultado = await _service.RelatorioOcorrenciasAsync();
            return Ok(resultado);
        }

        /// <summary>
        /// relatorio de caso se tutor morar na mesma moradia do animal
        /// </summary>
        [HttpGet("moradia-animal-tutor")]
        public async Task<IActionResult> MoradiaAnimalTutor()
        {
            var resultado = await _service.VerificarMoradiaAnimalTutorAsync();
            return Ok(resultado);
        }

        /// <summary>
        /// relatorio de consultas realizadas por um veterinario
        /// </summary>
        [HttpGet("veterinario/{nomeVet}")]
        public async Task<IActionResult> ConsultasVeterinario(string nomeVet)
        {
            var resultado = await _service.ProcurarVetConsultasAsync(nomeVet);
            return Ok(resultado);
        }
        /// <summary>
        /// relatorio sobre a informacao de um animal
        /// </summary>
        [HttpGet("animal/{nomeAnimal}")]
        public async Task<IActionResult> InformacoesAnimal(string nomeAnimal)
        {
            var resultado = await _service.ProcurarInformacaoAnimalAsync(nomeAnimal);
            return Ok(resultado);
        }
        /// <summary>
        /// Relatorio pessoal do veterinario sobre suas consultas realizadas
        /// </summary>
        [HttpGet("veterinario/consultas/{idVet:int}")]
        public async Task<IActionResult> RelatorioVetConsulta(int idVet)
        {
            var resultado = await _service.RelatorioVetConsultaAsync(idVet);
            return Ok(resultado);
        }
    }
}
