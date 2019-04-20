using MindMiners.Application;
using MindMiners.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System.IO;

namespace Tests
{
    public class SynchronizationApplicationTest
    {
        private static SynchronizationApplication _application;
        private static Mock<ISrtParser> _srtParser;
        private static Mock<IFileHistoryRepository> _fileHistoryRepository;


        [SetUp]
        public void Setup()
        {
            _srtParser = new Mock<ISrtParser>();
            _fileHistoryRepository = new Mock<IFileHistoryRepository>();

        }

        [Test]
        public void When_Call_SubtitleSync_Should_Return_ByteArray()
        {
            _application = new SynchronizationApplication(_srtParser.Object, _fileHistoryRepository.Object);
            var result = _application.SubtitleSync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<double>());
            Assert.IsInstanceOf<byte[]>(result);
        }
    }

}