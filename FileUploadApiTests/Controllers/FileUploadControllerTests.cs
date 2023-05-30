using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using NSubstitute;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileUploadApi.Controllers.Tests
{
    [TestClass]
    public class FileUploadControllerTests
    {
        [TestMethod]
        public async Task Upload_ValidFile_ReturnsOk()
        {
            // Arrange
            var fileMock = Substitute.For<IFormFile>();
            fileMock.FileName.Returns("testfile.txt");
            fileMock.Length.Returns(100); // Set the desired file length

            var fileUploadServiceMock = Substitute.For<IFileUploadService>();
            fileUploadServiceMock.UploadFile(fileMock).Returns("File uploaded successfully.");

            var controller = new FileUploadController(fileUploadServiceMock);

            // Act
            var result = await controller.Upload(fileMock);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("File uploaded successfully.");
        }

        [TestMethod]
        public async Task Upload_NoFile_ReturnsBadRequest()
        {
            // Arrange
            var fileUploadServiceMock = Substitute.For<FileUploadService>();
            var controller = new FileUploadController(fileUploadServiceMock);

            // Act
            var result = await controller.Upload(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("No file uploaded.");
        }
    }
}
