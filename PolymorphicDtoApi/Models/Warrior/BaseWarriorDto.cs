using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Models.Warrior.SpecialAbility;

namespace PolymorphicDtoApi.Models.Warrior
{
    public class BaseWarriorDto
    {
        public string? Name { get; set; }

        public virtual string BestAttack { get => "hands"; }

        public string Type => GetType().Name;


    }
}
