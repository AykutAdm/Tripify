using Tripify.Dtos.CommentDtos;

namespace Tripify.Services.CommentServices
{
    public class CommentManager : ICommentService
    {
        public Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultCommentDto>> GetAllCommentAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetCommentByIdDto> GetCommentByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            throw new NotImplementedException();
        }
    }
}
