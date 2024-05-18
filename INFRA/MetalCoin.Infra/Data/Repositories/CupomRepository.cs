using Metalcoin.Core.Domain;
using Metalcoin.Core.Enums;
using Metalcoin.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MetalCoin.Infra.Data.Repositories
{
    public class CupomRepository : Repository<Cupom>, ICuponsRepository
    {
        public CupomRepository(AppDbContext db) : base(db) { }

        public async Task<Cupom> BuscarPorCodigoCupom(string codigo)
        {
            try
            {
                var response = await DbSet.Where(x => x.Codigo == codigo).SingleOrDefaultAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Cupom>> ObterPorStatus(TipoStatus status)
        {
            var response = await DbSet.Where(c => c.Status == status).ToListAsync();
            return response;
        }
    }
}
