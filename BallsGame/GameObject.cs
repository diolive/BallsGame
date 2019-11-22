using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BallsGame
{
    public class GameObject : DrawableGameComponent
    {
        public GameObject(Game game)
            : base(game)
        {
        }

        protected SpriteBatch SpriteBatch => ((TheGame) Game).SpriteBatch;
    }
}