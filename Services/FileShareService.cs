﻿using Azure.Storage.Files.Shares.Models;
using Azure.Storage.Files.Shares;
using Azure;
using System.IO;
using st10293982CLD6212POE1.Models;

namespace st10293982CLD6212POE1.Services
{
    public class FileShareService
    {
        private readonly string _connectionString;
        private readonly string _fileShareName;

        public FileShareService(string connectionString, string fileShareName)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _fileShareName = fileShareName ?? throw new ArgumentNullException(nameof(fileShareName));
        }

        public async Task UploadFileAsync(string directoryName, string fileName, Stream fileStream)
        {
            try
            {
                var serviceClient = new ShareServiceClient(_connectionString);
                var shareClient = serviceClient.GetShareClient(_fileShareName);

                var directoryClient = shareClient.GetDirectoryClient(directoryName);
                await directoryClient.CreateIfNotExistsAsync();


                var fileClient = directoryClient.GetFileClient(fileName);

                await fileClient.CreateAsync(fileStream.Length);
                await fileClient.UploadRangeAsync(new HttpRange(0, fileStream.Length), fileStream);

                Console.WriteLine($"File '{fileName}' uploaded to '{directoryName}' in file share '{_fileShareName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
        }

        public async Task<Stream> DownloadFileAsync(string directoryName, string fileName)
        {
            try
            {
                var serviceClient = new ShareServiceClient(_connectionString);
                var shareClient = serviceClient.GetShareClient(_fileShareName);

                var directoryClient = shareClient.GetDirectoryClient(directoryName);
                var fileClient = directoryClient.GetFileClient(fileName);

                var downloadInfo = await fileClient.DownloadAsync();
                return downloadInfo.Value.Content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
                return null;
            }
        }

        public async Task<List<FileModel>> ListFilesAsync(string directoryName)
        {
            var fileModels = new List<FileModel>();

            try
            {
                var serviceClient = new ShareServiceClient(_connectionString);
                var shareClient = serviceClient.GetShareClient(_fileShareName);

                var directoryClient = shareClient.GetDirectoryClient(directoryName);

                await foreach (ShareFileItem item in directoryClient.GetFilesAndDirectoriesAsync())
                {
                    if (!item.IsDirectory)
                    {
                        var fileClient = directoryClient.GetFileClient(item.Name);
                        var properties = await fileClient.GetPropertiesAsync();

                        fileModels.Add(new FileModel
                        {
                            Name = item.Name,
                            Size = properties.Value.ContentLength,
                            LastModified = properties.Value.LastModified
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing files: {ex.Message}");
                throw;
            }

            return fileModels;
        }
    }
}
