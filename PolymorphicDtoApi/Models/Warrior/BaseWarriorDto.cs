using PolymorphicDtoApi.Code;

namespace PolymorphicDtoApi.Models.Warrior
{
    public class BaseWarriorDto
    {
        public string? Name { get; set; }

        public virtual string BestAttack { get => "hands"; }

        public string Type => GetType().Name;
    }
}
