using PolymorphicDtoApi.Code;

namespace PolymorphicDtoApi.Models
{
    public class NinjaDto : BaseWarriorDto
    {
        public override string BestAttack => "shuriken";      

        public string? SpecialAbility { get; set; }
    }
}
