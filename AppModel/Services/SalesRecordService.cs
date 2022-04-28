using AppModel.Data;
using AppModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AppModel.Services
{
    public class SalesRecordService
    {
        private readonly AppModelContext _context;

        public SalesRecordService(AppModelContext context)
        {
            _context = context;
        }
        //operacoa assicronar usa "async", "Task", "await"
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date >= maxDate.Value);
            }
            return await result
                //faz as busca mas tabelas seller e department
                .Include(x => x.Seller)
                .Include(x => x.Seller.department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date >= maxDate.Value);
            }
            return await result
                //faz as busca mas tabelas seller e department
                .Include(x => x.Seller)
                .Include(x => x.Seller.department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.department)
                .ToListAsync();

        }
    }
}