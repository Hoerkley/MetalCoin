using Metalcoin.Core.Dtos.Request;
using Metalcoin.Core.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalcoin.Core.Interfaces.Services
{
    public interface ICupomServices
    {
        Task<CuponsResponse> CadastrarCupons(CadastrarCupunsRequest cupom);
        Task<CuponsResponse> AtualizarCupom(AtualizarCupomRequest cupom);
        Task<CuponsResponse> atualizarStatusCupom(AtualizarCupomRequest atualizarCupom);
        Task<bool> DeletarCupons(Guid id);
    }
}
