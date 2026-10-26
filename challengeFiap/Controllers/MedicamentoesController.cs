using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.InterfacesService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class MedicamentoesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<MedicamentoesController> _logger;
    private readonly IMedicamentoService _medicamentoService;
    public MedicamentoesController(AppDbContext context, ILogger<MedicamentoesController> logger, IMedicamentoService medicamentoService)
    {
        _context = context;
        _logger = logger;
        _medicamentoService = medicamentoService;
    }

    // GET: api/Medicamento

    /// <summary>
    /// Relatorio de medicamento: 
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>relatorio: </returns>
    [HttpGet]
    [Route("relatorio/medicamento", Name = "GetAllMedicamentos")]

    public async Task<ActionResult<IEnumerable<MedicamentoDTO>>> GetAllMedicamento()
    {
        _logger.LogInformation("Iniciando a busca de medicamentos");

        try
        {
            var relatorioMedicamento = await _medicamentoService.GetAllMedicamentoAsync();
            var medicamentoDtos = relatorioMedicamento
                .Select(medicamento => CreateMedicamentoDto(medicamento))
                .ToList();

            _logger.LogInformation("Busca de medicamentos concluída com sucesso");
            return Ok(medicamentoDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca de medicamentos");
            return BadRequest($"Erro em buscar: {ex.Message}");
        }
    }

    // GET: api/Medicamento/5

    /// <summary>
    /// Relatorio medicamento pelo id
    /// </summary>
    /// <param name="id_medicamento">Buscar pelo id: </param>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <returns>Relatorio</returns>
    [HttpGet]
    [Route("relatorio/medicamento/{id_medicamento:int}", Name = "GetMedicamentosById")]
    public async Task<ActionResult<MedicamentoDTO>> GetMedicamento(int id_medicamento)
    {
        _logger.LogInformation("Iniciando a busca do medicamento");

        try
        {
            try
            {
                var medicamento = await _medicamentoService.GetMedicamentoIdAsync(id_medicamento);
                _logger.LogInformation("Medicamento foi encontrado com sucesso");
                var medicamentoDTO = CreateMedicamentoDto(medicamento);
                return Ok(medicamentoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "medicamento não encontrado para o ID");
                return NotFound("Id de medicamento não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar medicamento");
            return BadRequest($"Erro em buscar: {ex.Message}");
        }

    }

    // PUT: api/Medicamento/5

    /// <summary>
    /// Atualizar medicamento
    /// </summary>
    /// <param name="id_medicamento">Id para pode atualizar: </param>
    /// <param name="medicamento">Dados para serem inseridos</param>
    /// <response code="200">Medicamento atualizado</response>
    /// <response code="400">Erro na requisição</response>
    /// <response code="404">Medicamento não encontrado</response>
    /// <returns>Atualizar: </returns>
    [HttpPut]
    [Route("atualizar/medicamento/{id_medicamento:int}", Name = "UpdateMedicamento")]
    public async Task<IActionResult> PutMedicamento(int id_medicamento, Medicamento medicamento)
    {
        _logger.LogInformation("Iniciando a atualização do medicamento");

        if (id_medicamento != medicamento.Id_medicamento)
        {
            _logger.LogWarning("ID do medicamento informado na rota é diferente do ID enviado no objeto");
            return BadRequest("Id medicamento não está incorreto");
        }

        try
        {
            var medicamentoAtualizado = await _medicamentoService.UpdateMedicamentoAsync(id_medicamento, medicamento);

            _logger.LogInformation("Medicamento atualizado com sucesso");
            return Ok(medicamentoAtualizado);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MedicamentoExists(id_medicamento))
            {
                _logger.LogWarning("Medicamento não encontrado durante a atualização");
                return NotFound("Medicamento não encontrado");
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar medicamento");
            return BadRequest($"Erro em atualizar medicamento: {ex.Message}");
        }
    }

    private bool MedicamentoExists(int id_medicamento)
    {
        return _context.Medicamentos.FirstOrDefault(e => e.Id_medicamento == id_medicamento) != null;
    }

    // POST: api/Medicamento

    /// <summary>
    /// Criar dado de medicamento
    /// </summary>
    /// <param name="medicamento">Inserir os dados de medicamento </param>
    ///  <response code="200">Medicamento criado com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criação medicamento: </returns>
    [HttpPost]
    [Route("criar/medicamento")]
    public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
    {
        _logger.LogInformation("Iniciando a criação de medicamento");

        try
        {
            var prescricaoId = await _context.Prescricaos.FirstOrDefaultAsync(m => m.Id_prescricao == medicamento.Id_prescricao);

            if (prescricaoId != null)
            {
                var medicamentoCriado = await _medicamentoService.CreateMedicamentoAsync(medicamento);

                _logger.LogInformation("Medicamento criado com sucesso");
                return Ok(medicamentoCriado);
            }
            else
            {
                _logger.LogWarning("Prescrição não encontrada para criação do medicamento");
                return BadRequest($"Id prescricao não existe");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao salvar medicamento");
            return BadRequest($"Erro ao salvar os dados: {ex.Message}");
        }

    }

    // DELETE: api/Medicamento/5

    /// <summary>
    /// Remove dados de medicamento
    /// </summary>
    /// <param name="id_medicamento">Id para pode remover: </param>
    /// <response code="204">Medicamento removido com sucesso.</response>
    /// <response code="404">Medicamento não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns>Deletar: </returns>
    [HttpDelete]
    [Route("deleta/medicamento/{id_medicamento:int}", Name = "DeletaMedicamento")]
    public async Task<IActionResult> DeleteMedicamento(int id_medicamento)
    {
        _logger.LogInformation("Iniciando a exclusão do medicamento");

        try
        {
            await _medicamentoService.DeleteMedicamentoAsync(id_medicamento);

            _logger.LogInformation("Medicamento foi excluído com sucesso");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar medicamento");
            return BadRequest($"Erro em deletar: {ex.Message}");
        }
    }

    private MedicamentoDTO CreateMedicamentoDto(Medicamento medicamento)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetMedicamentosById", new { id_medicamento = medicamento.Id_medicamento })!,
                "self",
                "GET"),
            
            new LinkDTO(
                Url.Link("GetAllMedicamentos", null)!,
                "collection",
                "GET"),
            
            new LinkDTO(
                Url.Link("UpdateMedicamento", new { id_medicamento = medicamento.Id_medicamento })!,
                "update",
                "PUT"),
            
            new LinkDTO(
                Url.Link("DeletaMedicamento", new { id_medicamento = medicamento.Id_medicamento })!,
                "delete",
                "DELETE")
        };

        return new MedicamentoDTO(medicamento, links);
    }
}
