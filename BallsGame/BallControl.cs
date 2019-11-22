using Microsoft.Xna.Framework.Input;

namespace BallsGame
{
    public class BallControl
    {
        public Keys TurnLeft { get; set; }
        public Keys TurnRight { get; set; }
        public Keys Enlarge { get; set; }
        public Keys Reduce { get; set; }

        public float TurnVelocity { get; set; } = 0.003f;
        public float EnlargeReduceRate { get; set; } = 0.002f;
    }
}