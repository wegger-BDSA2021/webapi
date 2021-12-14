using Data;
using Services;
using System.Threading.Tasks;
using static Data.Response;

namespace api.src.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<Result> AddComment(CommentCreateDTOServer comment)
        {
            try
            {
                var createdComment = await commentRepository.AddComment(comment);

                if (createdComment.Response is BadRequest)
                {
                    return new Result
                    {
                        Response = BadRequest,
                        Message = "Comment is null"
                    };
                }

                return new Result
                {
                    Response = Created,
                    Message = "A new comment was succesfully created",
                    DTO = createdComment.comment
                };
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);

                return new Result
                {
                    Response = InternalError,
                    Message = "Could not process the provided comment ... sorry about that. Try again later."
                };
            }
        }

        public async Task<Result> DeleteComment(int id)
        {
            var commentToDelete = await commentRepository.DeleteComment(id);

            if (commentToDelete == NotFound)
            {
                return new Result
                {
                    Response = NotFound,
                    Message = $"No comment found with id {id}"
                };
            }

            return new Result
            {
                Response = Deleted,
                Message = $"Comment with id {id} has succesfully benn deleted"
            };
        }

        public async Task<Result> GetCommentById(int id)
        {
            var result = await commentRepository.GetCommentById(id);

            if (result.comment == null)
            {
                return new Result
                {
                    Response = NotFound,
                    Message = $"No comment exists with the id {id}"
                };
            }

            return result.Response switch
            {
                NotFound => new Result
                {
                    Response = NotFound,
                    Message = "No comment found with the given entity"
                },
                OK => new Result
                {
                    Response = OK,
                    Message = $"Comment found at index {id}",
                    DTO = result.comment
                },
                _ => new Result
                {
                    Response = Conflict,
                    Message = "An error occured"
                },
            };
        }

        public async Task<Result> GetComments()
        {
            var collection = await commentRepository.GetComments();

            var result = new Result
            {
                Response = OK,
                DTO = collection
            };

            return result;
        }

        public async Task<Result> UpdateComment(CommentUpdateDTO comment)
        {

            var updatedComment = await commentRepository.UpdateComment(comment);

            if (updatedComment == NotFound)
                return new Result
                {
                    Response = NotFound,
                    Message = $"No comment found with the id {comment.Id}"
                };
            return new Result
                {
                Response = Updated,
                Message = $"Comment with id {comment.Id} has succefully been updated"
                };
        }
    }
}