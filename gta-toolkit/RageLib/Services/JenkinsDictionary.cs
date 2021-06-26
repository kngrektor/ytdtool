using RageLib.Hash;
using System.Collections.Generic;

namespace RageLib.Services
{
    public class JenkinsDictionary : Dictionary<uint, string>, IHashDictionary<uint, string>
    {
        public bool TryAdd(string data)
        {
            return this.TryAdd(Jenkins.Hash(data), data);
        }
    }
}
