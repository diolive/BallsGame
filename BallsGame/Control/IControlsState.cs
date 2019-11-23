using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace BallsGame.Control
{
	public interface IControlsState
	{
		KeyboardState Keyboard { get; }
		MouseState Mouse { get; }
		TouchCollection TouchPanel { get; }
		GamePadState GamePad(int index);
	}
}