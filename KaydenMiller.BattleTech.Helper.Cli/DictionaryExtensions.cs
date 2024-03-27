namespace KaydenMiller.BattleTech.Helper.Cli;

public static class DictionaryExtensions
{
    public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) where TKey : notnull
    {
        return dict.GetValueOrDefault(key) ?? throw new Exception();
    }
}