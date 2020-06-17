using Microsoft.AspNetCore.Hosting;
using opg_201910_interview.Models;
using opg_201910_interview.Services.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTest
{
    public class UnitTest
    {
        private FileService _fileService;
        private readonly string _clientId = "1001"; //1001, 2001
        private readonly string _clientFolder = "\\UploadFiles\\ClientA"; //ClientA, ClientB

        public UnitTest()
        {
            _fileService = new FileService();
        }

        [Fact]
        public void GetFilesTest()
        {
            var client = new ClientModel()
            {
                Id = _clientId, FileDirectoryPath = _clientFolder
            };

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var rootFolder = $"{AppDomain.CurrentDomain.BaseDirectory.Split("XUnitTest")[0]}\\Exam";

            List<string> fileNames;
            var files = _fileService.GetFiles(client, rootFolder, out fileNames);

            Assert.True(files.Count > 0);
        }

        [Fact]
        public void GetFileDateTimeTest()
        {
            var filename = "blaze-2018-05-01";
            var dateTime = _fileService.GetFileDateTime(_clientId, filename);

            Assert.True(dateTime != null);
        }
    }
}
