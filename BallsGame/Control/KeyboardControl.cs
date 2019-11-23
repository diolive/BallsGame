using Microsoft.Xna.Framework.Input;

namespace BallsGame.Control
{
	public class KeyboardControl : IControl
	{
		public KeyboardControl(Keys keys)
		{
			Keys = keys;
		}

		public Keys Keys { get; }

		public bool IsPressed(IControlsState controlsState)
		{
			return controlsState.Keyboard.IsKeyDown(Keys);
		}
	}
}