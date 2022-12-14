using PolymorphicDtoApi.Code;

namespace PolymorphicDtoApi.Models
{
    public class WarriorTypeDiscrimination
    {
        private static readonly Dictionary<WarriorTypeEnum, Type> typeDiscriminationDictionary = new Dictionary<WarriorTypeEnum, Type>()
        {
            { WarriorTypeEnum.Peasant, typeof(PeasantDto)},
            { WarriorTypeEnum.Samurai, typeof(SamuraiDto)},
            { WarriorTypeEnum.Ninja, typeof(NinjaDto)},
        };

        public static Type GetType(int typeDiscriminator)
        {
            return typeDiscriminationDictionary[(WarriorTypeEnum)typeDiscriminator];
        }

        public static int GetTypeDiscriminator(Type type)
        {
            return (int) typeDiscriminationDictionary.First(x=> x.Value == type).Key;
        }
    }
}
