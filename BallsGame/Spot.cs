using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BallsGame
{
    public class Spot : GameObject
    {
        private Vector2 _origin;
        private Texture2D _texture;

        public Spot(Game game)
            : base(game)
        {
        }
        
        public Color Color { get; set; }
        public Point Position { get; set; }
        public Vector2 Scale { get; set; }

        protected override void LoadContent()
        {
            base.LoadContent();

            _texture = Game.Content.Load<Texture2D>(@"ball");
            Color = Color.White;
            Scale = new Vector2(0.05f);
            _origin = _texture.Bounds.Size.ToVector2() / 2f;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Draw(_texture, Position.ToVector2(), null, Color, 0f, _origin, Scale, SpriteEffects.None, 0f);
        }
    }
}