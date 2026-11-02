using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.InterfacesService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
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
            int pageNumber = int.TryParse(Request.Query["pageNumber"], out var pNum) ? pNum : 1;
            int pageSize = int.TryParse(Request.Query["pageSize"], out var pSize) ? pSize : 10;
            string? veterinario = Request.Query["veterinario"].FirstOrDefault();
            string? search = Request.Query["search"].FirstOrDefault();
            string sortBy = Request.Query["sortBy"].FirstOrDefault() ?? "id";
            bool ascending = bool.TryParse(Request.Query["ascending"], out var asc) ? asc : true;

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 50) pageSize = 50;

            var clinicaVet = await _vetClinica.GetAllVetClinicaAsync();
            var query = clinicaVet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(veterinario) && int.TryParse(veterinario, out var idVeterinario))
            {
                query = query.Where(v => v.Id_vet == idVeterinario);
            }

            if (!string.IsNullOrWhiteSpace(search) && int.TryParse(search, out var idClinica))
            {
                query = query.Where(v => v.Id_clinica == idClinica);
            }

            query = sortBy.ToLower() switch
            {
                "veterinario" => ascending
                    ? query.OrderBy(v => v.Id_vet)
                    : query.OrderByDescending(v => v.Id_vet),

                "clinica" => ascending
                    ? query.OrderBy(v => v.Id_clinica)
                    : query.OrderByDescending(v => v.Id_clinica),

                _ => ascending
                    ? query.OrderBy(v => v.Id_clinica_vet)
                    : query.OrderByDescending(v => v.Id_clinica_vet)
            };
            var totalRecords = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var vetClinicaDtos = items.Select(CreateVetClinicaDto)
                .ToList();

            var paginationLinks = CreatePaginationLinks(
                pageNumber,
                pageSize,
                veterinario,
                search,
                sortBy,
                ascending,
                totalPages);

            var response = new PagedResponse<VetClinicaDTO>(
                vetClinicaDtos,
                pageNumber,
                pageSize,
                totalPages,
                totalRecords,
                paginationLinks
            );

            _logger.LogInformation("Busca de ClinicaVet concluída com sucesso");
            return Ok(response);
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

    private List<LinkDTO> CreatePaginationLinks(int pageNumber, int pageSize, string? veterinario, string? search, string sortBy, bool ascending, int totalPages)
    {
        var links = new List<LinkDTO> { new LinkDTO(Url.Link("GetAllVetClinicas", new { pageNumber, pageSize, veterinario, search, sortBy, ascending })!, "self", "GET") };
        if (pageNumber < totalPages) links.Add(new LinkDTO(Url.Link("GetAllVetClinicas", new { pageNumber = pageNumber + 1, pageSize, veterinario, search, sortBy, ascending })!, "next_page", "GET"));
        if (pageNumber > 1) links.Add(new LinkDTO(Url.Link("GetAllVetClinicas", new { pageNumber = pageNumber - 1, pageSize, veterinario, search, sortBy, ascending })!, "prev_page", "GET"));
        return links;
    }
}