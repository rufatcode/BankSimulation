using System;
namespace Utilities
{
	public static class Helper
	{
		public static void SetMessageAndColor(string message,ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}

