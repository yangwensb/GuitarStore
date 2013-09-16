using Application.MainBoundedContext;
using Application.MainBoundedContext.DTO;
using Presentation.WebUI.Areas.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.WebUI.Areas.Inventory.Controllers
{
    public class HomeController : Controller
    {
        IInventoryAppService _inventoryAppService;
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
            int pageCount = 10;
            int total;

            var field = (InventoryListDTO.Filds)Enum.Parse(typeof(InventoryListDTO.Filds), sortExpression);
            
            var list = _inventoryAppService.GetInventoryList(pageIndex, pageCount, out total, field, true, filterKeywords);
          
            var model = new InventoryListModel();
            model.NumberOfResults = total;
            model.InventoryList = list;
            model.ItemsPerPage = pageCount;
            model.CurrentPage = pageIndex;

            return model;
        }
    }
}
