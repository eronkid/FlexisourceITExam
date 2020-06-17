using opg_201910_interview.Models;
using opg_201910_interview.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace opg_201910_interview.Services.Services
{
    public class FileService : IFileService
    {
        public List<FileModel> GetFiles(ClientModel client, string rootFolder, out List<string> fileNames)
        {
            try
            {
                var folderPath = $"{rootFolder}{client.FileDirectoryPath.Replace("/", "\\")}";
                var paths = Directory.GetFiles(folderPath, GetSearchPattern(client.Id));

                fileNames = new List<string>();
                foreach (var path in Directory.GetFiles(folderPath))
                    fileNames.Add(Path.GetFileName(path));

                var files = new List<FileModel>();
                foreach (var path in paths)
                {
                    var name = Path.GetFileNameWithoutExtension(path);
                    var filename = Path.GetFileName(path);

                    files.Add(new FileModel()
                    {
                        Name = name,
                        Filename = filename,
                        DateCreated = GetFileDateTime(client.Id, name)
                    });
                }

                var sortedFiles = SortFileNames(client, files);
                return sortedFiles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DateTime GetFileDateTime(string clientId, string filename)
        {
            string[] date;
            DateTime dateTime;

            switch (clientId)
            {
                case "1001":
                    date = filename.Split("-");
                    dateTime = DateTime.Parse($"{date[1]}-{date[2]}-{date[3]}");
                    return dateTime;
                case "2001":
                    date = filename.Split("_");
                    var year = date[1].Remove(4, 4);
                    var month = date[1].Remove(6).Remove(0, 4);
                    var day = date[1].Remove(0, 6);
                    dateTime = DateTime.Parse($"{year}-{month}-{day}");
                    return dateTime;
                default:
                    return DateTime.Now;
            }
        }

        private List<FileModel> SortFileNames(ClientModel client, List<FileModel> files)
        {
            var sortedFiles = new List<FileModel>();

            var sortNameOrder = GetOrderByNames(client.Id);
            foreach (var name in sortNameOrder.Split(","))
            {
                var fs = files.Where(r => r.Name.Contains(name)).OrderBy(r => r.DateCreated).ToList();
                sortedFiles.AddRange(fs);
            }

            return sortedFiles;
        }

        private string GetOrderByNames(string clientId)
        {
            switch (clientId)
            {
                case "1001":
                    return "shovel,waghor,blaze,discus";
                case "2001":
                    return "orca,widget,eclair,talon";
                default:
                    return string.Empty;
            }
        }

        private string GetSearchPattern(string clientId)
        {
            switch (clientId)
            {
                case "1001":
                    return "*-*-*-*.xml";
                case "2001":
                    return "*_*.xml";
                default:
                    return string.Empty;
            }
        }
    }
}
