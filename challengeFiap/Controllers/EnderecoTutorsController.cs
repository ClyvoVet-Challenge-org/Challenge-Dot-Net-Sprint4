using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.InterfacesService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class EnderecoTutorsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<EnderecoTutorsController> _logger;
    private readonly IEnderecoTutorService _enderecoTutorService;
    public EnderecoTutorsController(AppDbContext context, ILogger<EnderecoTutorsController> logger, IEnderecoTutorService enderecoTutorService)
    {
        _context = context;
        _logger = logger;
        _enderecoTutorService = enderecoTutorService;
    }

    // GET: api/EnderecoResponsavel

    /// <summary>
    /// Relaorio de dados endereço responsavel
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>Relatorio endereço responsavel</returns>
    [HttpGet]
    [Route("relatorio/enderecoresponsavel", Name = "GetAllEnderecosResponsaveis")]
    public async Task<ActionResult<IEnumerable<EnderecoTutorDTO>>> GetAllEnderecoResponsavel()
    {
        _logger.LogInformation("Iniciando a busca de endereço responsavel");

        try
        {
            var relatorioEnResponsavel = await _enderecoTutorService.GetAllEnderecoTutorAsync();
            var enderecoTutorDtos = relatorioEnResponsavel
                .Select(endereco => CreateEnderecoTutorDto(endereco))
                .ToList();

            _logger.LogInformation("Busca de endereço responsavel concluída com sucesso");
            return Ok(enderecoTutorDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca de endereço responsavel");
            return BadRequest($"Erro em buscar: {ex.Message}");
        }
    }

    // GET: api/EnderecoResponsavel/5

    /// <summary>
    /// Relatorio de endereço responsavel pelo id
    /// </summary>
    /// <param name="id_endereco_responsavel"></param>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/enderecoresponsavel/{id_endereco_responsavel:int}", Name = "GetEnderecosResponsaveisById")]
    public async Task<ActionResult<EnderecoTutorDTO>> GetEnderecoResponsavel(int id_endereco_responsavel)
    {
        _logger.LogInformation("Iniciando a busca de endereço responsavel pelo ID {IdEnderecoResponsavel}", id_endereco_responsavel);

        try
        {
            try
            {
                var enderecoresponsavel = await _enderecoTutorService.GetEnderecoTutorIdAsync(id_endereco_responsavel);
                _logger.LogInformation("Endereço responsavel foi encontrado com sucesso");
                var enderecoTutor = CreateEnderecoTutorDto(enderecoresponsavel);
                return Ok(enderecoTutor);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "endereco tutor não encontrado para o ID");
                return NotFound("Id não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar endereço responsavel pelo ID {IdEnderecoResponsavel}", id_endereco_responsavel);
            return BadRequest($"Erro em buscar: {ex.Message}");
        }

    }

    // PUT: api/EnderecoResponsavel/5

    /// <summary>
    /// Atualizar dados endereço responsavel
    /// </summary>
    /// <param name="id_endereco_responsavel">Id para pode atualizar</param>
    /// <param name="enderecoresponsavel">Dados para ser inseridos</param>
    /// <response code="200">Endereço responsavel atualizado</response>
    /// <response code="400">Erro na requisição</response>
    /// <response code="404">Endereço responsavel não encontrado</response>
    /// <returns>Atualizar: </returns>
    [HttpPut]
    [Route("atualizar/enderecoresponsavel/{id_endereco_responsavel:int}", Name = "UpdateEnderecoResponsavel")]
    public async Task<IActionResult> PutEnderecoResponsavel(int id_endereco_responsavel, EnderecoTutor enderecoresponsavel)
    {
        _logger.LogInformation("Iniciando a atualização do endereço responsavel");

        if (id_endereco_responsavel != enderecoresponsavel.Id_endereco_tutor)
        {
            _logger.LogWarning("ID do endereço responsavel informado ta diferente do que foi informado");
            return BadRequest("Id endereco responsal esta incorreto");
        }

        try
        {
            var enderecoTutorAtualizado = await _enderecoTutorService.UpdateEnderecoTutorAsync(id_endereco_responsavel, enderecoresponsavel);

            _logger.LogInformation("Endereço responsavel atualizado com sucesso");
            
            return Ok(enderecoTutorAtualizado);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EnderecoResponsavelExists(id_endereco_responsavel))
            {
                _logger.LogWarning("Endereço responsavel não encontrado durante a atualização");
                return NotFound("Endereço responsavel não encontrado");
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar endereço responsavel");
            return BadRequest($"Erro encontrado: {ex.Message}");
        }

    }

    private bool EnderecoResponsavelExists(int id_endereco_responsavel)
    {
        return _context.EnderecoTutors.FirstOrDefault(e => e.Id_endereco_tutor == id_endereco_responsavel) != null;
    }

    // POST: api/EnderecoResponsavel

    /// <summary>
    /// Criar endereço responsavel
    /// </summary>
    /// <param name="enderecoresponsavel">Criação de dados de endereço responsavel</param>
    /// <response code="200">Endereço reponsavel criado com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criar endereço responsavel</returns>
    [HttpPost]
    [Route("criar/enderecoresponsavel")]
    public async Task<ActionResult<EnderecoTutor>> PostEnderecoResponsavel(EnderecoTutor enderecoresponsavel)
    {
        _logger.LogInformation("Iniciando a criação de endereço responsavel");
        try
        {
            var responsavelExiste = await _context.Tutor.FirstOrDefaultAsync(a => a.Id_tutor == enderecoresponsavel.Id_tutor);

            if (responsavelExiste != null)
            {
                var enderecoTutorCriado = await _enderecoTutorService.CreateEnderecoTutorAsync(enderecoresponsavel);

                _logger.LogInformation("Endereço responsavel criado com sucesso");
                return Ok(enderecoTutorCriado);
            }
            else
            {
                _logger.LogWarning("Tutor não encontrado para criação do endereço responsavel");
                return BadRequest($"ID resonsavel não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao salvar endereço responsavel");
            return BadRequest($"Erro ao salvar os dados: {ex.Message}");
        }

    }

    // DELETE: api/EnderecoResponsavel/5

    /// <summary>
    /// Remove dados de endereço responsavel
    /// </summary>
    /// <param name="id_endereco_responsavel">Id para pode remover: </param>
    /// <response code="204">Endereco responsavel removido com sucesso.</response>
    /// <response code="404">Endereco responsavel não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns>Deletado: </returns>
    [HttpDelete]
    [Route("deleta/enderecoresponsavel/{id_endereco_responsavel:int}", Name = "DeletaEnderecoResponsavel")]
    public async Task<IActionResult> DeleteEnderecoResponsavel(int id_endereco_responsavel)
    {
        _logger.LogInformation("Iniciando a exclusão do endereço responsavel");

        try
        {
            await _enderecoTutorService.DeleteEnderecoTutorAsync(id_endereco_responsavel);


            _logger.LogInformation("EnderecoTutor foi excluído com sucesso");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar endereço responsavel");
            return BadRequest($"Erro em deletar: {ex.Message}");
        }

    }

    private EnderecoTutorDTO CreateEnderecoTutorDto(EnderecoTutor enderecoTutor)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetEnderecosResponsaveisById", new { id_endereco_responsavel = enderecoTutor.Id_endereco_tutor })!,
                "self",
                "GET"),
            
            new LinkDTO(
                Url.Link("GetAllEnderecosResponsaveis", null)!,
                "collection",
                "GET"),
            
            new LinkDTO(
                Url.Link("UpdateEnderecoResponsavel", new { id_endereco_responsavel = enderecoTutor.Id_endereco_tutor })!,
                "update",
                "PUT"),
            
            new LinkDTO(
                Url.Link("DeletaEnderecoResponsavel", new { id_endereco_responsavel = enderecoTutor.Id_endereco_tutor })!,
                "delete",
                "DELETE")
        };

        return new EnderecoTutorDTO(enderecoTutor, links);
    }
}
