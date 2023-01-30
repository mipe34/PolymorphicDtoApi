using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Models.Warrior.SpecialAbility;

namespace PolymorphicDtoApi.Models.Warrior
{
    public class SamuraiDto : BaseWarriorDto
    {
        public override string BestAttack => "katana";

        public RideHorseSpecialAbility SpecialAbility { get; set; } = new RideHorseSpecialAbility();
    }
}
