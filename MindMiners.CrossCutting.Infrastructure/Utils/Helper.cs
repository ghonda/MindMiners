using System;
using System.Globalization;

namespace MindMiners.CrossCutting.Infrastructure.Utils
{
    public class Helper
    {
        public static int ConvertSecondsToMilliseconds(double seconds)
        {
            return Convert.ToInt32(TimeSpan.FromSeconds(seconds).TotalMilliseconds);
        }

        public static double ConvertStringToDouble(string value)
        {
            double.TryParse(value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double result);
            return result;
        }
    }
}
