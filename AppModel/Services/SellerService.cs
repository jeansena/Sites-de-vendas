using AppModel.Data;
using AppModel.Models;
using AppModel.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AppModel.Services
{
    public class SellerService 
    {
        private readonly AppModelContext _context;

        public SellerService(AppModelContext context)
        {
            _context = context;
        }


        //um tipo de filtro sincromo
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();    
        }

        //inserir no banco de dado
        //o formulario do vendedor 
        //o botao "Create"
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
           await _context.SaveChangesAsync();

        }

        //deletar o vendedor
        public async Task<Seller> FindBuIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.department).FirstOrDefaultAsync(odj => odj.Id == id);
        }
        // remove
        public async Task RemoveAsync(int id)
        {
            try
            {

                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new IntegrityExceptiom(e.Message);
            }    

        }

        //atualizar
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);  
            if(hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }


    }
}
