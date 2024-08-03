namespace OrganizationManagement.Services;

public static class ReflectionHelper
{

    /// <summary>
    /// Sets the value of a specified property on an object.
    /// </summary>
    /// <typeparam name="T">The type of the object on which to set the property value.</typeparam>
    /// <param name="obj">The object on which to set the property value.</param>
    /// <param name="propertyName">The name of the property to set.</param>
    /// <param name="value">The value to set to the property.</param>
    /// <remarks>
    /// If the property is not found or is not writable, no action is taken.
    /// </remarks>
    public static void SetPropertyValue<T>(T obj, string propertyName, object value)
    {
        var property = typeof(T).GetProperty(propertyName);
        if (property != null && property.CanWrite)
        {
            property.SetValue(obj, value);
        }
    }

    /// <summary>
    /// Gets the value of a specified property from an object.
    /// </summary>
    /// <typeparam name="T">The type of the object from which to get the property value.</typeparam>
    /// <param name="obj">The object from which to get the property value.</param>
    /// <param name="propertyName">The name of the property to get.</param>
    /// <returns>
    /// The value of the property, or <c>null</c> if the property is not found.
    /// </returns>
    public static object? GetPropertyValue<T>(T obj, string propertyName)
    {
        var property = typeof(T).GetProperty(propertyName);
        return property?.GetValue(obj);
    }

    /// <summary>
    /// Checks whether a specified property exists on a type.
    /// </summary>
    /// <typeparam name="T">The type on which to check for the property.</typeparam>
    /// <param name="propertyName">The name of the property to check for.</param>
    /// <returns>
    /// <c>true</c> if the property exists; otherwise, <c>false</c>.
    /// </returns>
    public static bool PropertyExists<T>(string propertyName)
    {
        return typeof(T).GetProperty(propertyName) != null;
    }
}
