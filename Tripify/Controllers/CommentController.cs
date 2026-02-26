using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tripify.Dtos.CommentDtos;
using Tripify.Services.CommentServices;

namespace Tripify.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {
            createCommentDto.CommentDate = DateTime.Now;
            createCommentDto.IsStatus = false;
            await _commentService.CreateCommentAsync(createCommentDto);
            return RedirectToAction("CommentList");
        }

        public async Task<IActionResult> CommentListByTourId(string id)
        {
            var values = await _commentService.GetCommentsByTourIdAsync(id);
            return View(values);
        }
    }
}
