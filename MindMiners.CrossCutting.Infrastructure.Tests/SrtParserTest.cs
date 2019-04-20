using MindMiners.CrossCutting.Infrastructure.Services;
using MindMiners.Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public class SrtParserTest
    {
        private static SrtParser _application;
        [SetUp]
        public void Setup()
        {
            _application = new SrtParser();
        }

        [Test]
        public void When_Call_ParseToSubtitleItemList_Should_Return_SubtitleItemList()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Teste.srt");
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var result = _application.ParseToSubtitleItemList(stream);
                Assert.IsInstanceOf<List<SubtitleItem>>(result);
            }
        }
    }
}
