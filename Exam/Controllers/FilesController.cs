using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using opg_201910_interview.Models;
using opg_201910_interview.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace opg_201910_interview.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {        
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _adminService;

        public FilesController(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration config,
            IFileService adminService
            )
        {            
            _config = config;
            _webHostEnvironment = webHostEnvironment;
            _adminService = adminService;
        }

        [HttpGet]
        public JsonResult GetFiles()
        {
            try
            {
                var numOfClients = 2;
                var rootFolder = _webHostEnvironment.WebRootPath.Replace("wwwroot", "");

                var clients = new List<ClientModel>();
                for (int i = 0; i < numOfClients; i++)
                {
                    var clientId = _config.GetSection($"ClientSettings:{i}:ClientId").Value;
                    var uploadFolder = _config.GetSection($"ClientSettings:{i}:FileDirectoryPath").Value;

                    clients.Add(new ClientModel()
                    {
                        Id = clientId,
                        FileDirectoryPath = uploadFolder
                    });

                    List<string> fileNames;
                    foreach (var client in clients)
                    {
                        client.Files = _adminService.GetFiles(client, rootFolder, out fileNames);
                        client.UnorderedFiles = fileNames;
                    }                        
                }

                var model = new List<object>();
                foreach (var client in clients)
                {
                    model.Add(new
                    {
                        Id = client.Id,
                        SortedFiles = client.Files.Select(r => r.Filename).ToList(),
                        UnorderedFiles = client.UnorderedFiles
                    });
                }

                return new JsonResult(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
