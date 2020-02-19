using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BallsGame.GameObjects
{
	public abstract class BallBase : GameObject
	{
		protected BallBase(ContentManager contentManager)
		{
			Texture = contentManager.Load<Texture2D>(@"ball");
			Origin = Texture.Bounds.Size.ToVector2() / 2f;
			Scale = 1f;
		}

		protected Vector2 Origin { get; }

		protected Texture2D Texture { get; }

		public Color Color { get; set; }
		public Vector2 Position { get; set; }
		public float Scale { get; set; }

		protected override void DrawVisible(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, null, Color, 0f, Origin, Scale, SpriteEffects.None, 0f);
		}
	}
}