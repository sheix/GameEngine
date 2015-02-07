using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Contracts;

namespace Engine
{
    public abstract class MapRule : IRule
    {
        protected Dictionary<String, String> _parameters;
        protected Random Random;

        protected MapRule()
        {
            Random = new Random();
        }

        public abstract void Process(Grid grid);

        public void LoadParameters(string param)
        {
            _parameters = new Dictionary<string, string>();
            foreach (var pair in param.Split(';'))
            {
                var pairvalues = pair.Split('=');
				if (pairvalues == null)
					continue;
                _parameters.Add(pairvalues[0].ToUpper(), pairvalues[1].ToUpper());
            }
}

        public bool IsDefined(string key)
        {
            return _parameters.ContainsKey(key.ToUpper());
        }

        public string GetValue(string key)
        {
            if (IsDefined(key))
                return _parameters[key.ToUpper()];
            throw new ArgumentException("Can't find value for following key: " + key, key);
        }

		public List<String> GetAllParameterNames()
		{
			List<String> keys = new List<String> ();

			foreach (var k in _parameters.Keys.ToArray()) {

				keys.Add (k);
			}

			return keys;
		}

    }
}