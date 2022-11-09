public static class Extensions
{
    public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<TKey, TValue> invoke)
    {
        foreach(var kvp in dictionary)
            invoke(kvp.Key, kvp.Value);
    }
}