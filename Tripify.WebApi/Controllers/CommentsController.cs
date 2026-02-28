using Microsoft.AspNetCore.Mvc;
using Tripify.DTOs.CommentDtos;
using Tripify.WebAPI.Services.CommentServices;

namespace Tripify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var values = await _commentService.GetAllCommentAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(string id)
        {
            var value = await _commentService.GetCommentByIdAsync(id);
            if (value == null)
                return NotFound();
            return Ok(value);
        }

        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetCommentsByTourId(string tourId)
        {
            var values = await _commentService.GetCommentsByTourIdAsync(tourId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {
            await _commentService.CreateCommentAsync(createCommentDto);
            return Ok("Comment created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            await _commentService.UpdateCommentAsync(updateCommentDto);
            return Ok("Comment updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await _commentService.DeleteCommentAsync(id);
            return Ok("Comment deleted successfully");
        }
    }
}
