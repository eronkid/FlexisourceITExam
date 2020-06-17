using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace opg_201910_interview.Models
{
    public class ClientModel
    {
        public string Id { get; set; }
        public string FileDirectoryPath { get; set; }
        public List<string> UnorderedFiles { get; set; }
        public List<FileModel> Files { get; set; }
    }
}
