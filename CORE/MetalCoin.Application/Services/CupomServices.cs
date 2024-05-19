using Metalcoin.Core.Domain;
using Metalcoin.Core.Dtos.Request;
using Metalcoin.Core.Dtos.Response;
using Metalcoin.Core.Interfaces.Repositories;
using Metalcoin.Core.Interfaces.Services;

namespace MetalCoin.Application.Services
{
    public class CupomServices : ICupomServices
    {
        private readonly ICuponsRepository _cupomRepository;
        public CupomServices(ICuponsRepository repository) {
            _cupomRepository = repository;
        }

        public async Task<CuponsResponse> AtualizarCupom(AtualizarCupomRequest cupom)
        {
            var cupomDb = await _cupomRepository.ObterPorId(cupom.Id);
            cupomDb.Codigo = cupom.Codigo;
            cupomDb.Descricao = cupom.Descricao;
            cupomDb.Desconto = cupom.Desconto;
            cupomDb.QuantidadeLiberado = cupom.QuantidadeLiberado;
            cupomDb.QuantidadeUsado = cupom.QuantidadeUsado;
            cupomDb.Status = cupom.Status;
            cupomDb.DataValidade = cupom.DataValidade;

            await _cupomRepository.Atualizar(cupomDb);
            var response = new CuponsResponse
            {
                Id = cupomDb.Id,
                Codigo = cupomDb.Codigo,
                Descricao = cupomDb.Descricao,
                Desconto = cupomDb.Desconto,
                QuantidadeLiberado = cupomDb.QuantidadeLiberado,
                QuantidadeUsado = cupomDb.QuantidadeUsado,
                Status = cupomDb.Status,
                DataValidade = cupomDb.DataValidade
            };
            return response;
        }

        public async Task<CuponsResponse> atualizarStatusCupom(AtualizarCupomRequest atualizarCupom)
        {
            var statusDb = await _cupomRepository.ObterPorId(atualizarCupom.Id);
           
            statusDb.Status = atualizarCupom.Status;

            await _cupomRepository.Atualizar(statusDb);
            var response = new CuponsResponse
            {
                Status = statusDb.Status,
            };
            return response;
        }

        public async Task<CuponsResponse> CadastrarCupons(CadastrarCupunsRequest cupom) {

            var cupomExiste = await _cupomRepository.BuscarPorCodigoCupom(cupom.Codigo);

            if (cupomExiste == null)
            {
                return null;
            }

            var cupomEntidade = new Cupom
           {
               Codigo = cupom.Codigo,
               Descricao = cupom.Descricao,
               Desconto = cupom.Desconto,
               QuantidadeLiberado = cupom.QuantidadeLiberado,
               QuantidadeUsado = cupom.QuantidadeUsado,
               Status = cupom.Status,
               DataValidade = cupom.DataValidade
           };
           
            await _cupomRepository.Adicionar(cupomEntidade);

            var response = new CuponsResponse
            {
                Id = cupomEntidade.Id,
                Codigo = cupomEntidade.Codigo,
                Desconto = cupomEntidade.Desconto,
                Descricao = cupomEntidade.Descricao,
                QuantidadeLiberado = cupomEntidade.QuantidadeLiberado,
                QuantidadeUsado = cupomEntidade.QuantidadeUsado,
                Status = cupomEntidade.Status,
                DataValidade = cupomEntidade.DataValidade,
            };
            return response;
        }

        public async Task<bool> DeletarCupons(Guid id)
        {
           var cupom = await _cupomRepository.ObterPorId(id);
            if (cupom == null) {
                return false;
            }
            await _cupomRepository.Remover(id);
            return true;
        }
    }
}
