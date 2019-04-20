using MindMiners.Application;
using MindMiners.Domain.Entities;
using MindMiners.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public class HistoryApplicationTest
    {
        private static HistoryApplication _application;
        private static Mock<IFileHistoryRepository> _fileHistoryRepository;


        [SetUp]
        public void Setup()
        {
            _fileHistoryRepository = new Mock<IFileHistoryRepository>();
        }

        [Test]
        public void When_Call_GetFile_Should_Return_Tuple_ByteArray_And_String()
        {
            _application = new HistoryApplication(_fileHistoryRepository.Object);
            var result = _application.GetFile(It.IsAny<int>());
            Assert.IsInstanceOf<(byte[], string)>(result);
        }

        [Test]
        public void When_Call_GetFileHistory_Should_Return_FileHistory()
        {
            _application = new HistoryApplication(_fileHistoryRepository.Object);
            var result = _application.GetFileHistory();
            Assert.IsInstanceOf<IEnumerable<FileHistory>>(result);
        }

        [Test]
        public void When_Call_RemoveFile_Should_Pass_RemoveFile_Once()
        {
            _application = new HistoryApplication(_fileHistoryRepository.Object);
            _application.RemoveFile(It.IsAny<int>());
            _fileHistoryRepository.Verify(c => c.RemoveFile(It.IsAny<int>()), Times.Once);
        }
    }
}