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
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                              //Check if Property is Decorated with the NoMapAttribute
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                IgnoreMapperAttribute attribute = (IgnoreMapperAttribute)descriptor.Attributes[typeof(IgnoreMapperAttribute)];
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
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
