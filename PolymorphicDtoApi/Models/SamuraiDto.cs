using PolymorphicDtoApi.Code;

namespace PolymorphicDtoApi.Models
{
    public class SamuraiDto : BaseWarriorDto
    {
        public override WarriorTypeEnum TypeDiscriminator => WarriorTypeEnum.Samurai;

        public override string BestAttack => "katana";

    }
}
