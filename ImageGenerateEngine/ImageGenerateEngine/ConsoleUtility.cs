using System;

namespace ImageGenerateEngine
{
	static class ConsoleUtility
	{
		public static string ConvertToTime(TimeSpan Milliseconds)
		{
			return string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", Milliseconds.Hours, Milliseconds.Minutes, Milliseconds.Seconds, Milliseconds.Milliseconds);
		}
	}
}
