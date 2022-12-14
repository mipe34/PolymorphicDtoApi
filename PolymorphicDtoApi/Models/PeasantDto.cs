using PolymorphicDtoApi.Code;

namespace PolymorphicDtoApi.Models
{
    public class PeasantDto : BaseWarriorDto
    {
        public override WarriorTypeEnum TypeDiscriminator => WarriorTypeEnum.Peasant;
    }
}
