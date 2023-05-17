using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.Models.In;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [AuthenticationRoleFilter(Roles = new[] { Role.Admin })]
    public class OffensiveWordsController : ControllerBase
    {
        private readonly IOffensiveWordLogic _offensiveLogic;

        public OffensiveWordsController(IOffensiveWordLogic offensiveLogic)
        {
            _offensiveLogic = offensiveLogic;
        }
        
        [HttpGet]
        public IActionResult GetAllOffensiveWords()
        {
            return Ok(_offensiveLogic.GetAllOffensiveWords());
        }        
        
        [HttpPost]
        public IActionResult CreateOffensiveWord([FromBody]OffensiveWordDTO offensiveWord)
        {
            OffensiveWord newOffensiveWord = _offensiveLogic.CreateOffensiveWord(offensiveWord.Word);
            return Created($"api/offensiveWords/{newOffensiveWord.Id}", newOffensiveWord);
        }
        
        [HttpDelete]
        public IActionResult DeleteOffensiveWord([FromBody]string offensiveWord)
        {
            _offensiveLogic.DeleteOffensiveWord(offensiveWord);
            return Ok($"{offensiveWord} was deleted");
        }
    }
}
