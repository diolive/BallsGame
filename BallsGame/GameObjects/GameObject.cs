using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BallsGame.GameObjects
{
	public abstract class GameObject
	{
		public bool Visible { get; set; } = true;

		public virtual void Update(GameTime gameTime)
		{
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (Visible)
			{
				DrawVisible(spriteBatch);
			}
		}

		protected virtual void DrawVisible(SpriteBatch spriteBatch)
		{
		}
	}
}