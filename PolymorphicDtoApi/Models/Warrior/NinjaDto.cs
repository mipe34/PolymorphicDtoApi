using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Models.Warrior.SpecialAbility;

namespace PolymorphicDtoApi.Models.Warrior
{
    public class NinjaDto : BaseWarriorDto
    {
        public override string BestAttack => "shuriken";

        public PoisonSpecialAbility? SpecialAbility { get; set; } = new PoisonSpecialAbility();
    }
}
