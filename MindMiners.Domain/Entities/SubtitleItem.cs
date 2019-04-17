using System;
using System.Collections.Generic;
using System.Text;

namespace MindMiners.Domain.Entities
{
    public class SubtitleItem
    {
        public SubtitleItem(int offsetMilliseconds)
        {
            OffsetMilliseconds = offsetMilliseconds;
        }
        public int Sequence { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public List<string> Lines { get; set; } = new List<string>();
        public int OffsetMilliseconds { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var startTs = new TimeSpan(0, 0, 0, 0, StartTime + OffsetMilliseconds);
            var endTs = new TimeSpan(0, 0, 0, 0, EndTime + OffsetMilliseconds);

            sb.AppendLine(Sequence.ToString());
            sb.AppendLine($"{startTs:hh\\:mm\\:ss\\,fff} --> {endTs:hh\\:mm\\:ss\\,fff}");
            sb.AppendLine(string.Join(Environment.NewLine, Lines));

            return sb.ToString();
        }
    }
}
