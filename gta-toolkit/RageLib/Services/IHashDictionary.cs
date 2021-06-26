namespace RageLib.Services
{
    /// <summary>
    /// A dictionary to store and resolve hashes
    /// </summary>
    /// <typeparam name="THash">The type of the hash</typeparam>
    /// <typeparam name="TValue">The type of the data being hashed</typeparam>
    public interface IHashDictionary<THash, TValue>
    {
        bool TryAdd(TValue data);

        bool TryGetValue(THash hash, out TValue data);
    }
}
