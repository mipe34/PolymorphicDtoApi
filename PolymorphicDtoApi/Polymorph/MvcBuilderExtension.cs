

using Microsoft.AspNetCore.Mvc;

namespace PolymorphicDtoApi.Polymorph
{
    public static class MvcBuilderExtension
    {
        public static IMvcBuilder AddPolymorphJsonConverter<T>(this IMvcBuilder builder, ITypeDiscriminator typeDiscriminator) where T : class, new() 
        {
            builder.AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new PolymorphJsonConverter<T>(typeDiscriminator)));           
            return builder;
        }
    }
}
