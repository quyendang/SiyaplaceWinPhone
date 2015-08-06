using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.Model
{
    public class FileBody
    {
        private File file;
        private string filename;
        public FileBody(File file, string filename, string mimeType, string charset)
        {
           // this(file, ContentType.create(mimeType, charset), filename);
        }
        public FileBody(File file,string mimeType,string charset)
        {

        }
    }
}
