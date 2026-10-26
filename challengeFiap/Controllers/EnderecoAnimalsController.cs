using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Data;
using Microsoft.AspNetCore.Mvc;
using challengeFiap.Application.InterfacesService;
using Microsoft.EntityFrameworkCore;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;

[Route("api/[controller]")]
[ApiController]
public class EnderecoAnimalsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<EnderecoAnimalsController> _logger;
    private readonly IEnderecoAnimalService _enderecoAnimalService;

    public EnderecoAnimalsController(
        AppDbContext context,
        ILogger<EnderecoAnimalsController> logger,
        IEnderecoAnimalService enderecoAnimalService)
    {
        _context = context;
        _logger = logger;
        _enderecoAnimalService = enderecoAnimalService;
    }

    // GET: api/EnderecoAnimal

    /// <summary>
    /// Relatorio de dados endereço animal
    /// </summary>
    /// <response code="200">Busca feita com sucesso</response>
    /// <returns>Relatorio endereço animal</returns>
    [HttpGet]
    [Route("relatorio/enderecoanimal", Name = "GetAllEnderecosAnimals")]
    public async Task<ActionResult<IEnumerable<EnderecoAnimalDTO>>> GetAllEnderecoAnimal()
    {
        _logger.LogInformation("Iniciando a busca de endereço animal");

        try
        {
            var relatorioEnAnimal = await _enderecoAnimalService.GetAllEnderecoAnimalAsync();
            var enderecoAnimalDtos = relatorioEnAnimal
                .Select(endereco => CreateEnderecoAnimalDto(endereco))
                .ToList();

            _logger.LogInformation("Busca de endereço animal concluída com sucesso");

            return Ok(enderecoAnimalDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a busca a endereço animal");

            return BadRequest($"Erro em processar a busca: {ex.Message}");
        }
    }

    // GET: api/EnderecoAnimal/5

    /// <summary>
    /// Relatorio de endereco animal feito pelo id
    /// </summary>
    /// <param name="id_endereco_animal">Buscar pelo id: </param>
    /// <response code="200">Busca feita com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    /// <returns>Relatorio: </returns>
    [HttpGet]
    [Route("relatorio/enderecoanimal/{id_endereco_animal:int}", Name = "GetEnderecosAnimalsById")]
    public async Task<ActionResult<EnderecoAnimalDTO>> GetEnderecoAnimal(int id_endereco_animal)
    {
        _logger.LogInformation("Iniciando a busca de endereço animal");

        try
        {
            try
            {
                var enderecoanimal = await _enderecoAnimalService.GetEnderecoAnimalIdAsync(id_endereco_animal);
                _logger.LogInformation("Endereço animal foi encontrado com sucesso");
                var enderecoAnimalDto = CreateEnderecoAnimalDto(enderecoanimal);
                return Ok(enderecoAnimalDto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "endereco animal não encontrado para o ID");
                return NotFound("Endereço animal não encontrado.");
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,"Erro em buscar endereço animal.");

            return BadRequest($"Erro em buscar: {ex.Message}");
        }
    }

    // PUT: api/EnderecoAnimal/5

    /// <summary>
    /// Atualizar dados endereço animal
    /// </summary>
    /// <param name="id_endereco_animal">Id para pode atualizar</param>
    /// <param name="enderecoanimal">dados para ser inseridos</param>
    /// <response code="204">Endereço animal atualizado</response>
    /// <response code="400">Erro na requisição</response>
    /// <response code="404">Endereço animal não encontrado</response>
    /// <returns>Atualizar: </returns>
    [HttpPut]
    [Route("atualizar/enderecoanimal/{id_endereco_animal:int}", Name = "UpdateEnderecoAnimal")]
    public async Task<IActionResult> PutEnderecoAnimal(int id_endereco_animal,EnderecoAnimal enderecoanimal)
    {
        _logger.LogInformation("Iniciando atualização de endereço animal");

        if (id_endereco_animal != enderecoanimal.Id_endereco_animal)
        {
            _logger.LogWarning("Id endereco animal está difeerente doque esta no sistema.");

            return BadRequest("Id endereco animal está incorreto");
        }
        try
        {
            var enderecoAnimalAtualizado =await _enderecoAnimalService.UpdateEnderecoAnimalAsync(id_endereco_animal,enderecoanimal);

            _logger.LogInformation("Atualização de endereço animal  foi concluída com sucesso.");

            return Ok(enderecoAnimalAtualizado);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EnderecoAnimalExists(id_endereco_animal))
            {
                _logger.LogWarning("Id endereço animal não encontrado.");

                return NotFound("Id endereço animal não encontrado");
            }
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Erro em atualizar endereco animal.");

            return BadRequest(
                $"Erro em atualizar endereço animal: {ex.Message} ");
        }
    }

    private bool EnderecoAnimalExists(int id_endereco_animal)
    {
        return _context.EnderecoAnimals.FirstOrDefault(e => e.Id_endereco_animal == id_endereco_animal) != null;
    }

    // POST: api/EnderecoAnimal

    /// <summary>
    /// Criar endereço animal
    /// </summary>
    /// <param name="enderecoanimal">Criação de dados de endereço animal</param>
    /// <response code="200">Endereço animal criado com sucesso.</response>
    /// <response code="400">Erro na validação.</response>
    /// <returns>Criar endereço animal</returns>
    [HttpPost]
    [Route("criar/enderecoanimal")]
    public async Task<ActionResult<EnderecoAnimal>> PostEnderecoAnimal(
        EnderecoAnimal enderecoanimal)
    {
        _logger.LogInformation("Iniciando criação de endereço animal");

        try
        {
            var animalExiste = await _context.Animals.FirstOrDefaultAsync(a => a.Id_animal == enderecoanimal.Id_animal);

            if (animalExiste != null)
            {
                var enderecoAnimalCriado = await _enderecoAnimalService.CreateEnderecoAnimalAsync(enderecoanimal);

                _logger.LogInformation("Endereço animal criado com sucesso.");

                return Ok(enderecoAnimalCriado);
            }
            else
            {
                _logger.LogWarning("Id animal informado não existe no sistema");

                return BadRequest("Id animal não existe");
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,"Erro ao salvar os dados do endereço animal");

            return BadRequest($"Erro ao salvar os dados: {ex.Message}");
        }
    }

    // DELETE: api/EnderecoAnimal/5

    /// <summary>
    /// Remove dados do endereço animal
    /// </summary>
    /// <param name="id_endereco_animal">Id para pode remover: </param>
    /// <response code="204">Endereco animal removido com sucesso.</response>
    /// <response code="404">Endereco animal não encontrado.</response>
    /// <response code="400">Erro ao processar.</response>
    /// <returns>deletado: </returns>
    [HttpDelete]
    [Route("deleta/enderecoanimal/{id_endereco_animal:int}", Name = "DeletaEnderecoAnimal")]
    public async Task<IActionResult> DeleteEnderecoAnimal(int id_endereco_animal)
    {
        _logger.LogInformation("Iniciando remoção de endereço animal");

        try
        {
            await _enderecoAnimalService.DeleteEnderecoAnimalAsync(id_endereco_animal);

            _logger.LogInformation("EnderecoAnimal foi excluído com sucesso");

            return NoContent();
        }catch(Exception ex)
        {
            _logger.LogError(ex,"Erro em deletar endereço animal.");

            return BadRequest($"Erro em deletar: {ex.Message}");
        }

    }

    private EnderecoAnimalDTO CreateEnderecoAnimalDto(EnderecoAnimal enderecoAnimal)
    {
        var links = new List<LinkDTO>
        {
            new LinkDTO(
                Url.Link("GetEnderecosAnimalsById", new { id_endereco_animal = enderecoAnimal.Id_endereco_animal })!,
                "self",
                "GET"
                ),
            
            new LinkDTO(
                Url.Link("GetAllEnderecosAnimals", null)!,
                "collection",
                "GET"
                ),
            
            new LinkDTO(
                Url.Link("UpdateEnderecoAnimal", new { id_endereco_animal = enderecoAnimal.Id_endereco_animal })!,
                "update",
                "PUT"
                ),
            
            new LinkDTO(
                Url.Link("DeletaEnderecoAnimal", new { id_endereco_animal = enderecoAnimal.Id_endereco_animal })!,
                "delete",
                "DELETE"
                )
        };

        return new EnderecoAnimalDTO(enderecoAnimal, links);
    }
}
