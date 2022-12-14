namespace PolymorphicDtoApi.Polymorph
{
    public interface ITypeDiscriminator
    {
        Type GetType(int typeDiscriminator);

        int GetTypeDiscriminator(Type type);
    }
}
