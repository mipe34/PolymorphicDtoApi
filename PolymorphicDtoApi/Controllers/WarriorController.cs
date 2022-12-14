using Microsoft.AspNetCore.Mvc;
using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Models;
using PolymorphicDtoApi.Polymorph;

namespace PolymorphicDtoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarriorController : Controller
    {
        private static readonly List<BaseWarriorDto> warriors = new List<BaseWarriorDto>
        {
            new PeasantDto{Name = "Morihei Ueshiba"},
            new NinjaDto{Name = "Hatori Hanzo", SpecialAbility = "Poison"},
            new SamuraiDto{Name = "Oda Nobunaga"}
        };


        [HttpGet]
        public IActionResult GetWarrior(string name)
        {
            var warrior = warriors.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));
            if (warrior == null) return NotFound();
            var result = new PolymorphDto<BaseWarriorDto>(WarriorTypeDiscrimination.GetTypeDiscriminator(warrior.GetType()), warrior);
            return Ok(result);
        }

        [HttpGet]
        [Route("list")]
        public IActionResult ListWarriors()
        {
            return Ok(warriors);
        }


        [HttpPost]
        public IActionResult CreateWarrior(PolymorphDto<BaseWarriorDto> warrior)
        {
            warriors.Add(warrior.TypeValue);
            return Ok(warrior);
        }
    }
}
