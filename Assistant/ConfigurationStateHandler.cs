using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant
{
    public delegate void ConfigStateHandler(object sender, ConfigurationEventArgs e);

    public class ConfigurationEventArgs
    {
        public string Message { get; private set; }

        public string Property { get; private set; }

        public ConfigurationEventArgs(string _message, string _property)
        {
            Message = _message;
            Property = _property;
        }
    }
}