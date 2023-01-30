namespace PolymorphicDtoApi.Models.Warrior.SpecialAbility
{
    public class PoisonSpecialAbility : BaseSpecialAbility
    {
        public override string Name => "Poison";

        public int PoisonStrength { get; set; }
    }
}
