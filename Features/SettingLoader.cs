using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using email_api.Database;
using Microsoft.Extensions.Caching.Distributed;

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
    private readonly IDistributedCache _distributedCache;
    private IList<SettingEntity> _allSettings;
    public SettingLoader(EmailContext emailContext, IDistributedCache distributedCache)
    {
        _emailContext = emailContext;
        _distributedCache = distributedCache;
    }

    private async Task<IList<SettingEntity>> GetSettingsAsync()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        var cacheKey = "__settings.all__";
        var settings = await _distributedCache.GetAsync(cacheKey);
        if (settings is null)
        {
            var setting = _emailContext.Setting.ToList();
            await _distributedCache.SetAsync(cacheKey, System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(setting)), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(10)
            });
            watch.Stop();
            Console.WriteLine(string.Format("Database Finished in {0} seconds", watch.Elapsed.TotalSeconds.ToString()));
            return setting;
        }

        watch.Stop();
        Console.WriteLine(string.Format("Cache Finished in {0} seconds", watch.Elapsed.TotalSeconds.ToString()));
        return JsonSerializer.Deserialize<List<SettingEntity>>(System.Text.Encoding.UTF8.GetString(settings));
    }

    public async Task<T> GetSettingValueByKey<T>(string key, T defaultValue = default)
    {
        if (string.IsNullOrEmpty(key))
            return defaultValue;
        if (_allSettings is null)
            _allSettings = await GetSettingsAsync();
        key = key.Trim().ToLowerInvariant();
        var settingByKey = _allSettings.FirstOrDefault(s => s.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
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