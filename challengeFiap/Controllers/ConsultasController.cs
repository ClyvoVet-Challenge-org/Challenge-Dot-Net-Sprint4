using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.InterfacesService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class ConsultasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<ConsultasController> _logger;
    private readonly IConsultaService _consultaService;

    public ConsultasController(
        AppDbContext context,
        ILogger<ConsultasController> logger,
        IConsultaService consultaService)
    {
        _context = context;
        _logger = logger;
        _consultaService = consultaService;
    }

    // GET: api/Consulta

    /// <summary>
    /// Relaorio consulta
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>Buscar</returns>
    [HttpGet]
    [Route("relatorio/consulta", Name = "GetAllConsultas")]
    public async Task<ActionResult<IEnumerable<ConsultaDTO>>> GetAllConsulta()
    {
        _logger.LogInformation("Iniciando a busca de consultas");

        try
        {
            int pageNumber = int.TryParse(Request.Query["pageNumber"], out var pNum) ? pNum : 1;
            int pageSize = int.TryParse(Request.Query["pageSize"], out var pSize) ? pSize : 10;
            string? status = Request.Query["status"].FirstOrDefault();
            string? search = Request.Query["search"].FirstOrDefault();
            string sortBy = Request.Query["sortBy"].FirstOrDefault() ?? "data";
            bool ascending = bool.TryParse(Request.Query["ascending"], out var asc) ? asc : true;

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 50) pageSize = 50;

            var relatorioConsulta = await _consultaService.GetAllConsultaAsync();
            var query = relatorioConsulta.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<challengeFiap.Domain.Enums.StatusConsulta>(status, true, out var statusConsulta))
            {
                query = query.Where(c => c.St_consulta == statusConsulta);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    c.Historico_consulta != null &&
                    c.Historico_consulta.Contains(
                        search.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }

            query = sortBy.ToLower() switch
            {
                "id" => ascending
                    ? query.OrderBy(c => c.Id_consulta)
                    : query.OrderByDescending(c => c.Id_consulta),

                "status" => ascending
                    ? query.OrderBy(c => c.St_consulta)
                    : query.OrderByDescending(c => c.St_consulta),

                _ => ascending
                    ? query.OrderBy(c => c.Dt_consulta)
                    : query.OrderByDescending(c => c.Dt_consulta)
            };
            var totalRecords = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var consultaDtos = items.Select(CreateConsultaDto)
                .ToList();

            var paginationLinks = CreatePaginationLinks(
                pageNumber,
                pageSize,
                status,
                search,
                sortBy,
                ascending,
                totalPages);

            var response = new PagedResponse<ConsultaDTO>(
                consultaDtos,
                pageNumber,
                pageSize,
                totalPages,
                totalRecords,
                paginationLinks
            );

            _logger.LogInformation("Busca de consultas concluída com sucesso");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca a consulta");

            return BadRequest($"Erro em processar a busca: {ex.Message}");
        }
    }

    // GET: api/Consulta/5

    /// <summary>
    /// Relatorio de consulta pelo id:
    /// </summary>
    /// <param name="id_consulta">Id para buscar a informação</param>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <returns>Relatorio</returns>
    [HttpGet]
    [Route("relatorio/consulta/{id_consulta:int}", Name = "GetConsultasById")]
    public async Task<ActionResult<ConsultaDTO>> GetConsulta(int id_consulta)
    {
        _logger.LogInformation("Iniciando a busca de consulta com ID: {IdConsulta}", id_consulta);

        try
        {
            try
            {
                var consulta = await _consultaService.GetConsultaIdAsync(id_consulta);
                _logger.LogInformation("Consulta foi encontrada com sucesso");
                var consultadto = CreateConsultaDto(consulta);
                return Ok(consultadto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "consulta não encontrada para o ID");
                return NotFound("Id Consulta não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro em buscar consulta");

            return BadRequest($"Erro em buscar: {ex.Message}");
        }
    }

    // PUT: api/Consulta/5

    /// <summary>
    /// Atualizar dados consulta
    /// </summary>
    /// <param name="id_consulta">Id consulta para verificar: </param>
    /// <param name="consulta">dados consulta</param>
    /// <response code="200">Consulta atualizado</response>
    /// <response code="400">Erro na requisição</response>
    /// <response code="404">Consulta não encontrado</response>
    /// <returns>Atualização consulta</returns>
    [HttpPut]
    [Route("atualizar/consulta/{id_consulta:int}", Name = "UpdateConsulta")]
    public async Task<IActionResult> PutConsulta(int id_consulta, Consulta consulta)
    {
        _logger.LogInformation("Iniciando atualização de consulta");

        if (id_consulta != consulta.Id_consulta)
        {
            _logger.LogWarning("Id da consulta oferencido não esta igual do atualzar.");

            return BadRequest("Id da consulta está incorreto");
        }

        try
        {
            var consultaAtualizada = await _consultaService.UpdateConsultaAsync(id_consulta, consulta);

            _logger.LogInformation("Consulta atualizada com sucesso");

            return Ok(consultaAtualizada);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ConsultaExists(id_consulta))
            {
                _logger.LogWarning("Consulta não encontrada");

                return NotFound("Consulta não encontrada");
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro em atualizar consulta");
            return BadRequest($"Erro em atualizar: {ex.Message}");
        }
    }
    private bool ConsultaExists(int id_consulta)
    {
        return _context.Consultas.FirstOrDefault(e => e.Id_consulta == id_consulta) != null;
    }

    // POST: api/Consulta

    /// <summary>
    /// Criação de consulta
    /// </summary>
    /// <param name="consulta">Dados para ser inseridos</param>
    /// <response code="201">Consulta criada com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criação de dados consulta</returns>
    [HttpPost]
    [Route("criar/consulta")]
    public async Task<ActionResult<Consulta>> PostConsulta(Consulta consulta)
    {
        _logger.LogInformation("Iniciado criação de uma nova consulta.");

        try
        {
            var animalExistente = await _context.Animals.FirstOrDefaultAsync(a => a.Id_animal == consulta.Id_animal);

            var vetExistente = await _context.Veterinarios.FirstOrDefaultAsync(v => v.Id_vet == consulta.Id_vet);

            if (animalExistente != null && vetExistente != null)
            {
                var consultaCriada = await _consultaService.CreateConsultaAsync(consulta);

                _logger.LogInformation("Consulta criada com sucesso");

                return Ok(consultaCriada);
            }
            else
            {
                _logger.LogWarning("Id animal ou vet não foram encontrado.");

                return BadRequest("Ids não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao salvar os dados da consulta.");
            return BadRequest($"Erro ao salvar os dados: {ex.Message}");
        }
    }

    // DELETE: api/Consulta/5

    /// <summary>
    /// Deletar consulta
    /// </summary>
    /// <param name="id_consulta">id para buscar</param>
    /// <response code="204">consulta removido com sucesso.</response>
    /// <response code="404">consulta não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns>deletar consulta</returns>
    [HttpDelete]
    [Route("deleta/consulta/{id_consulta:int}", Name = "DeletaConsulta")]
    public async Task<IActionResult> DeleteConsulta(int id_consulta)
    {
        _logger.LogInformation("Iniciando remoção de consulta.");

        try
        {
            await _consultaService.DeleteConsultaAsync(id_consulta);

            _logger.LogInformation("Consulta foi excluída com sucesso");

            return NoContent();
        }
        catch (Exception ex)
        {

            return BadRequest($"Erro em deletar: {ex.Message}");
        }
    }

    private ConsultaDTO CreateConsultaDto(Consulta consulta)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link(
                "GetConsultasById", new { id_consulta = consulta.Id_consulta })!,
                "self",
                "GET"),

            new LinkDTO(
                Url.Link(
                "GetAllConsultas", null)!,
                "collection",
                "GET"),

            new LinkDTO(
                Url.Link(
                "UpdateConsulta", new { id_consulta = consulta.Id_consulta })!,
                "update",
                "PUT"),

            new LinkDTO(
                Url.Link(
                "DeletaConsulta", new { id_consulta = consulta.Id_consulta })!,
                "delete",
                "DELETE"
                )
        };

        return new ConsultaDTO(consulta, links);
    }

    private List<LinkDTO> CreatePaginationLinks(
        int pageNumber,
        int pageSize,
        string? status,
        string? search,
        string sortBy,
        bool ascending,
        int totalPages)
    {
        var routeValues = new
        {
            pageNumber,
            pageSize,
            status,
            search,
            sortBy,
            ascending
        };

        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetAllConsultas", routeValues)!,
                "self",
                "GET")
        };

        if (pageNumber < totalPages)
        {
            links.Add(new LinkDTO(
                Url.Link("GetAllConsultas", new
                {
                    pageNumber = pageNumber + 1,
                    pageSize,
                    status,
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
                Url.Link("GetAllConsultas", new
                {
                    pageNumber = pageNumber - 1,
                    pageSize,
                    status,
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
