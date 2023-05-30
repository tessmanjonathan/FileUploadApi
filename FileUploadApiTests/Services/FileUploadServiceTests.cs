using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FileUploadApi.Services.Tests
{
    [TestClass]
    public class FileUploadServiceTests
    {
        [TestMethod]
        public async Task UploadFile_ValidFile_ReturnsSuccessMessage()
        {
            var fileMock = Substitute.For<IFormFile>();
            fileMock.FileName.Returns("testfile.txt");
            fileMock.Length.Returns(100);

            var service = new FileUploadService();

            var result = await service.UploadFile(fileMock);

            result.Should().Be("File uploaded successfully.");
            fileMock.Received(1).CopyToAsync(Arg.Any<FileStream>());
        }

        [TestMethod]
        public async Task UploadFile_NoFile_ReturnsNoFileUploadedMessage()
        {
            // Arrange
            var service = new FileUploadService();

            // Act
            var result = await service.UploadFile(null);

            // Assert
            result.Should().Be("No file uploaded.");
        }

        [TestMethod]
        public async Task UploadFile_UploadsFilesToConfiguredDirectory()
        {
            var expectedUploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "testingfile.txt");

            var fileMock = Substitute.For<IFormFile>();
            fileMock.FileName.Returns("testingfile.txt");
            fileMock.Length.Returns(100);

            var service = new FileUploadService();

            var result = await service.UploadFile(fileMock);

            fileMock.Received(1).CopyToAsync(Arg.Is<FileStream>(stream => stream.Name == expectedUploadDirectory));
        }

    }
}
