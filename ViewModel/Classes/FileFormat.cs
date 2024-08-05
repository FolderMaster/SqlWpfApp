using System.Collections.Generic;

namespace ViewModel.Classes
{
    public class FileFormat
    {
        public string Name { get; private set; }

        public IEnumerable<string> Extensions { get; private set; }

        public FileFormat(string name, IEnumerable<string> extensions)
        {
            Name = name;
            Extensions = extensions;
        }
    }
}
