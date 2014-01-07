using Application.MainBoundedContext;
using Application.MainBoundedContext.DTO;
using Presentation.WebUI.Areas.Inventory.Models;
using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Presentation.WebUI.Areas.Inventory.Controllers
{
    public class HomeController : Controller
    {
        readonly IInventoryAppService _inventoryAppService;
        public HomeController(IInventoryAppService inventoryAppService)
        {
            Contract.Requires<ArgumentNullException>(inventoryAppService != null, "inventoryAppService");
            
            _inventoryAppService = inventoryAppService;
        }

        //
        // GET: /Inventory/Home/

        public ActionResult Index()
        {
            return View();
        }

        [AsyncTimeout(2500)]
        [HandleError(ExceptionType = typeof(TaskCanceledException), View = "AjaxTimedOut")]
        public async Task<ActionResult> GetInventoryList(int? pageIndex, string sortExpression, string filterKeywords)        
        {
            pageIndex = pageIndex ?? 0;
            var result = await GetInventoryListModel(pageIndex.Value, sortExpression, filterKeywords);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private async Task<InventoryListModel> GetInventoryListModel(int pageIndex, string sortExpression, string filterKeywords)
        {
            const int pageCount = 10;
            int total;

            var field = (InventoryListDto.Fields)Enum.Parse(typeof(InventoryListDto.Fields), sortExpression);
            
            var list = _inventoryAppService.GetInventoryList(pageIndex, pageCount, out total, field, true, filterKeywords);
          
            var model = new InventoryListModel
            {
                NumberOfResults = total,
                InventoryList = list,
                ItemsPerPage = pageCount,
                CurrentPage = pageIndex
            };

            return model;
        }
    }
}
