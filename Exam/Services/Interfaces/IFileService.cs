using opg_201910_interview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace opg_201910_interview.Services.Interfaces
{
    public interface IFileService
    {
        List<FileModel> GetFiles(ClientModel client, string rootFolder, out List<string> fileNames);
    }
}
