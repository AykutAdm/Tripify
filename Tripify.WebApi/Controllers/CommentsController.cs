using Microsoft.AspNetCore.Mvc;
using Tripify.DTOs.CommentDtos;
using Tripify.WebApi.Services.OpenAIServices;
using Tripify.WebAPI.Services.CommentServices;

namespace Tripify.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IOpenAIService _openAIService;

        public CommentsController(ICommentService commentService, IOpenAIService openAIService)
        {
            _commentService = commentService;
            _openAIService = openAIService;
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

        [HttpGet("tour/{tourId}/summary")]
        public async Task<IActionResult> GetCommentsSummary(string tourId)
        {
            var comments = await _commentService.GetCommentsByTourIdAsync(tourId);
            
            if (comments == null || !comments.Any())
            {
                return Ok(new { summary = "Henüz yorum bulunmamaktadır." });
            }

            var commentSummaries = comments.Select(c => new CommentSummaryDto
            {
                Headline = c.Headline,
                CommentDetail = c.CommentDetail,
                Score = c.Score
            }).ToList();

            var summary = await _openAIService.GenerateCommentSummaryAsync(commentSummaries);

            return Ok(new { summary });
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
