

using Microsoft.AspNetCore.Mvc;

namespace PolymorphicDtoApi.Polymorph
{
    public static class MvcBuilderExtension
    {
        public static IMvcBuilder AddPolymorphJsonConverter<T>(this IMvcBuilder builder, Func<int, Type> getTypeFunc) where T : class, new() 
        {
            builder.AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new PolymorphJsonConverter<T>(getTypeFunc)));           
            return builder;
        }
    }
}
