using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckManager.Api.Filters
{
    public class ResponseObject
    {
        private string _fieldName;

        public string fieldName
        {
            get { return _fieldName; }
            set { _fieldName = Camelize(value); }
        }

        public IEnumerable<string> errorMessages { get; set; }

        private string Camelize(string s)
        {
            return string.Join('.', s.Split('.').Select(term => CamelizeImpl(term)));
        }

        private string CamelizeImpl(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length == 1)
                return s.ToLowerInvariant();

            return char.ToLowerInvariant(s[0]) + s.Substring(1);
        }
    }
}
