namespace PolymorphicDtoApi.Models.Warrior.SpecialAbility
{
    public class RideHorseSpecialAbility : BaseSpecialAbility
    {
        public override string Name => "Ride a horse";

        public int Speed { get; set; }
    }
}
