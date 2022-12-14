using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Polymorph;

namespace PolymorphicDtoApi.Models.Warrior
{
    public class WarriorTypeDiscriminator : ITypeDiscriminator
    {
        private static readonly Dictionary<WarriorTypeEnum, Type> typeDiscriminationDictionary = new Dictionary<WarriorTypeEnum, Type>()
        {
            { WarriorTypeEnum.Peasant, typeof(PeasantDto)},
            { WarriorTypeEnum.Samurai, typeof(SamuraiDto)},
            { WarriorTypeEnum.Ninja, typeof(NinjaDto)},
        };

        public Type GetType(int typeDiscriminator)
        {
            return typeDiscriminationDictionary[(WarriorTypeEnum)typeDiscriminator];
        }

        public int GetTypeDiscriminator(Type type)
        {
            return (int)typeDiscriminationDictionary.First(x => x.Value == type).Key;
        }
    }
}
