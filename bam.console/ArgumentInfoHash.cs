/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Console
{
    public class ArgumentInfoHash
    {
        readonly Dictionary<string, ArgumentInfo> _innerHash;
        public ArgumentInfoHash(ArgumentInfo[] argumentInfo)
        {
            _innerHash = new Dictionary<string, ArgumentInfo>(argumentInfo.Length);
            foreach (ArgumentInfo info in argumentInfo)
            {
                if (_innerHash.ContainsKey(info.Name))
                    _innerHash[info.Name] = info;
                else
                    _innerHash.Add(info.Name, info);
            }
        }
        public string[] ArgumentNames
        {
            get
            {
                List<string> returnValue = new List<string>(_innerHash.Keys.Count);
                foreach (string name in _innerHash.Keys)
                {
                    returnValue.Add(name);
                }
                return returnValue.ToArray();
            }
        }

        public ArgumentInfo? this[string argumentName]
        {
            get
            {
                if (_innerHash.ContainsKey(argumentName))
                    return _innerHash[argumentName];

                return null;
            }
        }
    }
}
