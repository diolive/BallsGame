using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BallsGame
{
	public class Cursor : Spot
	{
		public Cursor(Game game)
			: base(game)
		{
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			Position = Mouse.GetState().Position;
		}
	}
}