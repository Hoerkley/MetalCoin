using Metalcoin.Core.Dtos.Request;
using Metalcoin.Core.Enums;
using Metalcoin.Core.Interfaces.Repositories;
using Metalcoin.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetalCoin.Api.Controllers
{
    [ApiController]
    public class CuponsController : ControllerBase
    {
        private readonly ICuponsRepository _cuponsRepository;
        private readonly ICupomServices _cupomServices;

        public CuponsController(ICuponsRepository cuponsRepository, ICupomServices cupomServices) {
            _cuponsRepository = cuponsRepository;
            _cupomServices = cupomServices;
        }


        [HttpGet]
        [Route("todos")]
        public async Task<ActionResult> ObterTodosCupons() {
            var listaCupons = await _cuponsRepository.ObterTodos();

            if (listaCupons.Count == 0) {
                return NoContent();
            }

            return Ok(listaCupons);
        }
        [HttpGet]
        [Route("ativos")]
        public async Task<ActionResult> BuscarCuponsstatus(TipoStatus status){
            var listaCuponsAtivos = await _cuponsRepository.ObterPorStatus(status);
            if (listaCuponsAtivos == null) {
                return BadRequest("Não encontrado");
            }
            return Ok(listaCuponsAtivos);
        }

        [HttpGet]
        [Route("buscarUmCupons")]
        public async Task<ActionResult> ObterUmCupom(Guid id)
        {
            var cupom = await _cuponsRepository.ObterPorId(id);
            if (cupom == null)
            {
                return BadRequest("Cupom não encontrado");
            }
            return Ok(cupom);
        }

        [HttpPost]
        [Route("/cadastrar")]
        public async Task<ActionResult> CadastarCupons([FromBody] CadastrarCupunsRequest cadastrarCupuns){
            if (cadastrarCupuns == null) {
                return BadRequest("informe o cupom"); 
            }
            var response = _cupomServices.CadastrarCupons(cadastrarCupuns);
            if (response == null) {
                return BadRequest("Cupon já exixte");
            }
            return Created("cadastrar", response);
        }
        [HttpPatch]
        [Route("atualizar")]
        public async Task<ActionResult> AtualizarCupum([FromBody] AtualizarCupomRequest atualizarCupom){
            if (atualizarCupom == null) {
                return BadRequest("Nenhum valor encontrado");
            }

            var response = await _cupomServices.AtualizarCupom(atualizarCupom);
            return Ok(response);
        }


        [HttpDelete]
        [Route("deletar/{id:guid}")]
        public async Task<ActionResult> RemoverCupom(Guid id){
            if (id == Guid.Empty) {
                return BadRequest("Id não encontrado");
            }
            var response = await _cupomServices.DeletarCupons(id);
            if (!response) {
                return BadRequest("Cupum não existe");
            }
            return Ok("Cupom deletado");
        }
    }
}
