
using challengeFiap.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace challengeFiap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioRepository _repository;

        public RelatoriosController(IRelatorioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("resumo-veterinarios")]
        public async Task<IActionResult> ResumoVeterinarios()
        {
            var resultado = await _repository.ResumoConsultasVetAsync();
            return Ok(resultado);
        }

        [HttpGet("especies")]
        public async Task<IActionResult> RelatorioEspecies()
        {
            var resultado = await _repository.RelatorioEspecieAsync();
            return Ok(resultado);
        }

        [HttpGet("clinicas")]
        public async Task<IActionResult> RelatorioClinicas()
        {
            var resultado = await _repository.ConsultasClinicasAsync();
            return Ok(resultado);
        }

        [HttpGet("consultas-realizadas")]
        public async Task<IActionResult> ConsultasRealizadas()
        {
            var resultado = await _repository.ConsultasRealizadasAsync();
            return Ok(resultado);
        }

        [HttpGet("ocorrencias")]
        public async Task<IActionResult> RelatorioOcorrencias()
        {
            var resultado = await _repository.RelatorioOcorrenciasAsync();
            return Ok(resultado);
        }

        [HttpGet("moradia-animal-tutor")]
        public async Task<IActionResult> MoradiaAnimalTutor()
        {
            var resultado = await _repository.VerificarMoradiaAnimalTutorAsync();
            return Ok(resultado);
        }

        [HttpGet("veterinario/{nomeVet}")]
        public async Task<IActionResult> ConsultasVeterinario(string nomeVet)
        {
            var resultado = await _repository.ProcurarVetConsultasAsync(nomeVet);
            return Ok(resultado);
        }

        [HttpGet("animal/{nomeAnimal}")]
        public async Task<IActionResult> InformacoesAnimal(string nomeAnimal)
        {
            var resultado = await _repository.ProcurarInformacaoAnimalAsync(nomeAnimal);
            return Ok(resultado);
        }

        [HttpGet("medicamentos/{idVet:int}")]
        public async Task<IActionResult> RelatorioMedicamentos(int idVet)
        {
            var resultado = await _repository.ProRelatorioMedicamentoAnimalAsync(idVet);
            return Ok(resultado);
        }
    }
}
