using System.Collections.Generic;

namespace Assistant.Data
{
    public interface IContext
    {
        IEnumerable<string> Open(string path);
        void Save(IEnumerable<string> phrases, string path);
    }
}