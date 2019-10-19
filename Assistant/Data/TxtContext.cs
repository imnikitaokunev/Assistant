using System.Collections.Generic;
using System.IO;

namespace Assistant.Data
{
    public class TxtContext : IContext
    {
        public IEnumerable<string> Open(string path)
        {
            var result = new List<string>();
            foreach (var line in File.ReadAllLines(path))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public void Save(IEnumerable<string> phrases, string path)
        {
            File.WriteAllLines(path, phrases);
        }
    }
}
