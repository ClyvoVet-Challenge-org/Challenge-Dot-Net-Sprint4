using Microsoft.AspNetCore.Mvc;
using challengeFiap.Domain.Interfaces.IService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class PrescricaosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<PrescricaosController> _logger;
    private readonly IPrescricaoService _prescricaoService;
    public PrescricaosController(AppDbContext context, ILogger<PrescricaosController> logger, IPrescricaoService prescricaoService)
    {
        _context = context;
        _logger = logger;
        _prescricaoService = prescricaoService;
    }

    // GET: api/Prescricao

    /// <summary>
    /// Relatorio de daods prescrição
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>Relatorio prescrição</returns>
    [HttpGet]
    [Route("relatorio/prescricao", Name = "GetAllPrescricoes")]
    public async Task<ActionResult<IEnumerable<PrescricaoDTO>>> GetAllPrescricao()
    {
        _logger.LogInformation("Iniciando a busca de prescrições");

        try
        {
            int pageNumber = int.TryParse(Request.Query["pageNumber"], out var pNum) ? pNum : 1;
            int pageSize = int.TryParse(Request.Query["pageSize"], out var pSize) ? pSize : 10;
            string? observacoes = Request.Query["observacoes"].FirstOrDefault();
            string? search = Request.Query["search"].FirstOrDefault();
            string sortBy = Request.Query["sortBy"].FirstOrDefault() ?? "emissao";
            bool ascending = bool.TryParse(Request.Query["ascending"], out var asc) ? asc : true;

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 50) pageSize = 50;

            var prescricaoRelatorio = await _prescricaoService.GetAllPrescricaoAsync();
            var query = prescricaoRelatorio.AsQueryable();

            if (!string.IsNullOrWhiteSpace(observacoes))
            {
                query = query.Where(p =>
                    p.Observacoes_gerais != null &&
                    p.Observacoes_gerais.Contains(
                        observacoes.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(search) && int.TryParse(search, out var consultaId))
            {
                query = query.Where(p => p.Id_consulta == consultaId);
            }

            query = sortBy.ToLower() switch
            {
                "id" => ascending
                    ? query.OrderBy(p => p.Id_prescricao)
                    : query.OrderByDescending(p => p.Id_prescricao),

                "expiracao" => ascending
                    ? query.OrderBy(p => p.Dt_expiracao)
                    : query.OrderByDescending(p => p.Dt_expiracao),

                _ => ascending
                    ? query.OrderBy(p => p.Dt_emissao)
                    : query.OrderByDescending(p => p.Dt_emissao)
            };
            var totalRecords = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var prescricaoDtos = items.Select(CreatePrescricaoDto)
                .ToList();

            var paginationLinks = CreatePaginationLinks(
                pageNumber,
                pageSize,
                observacoes,
                search,
                sortBy,
                ascending,
                totalPages);

            var response = new PagedResponse<PrescricaoDTO>(
                prescricaoDtos,
                pageNumber,
                pageSize,
                totalPages,
                totalRecords,
                paginationLinks
            );

            _logger.LogInformation("Busca de prescrições concluída com sucesso");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca de prescrições");
            return BadRequest($"Erro em buscar: {ex.Message}");
        }
    }

    // GET: api/Prescricao/5

    /// <summary>
    /// Relatorio de prescrição
    /// </summary>
    /// <param name="id_prescricao">Buscar pelo id: </param>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/prescricao/{id_prescricao:int}", Name = "GetPrescricoesById")]
    public async Task<ActionResult<PrescricaoDTO>> GetPrescricao(int id_prescricao)
    {
        _logger.LogInformation("Iniciando a busca da prescrição");

        try
        {
            try
            {
                var prescricao = await _prescricaoService.GetPrescricaoIdAsync(id_prescricao);
                _logger.LogInformation("Prescricao foi encontrada com sucesso");
                var precricaoDTO = CreatePrescricaoDto(prescricao);
                return Ok(precricaoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "prescricao não encontrada para o ID");
                return NotFound("Id de Prescricao não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar prescrição");
            return BadRequest($"Erro em buscar: {ex.Message}");
        }

    }

    // PUT: api/Prescricao/5

    /// <summary>
    /// Atualizar dados de prescrição
    /// </summary>
    /// <param name="id_prescricao">ID para pode atualizar</param>
    /// <param name="prescricao">Dados para serem inseridos</param>
    /// <response code="204">Prescrição atualizado</response>
    /// <response code="400">Erro na requisição</response>
    /// <response code="404">Prescrição não encontrado</response>
    /// <returns></returns>
    [HttpPut]
    [Route("atualizar/prescricao/{id_prescricao:int}", Name = "UpdatePrescricao")]
    public async Task<IActionResult> PutPrescricao(int id_prescricao, Prescricao prescricao)
    {
        _logger.LogInformation("Iniciando a atualização da prescrição");

        if (id_prescricao != prescricao.Id_prescricao)
        {
            _logger.LogWarning("ID da prescrição informado na rota é diferente do ID enviado no objeto");
            return BadRequest("Id da prescrição está errado.");
        }

        try
        {
            var prescricaoAtualizada = await _prescricaoService.UpdatePrescricaoAsync(id_prescricao, prescricao);

            _logger.LogInformation("Prescrição atualizada com sucesso");
            return Ok(prescricaoAtualizada);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PrescricaoExists(id_prescricao))
            {
                _logger.LogWarning("Prescrição não encontrada durante a atualização");
                return NotFound("Id prescrição não encontrado");
            }
            throw;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar prescrição");
            return BadRequest($"Erro em atualizar: {ex.Message}");
        }
    }

    private bool PrescricaoExists(int id_prescricao)
    {
        return _context.Prescricaos.FirstOrDefault(e => e.Id_prescricao == id_prescricao) != null;
    }

    // POST: api/Prescricao

    /// <summary>
    /// Criar Precrição
    /// </summary>
    /// <param name="prescricao">Criação de dados a prescrição</param>
    /// <response code="201">Precisção criado com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criar prescrição</returns>
    [HttpPost]
    [Route("criar/prescricao")]
    public async Task<ActionResult<Prescricao>> PostPrescricao(Prescricao prescricao)
    {
        _logger.LogInformation("Iniciando a criação de prescrição para a consulta");

        try
        {
            var consultaExiste = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id_consulta == prescricao.Id_consulta);

            if (consultaExiste != null)
            {
                var prescricaoCriada = await _prescricaoService.CreatePrescricaoAsync(prescricao);

                _logger.LogInformation("Prescrição criada com sucesso.");
                return Ok(prescricaoCriada);
            }
            else
            {
                _logger.LogWarning("Consulta não encontrada para criação da prescrição");
                return BadRequest($"Id consulta não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao salvar prescrição para a consulta");
            return BadRequest($"Erro ao salvar os dados: {ex.Message}");
        }

    }

    // DELETE: api/Prescricao/5

    /// <summary>
    /// Remove dados Precrição
    /// </summary>
    /// <param name="id_prescricao">Id para pode remover: </param>
    /// <response code="204">Prescrição removido com sucesso.</response>
    /// <response code="404">Prescrição não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns>Deletado: </returns>
    [HttpDelete]
    [Route("deleta/prescricao/{id_prescricao:int}", Name = "DeletaPrescricao")]
    public async Task<IActionResult> DeletePrescricao(int id_prescricao)
    {
        _logger.LogInformation("Iniciando a exclusão da prescrição ");

        try
        {
            await _prescricaoService.DeletePrescricaoAsync(id_prescricao);

            _logger.LogInformation("Prescricao foi excluída com sucesso");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao deletar prescrição");
            return BadRequest($"Erro em deletar: {ex.Message}");
        }

    }

    private PrescricaoDTO CreatePrescricaoDto(Prescricao prescricao)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetPrescricoesById", new { id_prescricao = prescricao.Id_prescricao })!,
                "self",
                "GET"),

            new LinkDTO(
                Url.Link("GetAllPrescricoes", null)!,
                "collection",
                "GET"),

            new LinkDTO(
                Url.Link("UpdatePrescricao", new { id_prescricao = prescricao.Id_prescricao })!,
                "update",
                "PUT"),

            new LinkDTO(
                Url.Link("DeletaPrescricao", new { id_prescricao = prescricao.Id_prescricao })!,
                "delete",
                "DELETE")
        };

        return new PrescricaoDTO(prescricao, links);
    }

    private List<LinkDTO> CreatePaginationLinks(
        int pageNumber,
        int pageSize,
        string? observacoes,
        string? search,
        string sortBy,
        bool ascending,
        int totalPages)
    {
        var routeValues = new
        {
            pageNumber,
            pageSize,
            observacoes,
            search,
            sortBy,
            ascending
        };
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetAllPrescricoes", routeValues)!,
                "self",
                "GET")
        };

        if (pageNumber < totalPages)
        {
            links.Add(new LinkDTO(
                Url.Link("GetAllPrescricoes", new
            {
                pageNumber = pageNumber + 1,
                pageSize,
                observacoes,
                search,
                sortBy,
                ascending
            })!,
                "next_page",
                "GET"));
        }

        if (pageNumber > 1)
        {
            links.Add(new LinkDTO(
                Url.Link("GetAllPrescricoes", new
            {
                pageNumber = pageNumber - 1,
                pageSize,
                observacoes,
                search,
                sortBy,
                ascending
            })!,
                "prev_page",
                "GET"));
        }

        return links;
    }
}