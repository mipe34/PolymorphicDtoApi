using Microsoft.AspNetCore.Mvc;
using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Models;

namespace PolymorphicDtoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarriorController : Controller
    {
        private static readonly List<BaseWarriorDto> warriors = new List<BaseWarriorDto>
        {
            new PeasantDto{Name = "Mamoko Otuna"},
            new NinjaDto{Name = "Hatori Hanzo", SpecialAbility = "Poison"},
            new SamuraiDto{Name = "Oda Nobunaga"}
        };


        //[HttpGet]
        //public IActionResult GetWarrior(string warriorType)
        //{
        //    var warrior = warriors.FirstOrDefault(x => string.Equals(x.WarriorType.ToString(), warriorType, StringComparison.InvariantCultureIgnoreCase));
        //    if (warrior == null) return NotFound();
        //    return Ok(warrior);
        //}

        [HttpGet]
        [Route("list")]
        public IActionResult ListWarriors()
        {
            return Ok(warriors);
        }


        [HttpPost]
        public IActionResult CreateWarrior(BaseWarriorDto warrior)
        {
            warriors.Add(warrior);
            return Ok(warrior);
        }
    }
}
