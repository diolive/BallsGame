using System;

namespace BallsGame
{
#if WINDOWS || LINUX
	public static class Program
	{
		[STAThread]
		private static void Main()
		{
			using var game = new TheGame();
			game.Run();
		}
	}
#endif
}