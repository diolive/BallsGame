using System;

using Microsoft.Xna.Framework.Input;

namespace BallsGame.Control
{
	public class MouseControl : IControl
	{
		public enum MouseButtons
		{
			None,
			Left,
			Right,
			Middle,
			XButton1,
			XButton2
		}

		public MouseControl(MouseButtons button, Func<(int x, int y), bool> position = null)
		{
			Button = button;
			Position = position;
		}

		public MouseButtons Button { get; }
		public Func<(int, int), bool> Position { get; }

		public bool IsPressed(IControlsState controlsState)
		{
			return (Position?.Invoke((controlsState.Mouse.X, controlsState.Mouse.Y)) ?? true) &&
			       Button switch
			       {
				       MouseButtons.None => true,
				       MouseButtons.Left => controlsState.Mouse.LeftButton == ButtonState.Pressed,
				       MouseButtons.Right => controlsState.Mouse.RightButton == ButtonState.Pressed,
				       MouseButtons.Middle => controlsState.Mouse.MiddleButton == ButtonState.Pressed,
				       MouseButtons.XButton1 => controlsState.Mouse.XButton1 == ButtonState.Pressed,
				       MouseButtons.XButton2 => controlsState.Mouse.XButton2 == ButtonState.Pressed,
				       _ => throw new ArgumentOutOfRangeException("button")
			       };
		}
	}
}