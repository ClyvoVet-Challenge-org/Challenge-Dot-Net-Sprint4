using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class VetClinicasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<VetClinicasController> _logger;
    private readonly IVetClinicaService _vetClinica;
    public VetClinicasController(AppDbContext context, ILogger<VetClinicasController> logger, IVetClinicaService vetClinica)
    {
        _context = context;
        _logger = logger;
        _vetClinica = vetClinica;
    }

    // GET: api/VetClinica

    /// <summary>
    /// Relatorio de dados de Vet e clinica
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/vetclinica", Name = "GetAllVetClinicas")]
    public async Task<ActionResult<IEnumerable<VetClinicaDTO>>> GetAllVetClinica()
    {
        _logger.LogInformation("Iniciando a busca de veterinários e clínicas");

        try
        {
            var clinicaVet = await _vetClinica.GetAllVetClinicaAsync();
            var vetClinicaDtos = clinicaVet
                .Select(vetClinica => CreateVetClinicaDto(vetClinica))
                .ToList();

            _logger.LogInformation("Busca de ClinicaVet concluída com sucesso");
            return Ok(vetClinicaDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca de ClinicaVet");
            return BadRequest($"Erro em buscar: {ex.Message}");
        }
    }

    // GET: api/VetClinica/5

    /// <summary>
    /// Relatorio de Vet Clinica
    /// </summary>
    /// <param name="id_clinica_vet">Buscar id: </param>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/vetclinica/{id_clinica_vet:int}", Name = "GetVetClinicasById")]
    public async Task<ActionResult<VetClinicaDTO>> GetVetClinica(int id_clinica_vet)
    {
        _logger.LogInformation("Iniciando a busca de ClinicaVet");

        try
        {
            try
            {
                var vetclinica = await _vetClinica.GetVetClinicaIdAsync(id_clinica_vet);
                _logger.LogInformation("ClinicaVet foi encontrada com sucesso");
                var vetClinicaDto = CreateVetClinicaDto(vetclinica);
                return Ok(vetClinicaDto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "vetclinica não encontrada para o ID");
                return NotFound("Id vetclinica não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar ClinicaVet ");
            return BadRequest($"Erro em buscar: {ex.Message}");
        }

    }

    // PUT: api/VetClinica/5

    /// <summary>
    /// Atualizar dados de vet clinica
    /// </summary>
    /// <param name="id_clinica_vet">Para inserir o id para pode atualizar: </param>
    /// <param name="vetclinica">dados para serem inseridos</param>
    /// <response code="204">Vet Clinica atualizado</response>
    /// <response code="400">Erro na requisição</response>
    /// <response code="404">Vet Clinica não encontrado</response>
    /// <returns>Atualizar: </returns>
    [HttpPut]
    [Route("atualizar/vetclinica/{id_clinica_vet:int}", Name = "UpdateVetClinica")]
    public async Task<IActionResult> PutVetClinica(int id_clinica_vet, VetClinica vetclinica)
    {
        _logger.LogInformation("Iniciando a atualização de ClinicaVet");

        if (id_clinica_vet != vetclinica.Id_clinica_vet)
        {
            _logger.LogWarning("ID da ClinicaVet informado é diferente");
            return BadRequest("Id ClinicaVet está incorretor");
        }

        try
        {
            var existeClinica = await _context.Clinicas
                .FirstOrDefaultAsync(c => c.Id_clinica == vetclinica.Id_clinica);
            var existeVet = await _context.Veterinarios
                .FirstOrDefaultAsync(c => c.Id_vet == vetclinica.Id_vet);

            if (existeClinica != null && existeVet != null)
            {
                var vetClinicaAtualizado = await _vetClinica.UpdateVetClinicaAsync(id_clinica_vet, vetclinica);

                _logger.LogInformation("ClinicaVet atualizado com sucesso");
                return Ok(vetClinicaAtualizado);
            }
            else
            {
                _logger.LogWarning("Não foi possível encontrar a Clinica Vet para atualização da vet clínica");
                return NotFound("Não foi possivel encontrar os id clinica ou vet.");
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VetClinicaExists(id_clinica_vet))
            {
                _logger.LogWarning("ID ClinicaVet não encontrada durante a atualização");
                return NotFound("Id clinica vet não existe");
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar ClinicaVet");
            return BadRequest($"Erro em atualizar: {ex.Message}");
        }
    }
    private bool VetClinicaExists(int id_clinica_vet)
    {
        return _context.VetClinicas.FirstOrDefault(e => e.Id_clinica_vet == id_clinica_vet) != null;
    }

    // POST: api/VetClinica
    /// <summary>
    /// Inserir Vet e clinica
    /// </summary>
    /// <param name="vetclinica">Inserir os dados de vet e clinica: </param>
    /// <response code="201">Vet Clinica criado com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criação: </returns>
    [HttpPost]
    [Route("criar/vetclinica")]
    public async Task<ActionResult<VetClinica>> PostVetClinica(VetClinica vetclinica)
    {
        _logger.LogInformation("Iniciando a criação de ClinicaVet");

        try
        {
            var exiteClinica = await _context.Clinicas.FirstOrDefaultAsync(c => c.Id_clinica == vetclinica.Id_clinica);
            var existeVet = await _context.Veterinarios.FirstOrDefaultAsync(v => v.Id_vet == vetclinica.Id_vet);

            if (exiteClinica != null && existeVet != null)
            {
                var vetClinicaCriada = await _vetClinica.CreateVetClinicaAsync(vetclinica);

                _logger.LogInformation("Vet clínica criada com sucesso");
                return Ok(vetClinicaCriada);
            }
            else
            {
                _logger.LogWarning("Clínica ou veterinário não encontrado, verificar.");
                return BadRequest("Id clinica e vet não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao salvar ClinicaVet ");
            return BadRequest($"Erro ao salvar os dados: {ex.Message}");
        }

    }

    // DELETE: api/VetClinica/5

    /// <summary>
    /// Remove dados de vet clinica
    /// </summary>
    /// <param name="id_clinica_vet">Inserir o id para pode deletar: </param>
    /// <response code="204">Vet clinica removido com sucesso.</response>
    /// <response code="404">Vet clinica não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns>Deletar: </returns>
    [HttpDelete]
    [Route("deleta/vetclinica/{id_clinica_vet:int}", Name = "DeletaVetClinica")]
    public async Task<IActionResult> DeleteVetClinica(int id_clinica_vet)
    {
        _logger.LogInformation("Iniciando a exclusão da ClinicaVet");

        try
        {
            await _vetClinica.DeleteVetClinicaAsync(id_clinica_vet);

            _logger.LogInformation("VetClinica foi excluída com sucesso");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar vetclinica");
            return BadRequest($"Erro em deletar: {ex.Message}");
        }
    }

    private VetClinicaDTO CreateVetClinicaDto(VetClinica vetClinica)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetVetClinicasById", new { id_clinica_vet = vetClinica.Id_clinica_vet })!,
                "self",
                "GET"),
            
            new LinkDTO(
                Url.Link("GetAllVetClinicas", null)!,
                "collection",
                "GET"),
            
            new LinkDTO(
                Url.Link("UpdateVetClinica", new { id_clinica_vet = vetClinica.Id_clinica_vet })!,
                "update",
                "PUT"),
            
            new LinkDTO(
                Url.Link("DeletaVetClinica", new { id_clinica_vet = vetClinica.Id_clinica_vet })!,
                "delete",
                "DELETE")
        };

        return new VetClinicaDTO(vetClinica, links);
    }
}