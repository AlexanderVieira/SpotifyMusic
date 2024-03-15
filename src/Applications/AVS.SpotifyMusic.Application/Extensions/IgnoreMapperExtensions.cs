using AutoMapper;
using AVS.SpotifyMusic.Domain.Contas;
using System.ComponentModel;

namespace AVS.SpotifyMusic.Application.Extensions
{
    public static class IgnoreMapperExtensions
    {
        //Method is Generic and Hence we can use with any TSource and TDestination Type
        public static IMappingExpression<TSource, TDestination> IgnoreMapperp<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            //Fetching Type of the TSource
            var sourceType = typeof(TSource);
            //Fetching All Properties of the Source Type using GetProperties() method
            foreach (var property in sourceType.GetProperties())
            {
                //Get the Property Name
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                //Check if Property is Decorated with the NoMapAttribute
                IgnoreMapperAttribute attribute = (IgnoreMapperAttribute)descriptor.Attributes[typeof(IgnoreMapperAttribute)];
                if (attribute != null)
                {
                    //If Property is Decorated with NoMap Attribute, call the Ignore Method
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
