using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.InterfacesService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class ClinicasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<ClinicasController> _logger;
    private readonly IClinicaService _clinicaService;

    public ClinicasController(
        AppDbContext context,
        ILogger<ClinicasController> logger,
        IClinicaService clinicaService)
    {
        _context = context;
        _logger = logger;
        _clinicaService = clinicaService;
    }

    // GET: api/Clinica

    /// <summary>
    /// Relatorio clinica.
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>Relatorio clinica</returns>
    [HttpGet]
    [Route("relatorio/clinica", Name = "GetAllClinicas")]
    public async Task<ActionResult<IEnumerable<ClinicaDTO>>> GetAllClinica()
    {
        _logger.LogInformation("Iniciando a busca de clinicas");

        try
        {
            int pageNumber = int.TryParse(Request.Query["pageNumber"], out var pNum) ? pNum : 1;
            int pageSize = int.TryParse(Request.Query["pageSize"], out var pSize) ? pSize : 10;
            string? nome = Request.Query["nome"].FirstOrDefault();
            string? search = Request.Query["search"].FirstOrDefault();
            string sortBy = Request.Query["sortBy"].FirstOrDefault() ?? "nome";
            bool ascending = bool.TryParse(Request.Query["ascending"], out var asc) ? asc : true;

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 50) pageSize = 50;

            var relatorioClinica = await _clinicaService.GetAllClinicaAsync();
            var query = relatorioClinica.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(c =>
                    c.Nm_clinica != null &&
                    c.Nm_clinica.Contains(
                        nome.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    c.Cnpj_clinica != null &&
                    c.Cnpj_clinica.Contains(
                        search.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            }

            query = sortBy.ToLower() switch
            {
                "id" => ascending
                    ? query.OrderBy(c => c.Id_clinica)
                    : query.OrderByDescending(c => c.Id_clinica),

                "cnpj" => ascending
                    ? query.OrderBy(c => c.Cnpj_clinica)
                    : query.OrderByDescending(c => c.Cnpj_clinica),

                _ => ascending
                    ? query.OrderBy(c => c.Nm_clinica)
                    : query.OrderByDescending(c => c.Nm_clinica)
            };

            var totalRecords = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            
            var clinicaDtos = items.Select(CreateClinicaDto)
                .ToList();
            
            var paginationLinks = CreatePaginationLinks(
                pageNumber,
                pageSize,
                nome,
                search,
                sortBy,
                ascending,
                totalPages);

            var response = new PagedResponse<ClinicaDTO>(
                clinicaDtos,
                pageNumber,
                pageSize,
                totalPages,
                totalRecords,
                paginationLinks
            );

            _logger.LogInformation("Busca de clinicas concluída com sucesso");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca na clinica");

            return BadRequest($"Erro em processar a busca: {ex.Message}");
        }
    }

    // GET: api/Clinica/5

    /// <summary>
    /// Relatorio de clinica feito pelo id.
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <param name="id_clinica">Id de buscar da clinica</param>
    /// <returns>Relatorio clinica</returns>
    [HttpGet]
    [Route("relatorio/clinica/{id_clinica:int}", Name = "GetClinicasById")]
    public async Task<ActionResult<ClinicaDTO>> GetClinica(int id_clinica)
    {
        _logger.LogInformation(
            "Iniciando a busca de clinica com ID: {IdClinica}",
            id_clinica);

        try
        {
            try
            {
                var clinica = await _clinicaService.GetClinicaIdAsync(id_clinica);
                _logger.LogInformation("Clinica foi encontrada com sucesso");
                var clinicadto = CreateClinicaDto(clinica);
                return Ok(clinicadto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "clinica não encontrada para o ID");
                return NotFound("Id clinica não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no processamento da buscar");
            return BadRequest($"Erro em buscar pelo id: {ex.Message}");
        }
    }

    // PUT: api/Clinica/5

    /// <summary>
    /// Atualizar dados clinica
    /// </summary>
    /// <param name="id_clinica">Id clinica para a url</param>
    /// <param name="clinica">Novos dados a clinica</param>
    /// <response code="200">clinica atualizado</response>
    /// <response code="400">Erro na requisição</response>
    /// <response code="404">clinica não encontrado</response>
    /// <returns>Atualização clinica</returns>
    [HttpPut]
    [Route("atualizar/clinica/{id_clinica:int}", Name = "UpdateClinica")]
    public async Task<IActionResult> PutClinica(int id_clinica, Clinica clinica)
    {
        _logger.LogInformation("Iniciando atualizacao de clinica com ID: {IdClinica}", id_clinica);

        if (id_clinica != clinica.Id_clinica)
        {
            _logger.LogWarning("O id da clinica esta incorreto.");
            return BadRequest("O id da clinica esta incorreto");
        }

        try
        {
            var clinicaAtualizada = await _clinicaService.UpdateClinicaAsync(id_clinica, clinica);

            _logger.LogInformation("Atualizacao de clinica do id");

            return Ok(clinicaAtualizada);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClinicaExists(id_clinica))
            {
                _logger.LogWarning("Id {IdClinica} não foi encontrado", id_clinica);

                return NotFound("A clinica não encontrada");
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro em atualizar clinica.");

            return BadRequest($"Erro em atualizar clinica: {ex.Message}");
        }
    }

    private bool ClinicaExists(int id_clinica)
    {
        return _context.Clinicas.FirstOrDefault(e => e.Id_clinica == id_clinica) != null;
    }

    // POST: api/Clinica

    /// <summary>
    /// Criação clinica
    /// </summary>
    /// <param name="clinica">Inserir dados</param>
    /// <response code="200">clinica criado com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criado sucesso</returns>
    [HttpPost]
    [Route("criar/clinica")]
    public async Task<ActionResult<Clinica>> PostClinica(Clinica clinica)
    {
        _logger.LogInformation("Iniciando criação de clinica");

        try
        {
            var existecpnj = await _context.Clinicas.FirstOrDefaultAsync(c => c.Cnpj_clinica == clinica.Cnpj_clinica);

            if (existecpnj != null)
            {
                _logger.LogWarning("O cnpj informado já existe");

                return BadRequest("O cnpj já existe");
            }

            var clinicaCriada = await _clinicaService.CreateadAsync(clinica);

            _logger.LogInformation("Clinica {IdClinica}, criada com sucesso", clinica.Id_clinica);

            return Ok(clinicaCriada);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro em salvar os dados");

            return BadRequest($"Erro em salvar os dados: {ex.Message}");
        }
    }

    // DELETE: api/Clinica/5

    /// <summary>
    /// Remove dados de um clinica pelo id
    /// </summary>
    /// <param name="id_clinica">Id clinica para ser buscado</param>
    /// <response code="204">clinica removido com sucesso.</response>
    /// <response code="404">Animal não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns></returns>
    [HttpDelete]
    [Route("deleta/clinica/{id_clinica:int}", Name = "DeletaClinica")]
    public async Task<IActionResult> DeleteClinica(int id_clinica)
    {
        _logger.LogInformation("Iniciando remoção de clinica. ID: {IdClinica}", id_clinica);

        try
        {
            await _clinicaService.DeleteClinicaAsync(id_clinica);

            _logger.LogInformation("Clinica foi excluída com sucesso");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro em deletar clinica.");
            return BadRequest($"Erro em deletar: {ex.Message}");
        }
    }

    private ClinicaDTO CreateClinicaDto(Clinica clinica)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetClinicasById", new { id_clinica = clinica.Id_clinica })!,
                "self",
                "GET"),

            new LinkDTO(
                Url.Link("GetAllClinicas", null)!,
                "collection",
                "GET"),

            new LinkDTO(
                Url.Link("UpdateClinica", new { id_clinica = clinica.Id_clinica })!,
                "update",
                "PUT"),

            new LinkDTO(
                Url.Link("DeletaClinica", new { id_clinica = clinica.Id_clinica })!,
                "delete",
                "DELETE")
        };

        return new ClinicaDTO(clinica, links);
    }

    private List<LinkDTO> CreatePaginationLinks(
        int pageNumber,
        int pageSize,
        string? nome,
        string? search,
        string sortBy,
        bool ascending,
        int totalPages)
    {
        var routeValues = new
        {
            pageNumber,
            pageSize,
            nome,
            search,
            sortBy,
            ascending
        };

        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetAllClinicas", routeValues)!,
                "self",
                "GET")
        };

        if (pageNumber < totalPages)
        {
            links.Add(new LinkDTO(
                Url.Link("GetAllClinicas", new
                {
                    pageNumber = pageNumber + 1,
                    pageSize,
                    nome,
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
                Url.Link("GetAllClinicas", new
                {
                    pageNumber = pageNumber - 1,
                    pageSize,
                    nome,
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
