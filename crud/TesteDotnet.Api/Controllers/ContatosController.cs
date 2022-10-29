using Microsoft.AspNetCore.Mvc;
using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Services.Interfaces;

namespace TesteDotnet.Api.Controllers;

[ApiController]
[Route("/api/contatos")]
public class ContatosController : ControllerBase
{
    private readonly IPessoaService _pessoaService;
    private readonly IContatoService _contatoService;

    public ContatosController(IPessoaService pessoaService, IContatoService contatoService)
    {
        _pessoaService = pessoaService;
        _contatoService = contatoService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateAsync([FromBody] NewContatoInputModel model)
    {
        var pessoaExists = await _pessoaService.GetPessoaAsync(model.PessoaId) != null;
        var alreadyExists = await _contatoService.GetContatoAsync(model.PessoaId, model.Celular) != null;
        
        if (alreadyExists || !pessoaExists)
            return BadRequest();

        await _contatoService.CreateContatoAsync(model);
        return Ok();
    }

    [HttpGet("{pessoaId}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid pessoaId, [FromQuery] long? celular)
    {
        var pessoaExists = await _pessoaService.GetPessoaAsync(pessoaId) != null;
        if (!pessoaExists)
            return BadRequest();

        if (celular is null)
            return Ok(await _contatoService.GetContatosAsync(pessoaId));
        else
            return Ok(await _contatoService.GetContatoAsync(pessoaId, (long)celular));
    }
    
    [HttpPut("{pessoaId}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid pessoaId, [FromBody] NewContatoInputModel model)
    {
        var pessoaExists = await _pessoaService.GetPessoaAsync(pessoaId) != null;
        var old = await _contatoService.GetContatoAsync(pessoaId, model.OldCelular);

        if (old is null || !pessoaExists)
            return NotFound();
        
        await _contatoService.UpdateContatoAsync(pessoaId, model);
        return NoContent();
    }

    [HttpDelete("{pessoaId}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid pessoaId, [FromQuery] long celular)
    {
        var pessoaExists = await _pessoaService.GetPessoaAsync(pessoaId) != null;
        var toDelete = await _contatoService.GetContatoAsync(pessoaId, celular);
        
        if (toDelete is null || !pessoaExists)
            return NotFound();

        await _contatoService.DeleteContatoAsync(pessoaId, celular);
        return NoContent();
    }
}
