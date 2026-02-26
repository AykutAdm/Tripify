using Tripify.Dtos.CommentDtos;

namespace Tripify.Services.CommentServices
{
    public interface ICommentService
    {
        Task<List<ResultCommentDto>> GetAllCommentAsync();
        Task CreateCommentAsync(CreateCommentDto createCommentDto);
        Task UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task DeleteCommentAsync(string id);
        Task<GetCommentByIdDto> GetCommentByIdAsync(string id);
        Task<List<ResultCommentListByTourIdDto>> GetCommentsByTourIdAsync(string id);
    }
}
