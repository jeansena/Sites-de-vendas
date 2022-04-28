using AppModel.Data;
using AppModel.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace AppModel.Services
{
    public class DepartmentService
    {
        private readonly AppModelContext _context;

        public DepartmentService(AppModelContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x=> x.Name).ToListAsync();    
        }
    }
}
