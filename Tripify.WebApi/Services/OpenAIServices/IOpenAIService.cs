using Tripify.DTOs.BookingDtos;
using Tripify.DTOs.CommentDtos;

namespace Tripify.WebApi.Services.OpenAIServices
{
    public interface IOpenAIService
    {
        Task<string> GenerateWhatToExpectAsync(string tourTitle, string tourDescription, string location, string duration);
        Task<string> GenerateCommentSummaryAsync(List<CommentSummaryDto> comments);
        Task<string> GenerateBookingStatusEmailAsync(GetBookingByIdDto booking, string status, string? tourTitle);
    }
}
