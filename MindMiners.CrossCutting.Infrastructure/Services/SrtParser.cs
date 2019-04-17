using MindMiners.Domain.Entities;
using MindMiners.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MindMiners.CrossCutting.Infrastructure.Services
{
    public class SrtParser : ISrtParser
    {
        private readonly string[] _delimiters = { "-->", "- >", "->" };
        public IList<SubtitleItem> ParseToSubtitleItemList(Stream srtStream, int offsetMilliSeconds = 0)
        {
            if (!srtStream.CanRead || !srtStream.CanSeek)
            {
                var message = string.Format("Stream must be seekable and readable in a subtitles parser. " +
                                   "Operation interrupted; isSeekable: {0} - isReadable: {1}",
                                   srtStream.CanSeek, srtStream.CanSeek);
                throw new ArgumentException(message);
            }

            srtStream.Position = 0;

            var reader = new StreamReader(srtStream, true);

            var items = new List<SubtitleItem>();
            var srtSubParts = GetSrtSubTitleParts(reader).ToList();
            if (srtSubParts.Any())
            {
                foreach (var srtSubPart in srtSubParts)
                {
                    var lines =
                        srtSubPart.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                            .Select(s => s.Trim())
                            .Where(l => !string.IsNullOrEmpty(l))
                            .ToList();

                    var item = new SubtitleItem(offsetMilliSeconds);
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (i == 0 && int.TryParse(lines[i], out int sequence))
                        {
                            item.Sequence = sequence;
                            i++;
                        }

                        if (item.StartTime == 0 && item.EndTime == 0)
                        {
                            if (TryParseTimecodeLine(lines[i], out int startTc, out int endTc))
                            {
                                item.StartTime = startTc;
                                item.EndTime = endTc;
                            }
                        }
                        else
                            item.Lines.Add(lines[i]);
                    }

                    if ((item.StartTime != 0 || item.EndTime != 0) && item.Lines.Any())
                        items.Add(item);
                }

                if (items.Any())
                    return items;
                else
                    throw new ArgumentException("Stream is not in a valid Srt format");
            }
            else
                throw new FormatException("Parsing as srt returned no srt part.");
        }
        private IEnumerable<string> GetSrtSubTitleParts(TextReader reader)
        {
            string line;
            var sb = new StringBuilder();

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                {
                    var res = sb.ToString().TrimEnd();
                    if (!string.IsNullOrEmpty(res))
                    {
                        yield return res;
                    }
                    sb = new StringBuilder();
                }
                else
                    sb.AppendLine(line);
            }

            if (sb.Length > 0)
                yield return sb.ToString();
        }
        private bool TryParseTimecodeLine(string line, out int startTc, out int endTc)
        {
            var parts = line.Split(_delimiters, StringSplitOptions.None);
            if (parts.Length != 2)
            {
                startTc = -1;
                endTc = -1;
                return false;
            }
            else
            {
                startTc = ParseSrtTimecode(parts[0]);
                endTc = ParseSrtTimecode(parts[1]);
                return true;
            }
        }
        private static int ParseSrtTimecode(string value)
        {
            var match = Regex.Match(value, "[0-9]+:[0-9]+:[0-9]+([,\\.][0-9]+)?");
            if (match.Success)
            {
                value = match.Value;
                if (TimeSpan.TryParse(value.Replace(',', '.'), out TimeSpan result))
                    return (int)result.TotalMilliseconds;
            }
            return -1;
        }
    }
}
