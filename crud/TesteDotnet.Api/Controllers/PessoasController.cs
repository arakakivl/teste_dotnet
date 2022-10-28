using Microsoft.AspNetCore.Mvc;
using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Services.Interfaces;

namespace TesteDotnet.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;
    public PessoasController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    [HttpPost("/")]
    public async Task<IActionResult> CreateAsync([FromBody] NewPessoaInputModel model)
    {    
        var id = await _pessoaService.CreatePessoaAsync(model);
        var created = await _pessoaService.GetPessoaAsync(id);

        return CreatedAtAction(nameof(GetAsync), new { Id = id }, created);
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var p = await _pessoaService.GetPessoaAsync(id);
        if (p is null)
            return NotFound();
            
        return Ok((await _pessoaService.GetPessoaAsync(id)));
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _pessoaService.GetPessoasAsync());
    }

    [HttpPut("/{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] NewPessoaInputModel model)
    {
        var exists = (await _pessoaService.GetPessoaAsync(id)) != null;
        if (exists)
        {
            await _pessoaService.UpdatePessoaAsync(id, model);
            return NoContent();
        }
        
        return NotFound();
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
    {
        var exists = (await _pessoaService.GetPessoaAsync(id)) != null;
        if (exists)
        {
            await _pessoaService.DeletePessoaAsync(id);
            return NoContent();
        }
        
        return NotFound();
    }
}