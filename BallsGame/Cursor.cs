using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BallsGame
{
    public class Cursor : GameObject
    {
        private Color _color;
        private Vector2 _origin;
        private Vector2 _scale;
        private Texture2D _texture;

        public Cursor(Game game)
            : base(game)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _texture = Game.Content.Load<Texture2D>(@"ball");
            _color = Color.White;
            _scale = Vector2.One / 20f;
            _origin = _texture.Bounds.Size.ToVector2() / 2f;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Draw(_texture, Mouse.GetState().Position.ToVector2(), null, _color, 0f, _origin, _scale, SpriteEffects.None, 0f);
        }
    }
}