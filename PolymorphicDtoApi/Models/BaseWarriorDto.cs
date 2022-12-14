using PolymorphicDtoApi.Code;

namespace PolymorphicDtoApi.Models
{
    public class BaseWarriorDto
    {
        public string? Name { get; set; }

        public virtual WarriorTypeEnum TypeDiscriminator { get; }

        public virtual string BestAttack { get => string.Empty; }
    }
}
