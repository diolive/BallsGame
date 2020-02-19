using System;

namespace BallsGame
{
	public static class RandomHelper
	{
		public static Random Instance { get; } = new Random();

		public static int NextInt()
		{
			return Instance.Next();
		}

		public static int NextInt(int maxValue)
		{
			return Instance.Next(maxValue);
		}

		public static int NextInt(int minValue, int maxValue)
		{
			return Instance.Next(minValue, maxValue);
		}

		public static float NextFloat()
		{
			return (float) Instance.NextDouble();
		}

		public static double NextDouble()
		{
			return Instance.NextDouble();
		}
	}
}