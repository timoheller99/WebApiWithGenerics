namespace WebApiWithGenerics.WebApi.Contracts.Common
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public abstract class DbContract<T>
    {
        [SuppressMessage("", "MA0018", Justification = "Used to determine database entity name.")]
        public static string GetEntityName()
        {
            var displayNameAttribute = typeof(T).CustomAttributes.FirstOrDefault(attribute => attribute.AttributeType == typeof(DisplayNameAttribute));

            if (displayNameAttribute == null)
            {
                throw new Exception($"Attribute {nameof(DisplayNameAttribute)} is missing for class {typeof(T).Name}.");
            }

            var displayName = displayNameAttribute.ConstructorArguments.First();

            if (displayName.Value == null)
            {
                throw new Exception($"Attribute {nameof(DisplayNameAttribute)} for class {typeof(T).Name} must have a value.");
            }

            var displayNameValue = displayName.Value.ToString();
            return displayNameValue;
        }
    }
}
