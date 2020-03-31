using System;
using System.ComponentModel;
using System.Reflection;

namespace Mvc
{
public class ModelMetadata
{
    public ParameterInfo Parameter { get; }
    public PropertyInfo Property { get; }
    public Type ModelType { get; }
    public bool CanConvertFromString { get; }
    private ModelMetadata(ParameterInfo parameter, PropertyInfo property)
    {
        Parameter = parameter;
        Property = property;
        ModelType = parameter?.ParameterType ?? property.PropertyType;
        CanConvertFromString = TypeDescriptor.GetConverter(ModelType).CanConvertFrom(typeof(string));
    }

    public static ModelMetadata CreateByParameter(ParameterInfo parameter)
        => new ModelMetadata(parameter, null);

    public static ModelMetadata CreateByProperty(PropertyInfo  property)
        => new ModelMetadata(null, property);
}
}
