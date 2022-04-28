using AppModel.Models;
using AppModel.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AppModel.Services.Exceptions;

namespace AppModel.Controllers
{
    public class SellersController : Controller
    {
        //implatando idependecias da class SellerService com a
        //sellersController
        private readonly SellerService _sellerServices;
        private readonly DepartmentService _departmentService;



        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _departmentService = departmentService;
            _sellerServices = sellerService;
        }


        public async Task<IActionResult> Index()
        {
            var list = await _sellerServices.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { departments = departments };
            return View(viewModel);
        }

        //metodo post para enviar para o banco de dados
        //açao do botao "create"
        //post e a validaçao autenticacao contra ataque malisiosos CSRF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departmets = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, departments = departmets };
                return View(viewModel);
            }
            await _sellerServices.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not provaided" });
            }

            var obj = await _sellerServices.FindBuIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not found" }); ;
            }

            return View(obj);
        }
        //deleta o vendedor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
            await _sellerServices.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
            }
            catch(IntegrityExceptiom e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        //acao detalhes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not provided" });
            }

            var obj = await _sellerServices.FindBuIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not found" });
            }

            return View(obj);
        }

        //açao EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not provided" });
            }
            var obj = await _sellerServices.FindBuIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id not found" });

            }
            List<Department> department = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, departments = department };
            return View(viewModel);
        }

        //Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {  
                var departmets = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, departments = departmets };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "id mismatch" });
            }
                //trata erros
                try
                {
                    await _sellerServices.UpdateAsync(seller);
                    return RedirectToAction(nameof(Index));
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }

            
         }
            //error
            public IActionResult Error(string message)
            {
                var viewModel = new ErrorViewModel
                {
                    Message = message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View(viewModel);
            }
        

    }
}
