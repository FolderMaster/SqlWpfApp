using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Interfaces.Services.Document
{
    public interface IDocument
    {
        public double Width { get; set; }

        public double Height { get; set; }

        public object DocumentPaginator { get; }
    }
}
