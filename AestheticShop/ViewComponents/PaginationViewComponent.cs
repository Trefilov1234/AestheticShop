using AestheticShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AestheticShop.ViewComponents
{
    public class PaginationViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalPages,int limit,int? tagId,int? categoryId,string action,string controller)
        {
            PaginationViewModel paginationViewModel = new PaginationViewModel()
            {
                TotalPages=totalPages,
                CurrentPage=currentPage,
                LimitItem=limit,
                Action=action,
                Controller=controller,
                TagId=tagId,
                CategoryId=categoryId,
            };
            return View("Pagination", paginationViewModel);
        }
    }
}
