using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.InterfacesService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class TutorController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<TutorController> _logger;
    private readonly ITutorService _tutorService;
    public TutorController(AppDbContext context, ILogger<TutorController> logger, ITutorService tutorService)
    {
        _context = context;
        _logger = logger;
        _tutorService = tutorService;
    }

    // GET: api/Tutor

    /// <summary>
    /// Relatorio Tutor
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/Tutor", Name = "GetAllTutores")]
    public async Task<ActionResult<IEnumerable<TutorDTO>>> GetAllTutor()
    {
        _logger.LogInformation("Iniciando a busca de tutores");

        try
        {
            var TutorRelatorio = await _tutorService.GetAllTutorAsync();
            var tutorDtos = TutorRelatorio
                .Select(tutor => CreateTutorDto(tutor))
                .ToList();

            _logger.LogInformation("Busca de tutores concluída com sucesso");
            return Ok(tutorDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca de tutores");
            return BadRequest($"Erro em buscar: {ex.Message}");
        }
    }

    // GET: api/Tutor/5

    /// <summary>
    /// Relatorio de Tutor feito pelo id
    /// </summary>
    /// <param name="id_Tutor">Buscar pelo id: </param>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/Tutor/{id_Tutor:int}", Name = "GetTutoresById")]
    public async Task<ActionResult<TutorDTO>> GetTutor(int id_Tutor)
    {
        _logger.LogInformation("Iniciando a busca do tutor pelo ID {IdTutor}", id_Tutor);

        try
        {
            try
            {
                var Tutor = await _tutorService.GetTutorIdAsync(id_Tutor);
                _logger.LogInformation("Tutor foi encontrado com sucesso");
                var tutorDto = CreateTutorDto(Tutor);
                return Ok(tutorDto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "tutor não encontrado para o ID");
                return NotFound("Id de tutor não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tutor pelo ID {IdTutor}", id_Tutor);
            return BadRequest($"Erro em buscar: {ex.Message}");
        }

    }

    // PUT: api/Tutor/5

    /// <summary>
    /// Atualizar dados reponsavel
    /// </summary>
    /// <param name="id_Tutor">Id para pode atualizar</param>
    /// <param name="Tutor">Dados para serem inseridos</param>
    ///  <response code="204">Tutor atualizado</response>
    /// <response code="400">Erro na requisição</response>
    /// <response code="404">Tutor não encontrado</response>
    /// <returns>Atualizar: </returns>
    [HttpPut]
    [Route("atualizar/Tutor/{id_Tutor:int}", Name = "UpdateTutor")]
    public async Task<IActionResult> PutTutor(int id_Tutor, Tutor Tutor)
    {
        _logger.LogInformation("Iniciando a atualização do tutor");

        if (id_Tutor != Tutor.Id_tutor)
        {
            _logger.LogWarning("ID do tutor informado na rota é diferente do ID enviado no objeto.");
            return BadRequest("Id Tutor está incorreto");
        }

        try
        {
            var cpfExiste = await _context.Tutor
                .FirstOrDefaultAsync(
                c => c.Cpf_tutor == Tutor.Cpf_tutor && c.Id_tutor != id_Tutor);

            if (cpfExiste != null)
            {
                _logger.LogWarning("CPF já está sendo utilizado por outro tutor");
                return BadRequest("Cpf já esta sendo utilizando");
            }

            var tutorAtualizado = await _tutorService.UpdateTutorAsync(id_Tutor, Tutor);

            _logger.LogInformation("Tutor atualizado com sucesso");
            return Ok(tutorAtualizado);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TutorExists(id_Tutor))
            {
                _logger.LogWarning("Tutor não encontrado durante a atualização");
                return NotFound("Id Tutor nao achando");
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar tutor");
            return BadRequest($"Erro em atualizar Tutor: {ex.Message}");
        }
    }

    private bool TutorExists(int id_Tutor)
    {
        return _context.Tutor.FirstOrDefault(e => e.Id_tutor == id_Tutor) != null;
    }

    // POST: api/Tutor

    /// <summary>
    /// Criar Tutor
    /// </summary>
    /// <param name="Tutor">Criação de dados de endereço Tutor </param>
    /// <response code="201">Tutor criado com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criação Tutor</returns>
    [HttpPost]
    [Route("criar/Tutor")]
    public async Task<ActionResult<Tutor>> PostTutor(Tutor Tutor)
    {
        _logger.LogInformation("Iniciando a criação do tutor");

        try
        {
            var cpfExiste = await
                _context.Tutor
                .FirstOrDefaultAsync(a => a.Cpf_tutor == Tutor.Cpf_tutor);

            if (cpfExiste == null)
            {
                var tutorCriado = await _tutorService.CreateTutorAsync(Tutor);

                _logger.LogInformation("Tutor criado com sucesso.");
                return Ok(tutorCriado);
            }
            else
            {
                _logger.LogWarning("Tentativa de criação de tutor com CPF já existente.");
                return BadRequest("Cpf já existente");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao criar tutor");
            return BadRequest($"Erro encontrado: {ex.Message}");
        }
    }

    // DELETE: api/Tutor/5

    /// <summary>
    /// Remove dados de Tutor
    /// </summary>
    /// <param name="id_Tutor">Id para pode remover: </param>
    /// <response code="204">Tutor removido com sucesso.</response>
    /// <response code="404">Tutor não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns>Deletado: </returns>
    [HttpDelete]
    [Route("deleta/Tutor/{id_Tutor:int}", Name = "DeletaTutor")]
    public async Task<IActionResult> DeleteTutor(int id_Tutor)
    {
        _logger.LogInformation("Iniciando a exclusão do tutor");

        try
        {
            await _tutorService.DeleteTutorAsync(id_Tutor);

            _logger.LogInformation("Tutor foi excluído com sucesso");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar ID tutor");
            return BadRequest($"Erro em deletar: {ex.Message}");
        }
    }

    private TutorDTO CreateTutorDto(Tutor tutor)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetTutoresById", new { id_Tutor = tutor.Id_tutor })!,
                "self",
                "GET"),
            
            new LinkDTO(
                Url.Link("GetAllTutores", null)!,
                "collection",
                "GET"),
            
            new LinkDTO(
                Url.Link("UpdateTutor", new { id_Tutor = tutor.Id_tutor })!,
                "update",
                "PUT"),
            
            new LinkDTO(
                Url.Link("DeletaTutor", new { id_Tutor = tutor.Id_tutor })!,
                "delete",
                "DELETE")
        };

        return new TutorDTO(tutor, links);
    }
}