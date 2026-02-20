using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tripify.Services.TourServices;

namespace Tripify.ViewComponents.TourViewComponents
{
    public class _TourListComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _TourListComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1)
        {
            int pageSize = 3;
            var allValues = await _tourService.GetAllTourAsync();

            var totalCount = allValues.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pagedValues = allValues
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedValues);
        }
    }
}
