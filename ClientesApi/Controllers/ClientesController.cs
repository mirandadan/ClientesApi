using ClientesApi.Models;
using ClientesApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> GetClientes()
        {
            //o mais indicado é usar um tratamento de erros global e deixar o controller limpo
            try
            {
                var clientes = await _clienteService.GetClientes();
                return Ok(clientes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao obter clientes");
            }
        }

        [HttpGet("ClientesPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>>
            GetClientesByName([FromQuery] string nome)
        {
            try
            {
                var clientes = await _clienteService.GetClienteByNome(nome);
                if (clientes == null)
                    return NotFound($"Não existem clientes com o critério {nome}");

                return Ok(clientes);
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("{id:int}", Name="GetCliente")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            try
            {
                var cliente =  await _clienteService.GetCliente(id);

                if (cliente == null)
                    return NotFound($"Não existe aluno com o id = {id}");

                return Ok(cliente);
            }
            catch 
            {
                return BadRequest("Request Inválido");
            }
        }
        [HttpPost]
        public async Task <ActionResult> Create(Cliente cliente)
        {
            try
            {
                await _clienteService.CreateCliente(cliente);
                return CreatedAtRoute(nameof(GetCliente), new { id = cliente.Id }, cliente);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if(cliente.Id == id)
                {
                    await _clienteService.UpdateCliente(cliente);
                    return Ok($"Cliente com o id={id} foi atualizado com sucesso");
                }

                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _clienteService.GetCliente(id);
                if(cliente != null)
                {
                    await _clienteService.DeleteCliente(cliente);
                    return Ok($"Cliente de id = {id} foi excluído com sucesso");
                }

                else
                {
                    return NotFound($"Cliente com Id {id} não encontrado");
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }



    }
}
