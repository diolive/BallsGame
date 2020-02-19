using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BallsGame.GameObjects
{
	public class Cursor : BallBase
	{
		public Cursor(ContentManager contentManager)
			: base(contentManager)
		{
			Color = Color.White;
			Scale = 0.05f;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			Position = Mouse.GetState().Position.ToVector2();
		}
	}
}