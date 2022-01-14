using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PWADemoProject.Repository.IRepository;
using PWAProject.Hubs;
using PWAProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork moUnitOfWork;
        private readonly IHubContext<BroadcastHub> moBroadcastHub;
        private readonly static int miPageSize = 10;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork foUnitOfWork, IHubContext<BroadcastHub> foBroadcastHub)
        {
            _logger = logger;
            moUnitOfWork = foUnitOfWork;
            moBroadcastHub = foBroadcastHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            string lsSearch = string.Empty;
            int liTotalRecords = 0, liStartIndex = 0, liEndIndex = 0;
            string sort_order = "desc";
            int sort_column = 1;
            int pg = 1;
            int size = 100;
            List<ProductList> loProductList = new List<ProductList>();
            loProductList = moUnitOfWork.Products.GetProduct(sort_column, sort_order, pg, size);
            dynamic loModel = new ExpandoObject();
            loModel.GetProductList = loProductList;
            if (loProductList.Count > 0)
            {
                liTotalRecords = loProductList[0].inRecordCount;
                liStartIndex = loProductList[0].inRownumber;
                liEndIndex = loProductList[loProductList.Count - 1].inRownumber;
            }
            //loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
            return View(loModel);
        }
        public IActionResult Create()
        {
            return View("~/Views/Home/AddProduct.cshtml");
        }
        public IActionResult Edit(int id)
        {
            Product loProduct = moUnitOfWork.Products.GetProductDetail(id);
            return View("~/Views/Home/AddProduct.cshtml", loProduct);
        }


        public async Task<JsonResult> SaveProduct(Product foProduct)
        {
            int liSuccess = 0;
            int liProductId = 0;
            bool flgIsEdit = false;
            moUnitOfWork.Products.SaveProduct(foProduct, out liSuccess, out liProductId);
            if (liSuccess == 101)
            {
                flgIsEdit = false;
            }
            else if(liSuccess==102)
            {
                flgIsEdit = true;
            }
            foProduct.inProductId = liProductId;
            ResponseObject responseObject = new ResponseObject()
            {
                inProductId = foProduct.inProductId,
                stProductName = foProduct.stProductName,
                dcPrice = foProduct.dcPrice,
                dcDiscount = foProduct.dcDiscount,
                inQuantity = foProduct.inQuantity,
                stDescription = foProduct.stDescription,
                flgIsEdit = flgIsEdit
            };
            await Common.SendMessage(moBroadcastHub, responseObject);
            return Json(new { success = liSuccess, productid = liProductId, url = Url.Action("ProductList", "Home") });
        }

        public JsonResult DeleteProduct(int id)
        {
            int liSuccess = 0;
            moUnitOfWork.Products.DeleteProduct(id, out liSuccess);
            return Json(new { success = liSuccess, productid = id });
        }
        /* public  IActionResult GetProductList(int? sort_column, string sort_order, int? pg, int? size)
         {
             try
             {
                 string lsSearch = string.Empty;
                 int liTotalRecords = 0, liStartIndex = 0, liEndIndex = 0;
                 if (sort_column == 0 || sort_column == null)
                     sort_column = 1;
                 if (string.IsNullOrEmpty(sort_order) || sort_order == "desc")
                 {
                     sort_order = "desc";
                     ViewData["sortorder"] = "asc";
                 }
                 else
                 {
                     ViewData["sortorder"] = "desc";
                 }
                 if (pg == null || pg <= 0)
                     pg = 1;
                 if (size == null || size.Value <= 0)
                     size = miPageSize;

                 List<ProductList> loProductList = new List<ProductList>();
                 loProductList = moUnitOfWork.Products.GetProduct(sort_column, sort_order, pg.Value, size.Value);
                 dynamic loModel = new ExpandoObject();
                 loModel.GetProductList = loProductList;
                 if (loProductList.Count > 0)
                 {
                     liTotalRecords = loProductList[0].inRecordCount;
                     liStartIndex = loProductList[0].inRownumber;
                     liEndIndex = loProductList[loProductList.Count - 1].inRownumber;
                 }
                 //loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                 return View("~/Areas/Admin/Views/NexusUsers/_NexusUsersList.cshtml", loModel);
             }
             catch (Exception ex)
             {
                 return RedirectToAction("Index", "Error");
             }
         }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
