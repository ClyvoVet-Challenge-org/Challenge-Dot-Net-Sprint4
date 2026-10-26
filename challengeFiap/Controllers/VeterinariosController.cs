using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.InterfacesService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class VeterinariosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<VeterinariosController> _logger;
    private readonly IVeterinarioService _veterinarioService;

    public VeterinariosController(AppDbContext context, ILogger<VeterinariosController> logger, IVeterinarioService veterinarioService)
    {
        _context = context;
        _logger = logger;
        _veterinarioService = veterinarioService;
    }

    // GET: api/Veterinario

    /// <summary>
    /// Relatorio Veterinario
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/veterinario", Name = "GetAllVeterinarios")]
    public async Task<ActionResult<IEnumerable<VeterinarioDTO>>> GetAllVeterinario()
    {
        _logger.LogInformation("Iniciando a busca de veterinarios");

        try
        {
            var relatorioVeterinario = await _veterinarioService.GetAllVeterinarioAsync();
            var veterinarioDtos = relatorioVeterinario
                .Select(veterinario => CreateVeterinarioDto(veterinario))
                .ToList();

            _logger.LogInformation("Busca de veterinarios concluída com sucesso");
            return Ok(veterinarioDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca de veterinarios");
            return BadRequest($"Erro em processar a busca: {ex.Message}");
        }
    }

    // GET: api/Veterinario/5

    /// <summary>
    /// Relatorio de Veterinario com o id: 
    /// </summary>
    /// <param name="id_vet">Buscar pelo id: </param>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/veterinario/{id_vet:int}", Name = "GetVeterinariosById")]
    public async Task<ActionResult<VeterinarioDTO>> GetVeterinario(int id_vet)
    {
        _logger.LogInformation("Iniciando a busca do veterinario");

        try
        {
            try
            {
                var veterinario = await _veterinarioService.GetVeterinarioIdAsync(id_vet);
                _logger.LogInformation("Veterinario foi encontrado com sucesso");
                var vetDto = CreateVeterinarioDto(veterinario);
                return Ok(vetDto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "veterinario não encontrada para o ID");
                return NotFound("Id de veterinario não encontrado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar o veterinario");
            return BadRequest($"Erro em buscar pelo id: {ex.Message}");
        }
    }

    // PUT: api/Veterinario/5

    /// <summary>
    /// Atualizar dados Veterinarios
    /// </summary>
    /// <param name="id_vet">Id para pode atualizar: </param>
    /// <param name="veterinario">Dados para serem inseridos: </param>
    /// <returns>atualizar: </returns>
    [HttpPut]
    [Route("atualizar/veterinario/{id_vet:int}", Name = "UpdateVeterinario")]
    public async Task<IActionResult> PutVeterinario(int id_vet, Veterinario veterinario)
    {
        _logger.LogInformation("Iniciando a atualização do veterinario");

        if (id_vet != veterinario.Id_vet)
        {
            _logger.LogWarning("ID do veterinario incorreto");
            return BadRequest("Id veterianario esta incorreto.");
        }

        try
        {
            var cpfVet = await _context.Veterinarios.FirstOrDefaultAsync(c => c.Cpf_vet == veterinario.Cpf_vet && c.Id_vet != id_vet);
            var crmvVet = await _context.Veterinarios.FirstOrDefaultAsync(cr => cr.Crmv_vet == veterinario.Crmv_vet && cr.Id_vet != id_vet);
            var email = await _context.Veterinarios.FirstOrDefaultAsync(e => e.Email_vet == veterinario.Email_vet && e.Id_vet != id_vet);

            if (cpfVet != null || crmvVet != null || email != null)
            {
                _logger.LogWarning("Precisa informar o email crmv e cpf");
                return BadRequest("Não foi possivel de cadastras: cpf, crmv ou email ja estão sento utilizando");
            }

            var veterinarioAtualizado = await _veterinarioService.UpdateVeterinarioAsync(id_vet, veterinario);


            _logger.LogInformation("Veterinario foi atualizado com sucesso");
            return Ok(veterinarioAtualizado);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VeterinarioExists(id_vet))
            {
                _logger.LogWarning("Veterinario informado não encontrado durante a atualização");
                return NotFound("Id verterinario não encontrado");
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar o veterinario");
            return BadRequest($"Erro em atualizar: {ex.Message}");
        }

    }

    private bool VeterinarioExists(int id_vet)
    {
        return _context.Veterinarios.FirstOrDefault(e => e.Id_vet == id_vet) != null;
    }

    // POST: api/Veterinario

    /// <summary>
    /// Criar Veterinarios
    /// </summary>
    /// <param name="veterinario">Criação de dados de veterianos</param>
    /// <response code="201">Veterinarios criado com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criação de veterinarios: </returns>
    [HttpPost]
    [Route("criar/veterinario")]
    public async Task<ActionResult<Veterinario>> PostVeterinario(Veterinario veterinario)
    {
        _logger.LogInformation("Iniciando o cadastro de um novo veterinario");

        try
        {
            var cpfVet = await _context.Veterinarios.FirstOrDefaultAsync(c => c.Cpf_vet == veterinario.Cpf_vet);
            var crmvVet = await _context.Veterinarios.FirstOrDefaultAsync(cr => cr.Crmv_vet == veterinario.Crmv_vet);
            var email = await _context.Veterinarios.FirstOrDefaultAsync(e => e.Email_vet == veterinario.Email_vet);

            if (cpfVet != null || crmvVet != null || email != null)
            {
                _logger.LogWarning("Não foi possível cadastrar o veterinario: CPF, CRMV ou email já existente");
                return BadRequest("Ja existe email, cpf ou crmv existente");
            }
            else
            {
                var veterinarioCriado = await _veterinarioService.CreateVeterinarioAsync(veterinario);
                _logger.LogInformation("Veterinario criado com sucesso.");
                return Ok(veterinarioCriado);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao salvar os dados do veterinario");
            return BadRequest($"Erro em salvar os dados: {ex.Message}");
        }
    }

    // DELETE: api/Veterinario/5
    /// <summary>
    /// Remove dados de veterinarios
    /// </summary>
    /// <param name="id_vet"></param>
    /// <response code="204">Veterinario removido com sucesso.</response>
    /// <response code="404">Veterinario não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns>Deletado: </returns>
    [HttpDelete]
    [Route("deleta/veterinario/{id_vet:int}", Name = "DeletaVeterinario")]
    public async Task<IActionResult> DeleteVeterinario(int id_vet)
    {
        _logger.LogInformation("Iniciando a exclusão do veterinario");

        try
        {
            await _veterinarioService.DeleteVeterinarioAsync(id_vet);

            _logger.LogInformation("Veterinario foi excluído com sucesso");

            return NoContent();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar o veterinario");
            return BadRequest($"Erro em deletar: {ex.Message}");
        }
    }

    private VeterinarioDTO CreateVeterinarioDto(Veterinario veterinario)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetVeterinariosById", new { id_vet = veterinario.Id_vet })!,
                "self",
                "GET"),
            
            new LinkDTO(
                Url.Link("GetAllVeterinarios", null)!,
                "collection",
                "GET"),

            new LinkDTO(
                Url.Link("UpdateVeterinario", new { id_vet = veterinario.Id_vet })!,
                "update",
                "PUT"),

            new LinkDTO(
                Url.Link("DeletaVeterinario", new { id_vet = veterinario.Id_vet })!,
                "delete",
                "DELETE")
        };

        return new VeterinarioDTO(veterinario, links);
    }
}