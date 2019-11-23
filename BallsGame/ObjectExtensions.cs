using System;

namespace BallsGame
{
	public static class ObjectExtensions
	{
		public static T Apply<T>(this T @object, Action<T> action)
		{
			action?.Invoke(@object);
			return @object;
		}
	}
}