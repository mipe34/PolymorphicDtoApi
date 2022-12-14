using PolymorphicDtoApi.Code;

namespace PolymorphicDtoApi.Models
{
    public class BaseWarriorDto
    {
        public string? Name { get; set; }

        public virtual string BestAttack { get => "hands"; }

        public string Type => this.GetType().Name;
    }
}
