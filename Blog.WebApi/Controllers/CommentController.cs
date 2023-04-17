using Blog.BusinessLogic;
using Blog.Models.In;
using Blog.Domain.Entities;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        
        [HttpPost]
        public IActionResult PostNewComment([FromBody] CommentInModel commentInModel)
        {
            var comment = commentInModel.ToEntity();
            var result = _commentService.AddNewComment(comment);

            var commentOut = new CommentOutModel(result);
            return new OkObjectResult(commentOut);
        }
    }    
}
