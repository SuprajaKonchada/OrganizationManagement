using System.Text.Json;
using System.Xml.Serialization;

namespace OrganizationManagement.Services;
public static class SerializationHelper
{
    /// <summary>
    /// Serializes an object to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>A JSON string representation of the object.</returns>
    public static string SerializeToJson<T>(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    /// <summary>
    /// Deserializes a JSON string to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>An object of type T.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the JSON input is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
    public static T? DeserializeFromJson<T>(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentNullException(nameof(json), "JSON input cannot be null or empty.");
        }

        try
        {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (JsonException ex)
        {
            // Handle JSON deserialization errors
            throw new InvalidOperationException("Deserialization failed", ex);
        }
    }

    /// <summary>
    /// Serializes an object to an XML string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>An XML string representation of the object.</returns>
    public static string SerializeToXml<T>(T obj)
    {
        using var stringWriter = new StringWriter();
        var serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(stringWriter, obj);
        return stringWriter.ToString();
    }

    /// <summary>
    /// Deserializes an XML string to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="xml">The XML string to deserialize.</param>
    /// <returns>An object of type T.</returns>
    public static T? DeserializeFromXml<T>(string xml)
    {
        using var stringReader = new StringReader(xml);
        var serializer = new XmlSerializer(typeof(T));
        return (T?)serializer.Deserialize(stringReader);
    }
}

