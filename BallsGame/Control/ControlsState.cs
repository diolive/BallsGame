using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace BallsGame.Control
{
	public class ControlsState : IControlsState
	{
		private KeyboardState? _keyboardState;
		private MouseState? _mouseState;
		private TouchCollection? _touchPanelState;
		private IDictionary<int, GamePadState> _gamePadStates;

		public ControlsState()
		{
			_gamePadStates = new Dictionary<int, GamePadState>();
		}

		public KeyboardState Keyboard
		{
			get
			{
				_keyboardState ??= Microsoft.Xna.Framework.Input.Keyboard.GetState();
				return _keyboardState.Value;
			}
		}

		public MouseState Mouse
		{
			get
			{
				_mouseState ??= Microsoft.Xna.Framework.Input.Mouse.GetState();
				return _mouseState.Value;
			}
		}

		public TouchCollection TouchPanel
		{
			get
			{
				_touchPanelState ??= Microsoft.Xna.Framework.Input.Touch.TouchPanel.GetState();
				return _touchPanelState.Value;
			}
		}

		public GamePadState GamePad(int index)
		{
			if (!_gamePadStates.ContainsKey(index))
			{
				_gamePadStates[index] = Microsoft.Xna.Framework.Input.GamePad.GetState(index);
			}

			return _gamePadStates[index];
		}
	}
}
