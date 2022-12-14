namespace PolymorphicDtoApi.Polymorph
{
    public record PolymorphDto<T> where T : class, new()
    {
        public int TypeDiscriminator { get; private set; }

        public T TypeValue { get; private set; }

        public PolymorphDto(int typeDiscriminator, T typeValue)
        {
            TypeDiscriminator = typeDiscriminator;
            TypeValue = typeValue;
        }
    }
}
