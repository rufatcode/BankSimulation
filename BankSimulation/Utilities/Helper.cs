using System;
namespace Utilities
{
	public static class Helper
	{
		public static string User { get; } = "Admin123";
		public static string Password { get; } = "Admin123";
		public static void SetMessageAndColor(string message,ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}

