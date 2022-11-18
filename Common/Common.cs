using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

public static class Common
{
    public static string NewShortGuid()
    {
        return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
    }
    public static string RandomCode(int length)
    {
        Random rd = new Random();
        const string allowedChars = "0123456789";
        char[] chars = new char[length];

        for (int i = 0; i < length; i++)
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];

        return new string(chars);
    }
    public static string RandomRefCode(int length)
    {
        Random rd = new Random();
        const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        char[] chars = new char[length];

        for (int i = 0; i < length; i++)
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];

        return new string(chars);
    }

    public static T To<T>(object value)
    {
        return (T)To(value, typeof(T));
    }

    public static object To(object value, Type destinationType)
    {
        return To(value, destinationType, CultureInfo.InvariantCulture);
    }

    public static object To(object value, Type destinationType, CultureInfo culture)
    {
        if (value == null)
            return null;

        var sourceType = value.GetType();

        var destinationConverter = TypeDescriptor.GetConverter(destinationType);
        if (destinationConverter.CanConvertFrom(value.GetType()))
            return destinationConverter.ConvertFrom(null, culture, value);

        var sourceConverter = TypeDescriptor.GetConverter(sourceType);
        if (sourceConverter.CanConvertTo(destinationType))
            return sourceConverter.ConvertTo(null, culture, value, destinationType);

        if (destinationType.IsEnum && value is int)
            return Enum.ToObject(destinationType, (int)value);

        if (!destinationType.IsInstanceOfType(value))
            return Convert.ChangeType(value, destinationType, culture);

        return value;
    }
}