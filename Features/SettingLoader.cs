using System.ComponentModel;
using email_api.Database;

public interface ISettingService
{
    Task<T> LoadSettingAsync<T>() where T : ISettings, new();
}
public interface ISettings
{
}
public class SettingLoader : ISettingService
{
    private readonly EmailContext _emailContext;
    public SettingLoader(EmailContext emailContext)
    {
        _emailContext = emailContext;
    }

    public IList<Setting> GetSettings()
    {
        return _emailContext.Setting.ToList(); ;
    }

    public async Task<T> GetSettingValueByKey<T>(string key, T defaultValue = default)
    {
        if (string.IsNullOrEmpty(key))
            return defaultValue;

        var allSettings = GetSettings();
        key = key.Trim().ToLowerInvariant();
        var settingByKey = allSettings.FirstOrDefault(s => s.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        if (settingByKey == null)
            return defaultValue;

        return Common.To<T>(settingByKey.Value);
    }

    public async Task<T> LoadSettingAsync<T>() where T : ISettings, new()
    {
        // class.property
        var settings = Activator.CreateInstance<T>();

        foreach (var prop in typeof(T).GetProperties())
        {
            if (!prop.CanRead || !prop.CanWrite)
                continue;

            var key = typeof(T).Name + "." + prop.Name;
            var settingValue = await GetSettingValueByKey<string>(key);
            if (settingValue == null)
                continue;

            if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                continue;

            if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(settingValue))
                continue;

            object value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(settingValue);

            //set property
            prop.SetValue(settings, value, null);
        }

        return settings;
    }

}