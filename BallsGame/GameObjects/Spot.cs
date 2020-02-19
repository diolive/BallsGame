using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BallsGame.GameObjects
{
	public class Spot : BallBase
	{
		public Spot(ContentManager contentManager)
			: base(contentManager)
		{
			Color = Color.Red;
			Scale = 0.05f;
		}
	}
}