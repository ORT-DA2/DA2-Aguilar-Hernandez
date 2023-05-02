using Blog.Models.In;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("api/comments")]
[ExceptionFilter]
[ServiceFilter(typeof(AuthorizationFilter))]
public class CommentController : ControllerBase{

    private readonly ICommentLogic _commentLogic;
    public CommentController(ICommentLogic commentLogic)
    {
        _commentLogic = commentLogic;
    }


    [HttpPost]
    public IActionResult PostNewComment([FromBody] CommentInModel commentInModel, [FromHeader] Guid Authorization)
    {
            Comment comment = commentInModel.ToEntity();
            Comment result = _commentLogic.AddNewComment(comment, commentInModel.ArticleId, Authorization);
            CommentOutModel commentOut = new CommentOutModel(result);
            return Created("Comment created ",commentOut);
    }

    [HttpDelete]
    public IActionResult DeleteCommentById([FromBody] Guid id)
    {
        try
        {
            _commentLogic.DeleteCommentById(id);
            return Ok($"Comment with the id {id} was deleted");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult ReplyComment([FromBody]Guid id, string reply)
    {
        Comment commentReplied = _commentLogic.ReplyComment(id, reply);
        return Ok(new CommentOutModel(commentReplied));
    }
}