using System;

using Microsoft.Xna.Framework;

namespace BallsGame
{
    public class Direction
    {
        private float _angle;

        public Direction(float angle)
        {
            _angle = angle;
        }

        public float Angle
        {
            get => _angle;
            set => _angle = value % MathHelper.TwoPi;
        }

        public void FlipX()
        {
            Flip(0);
        }

        public void FlipY()
        {
            Flip(MathHelper.PiOver2);
        }

        public void Flip(float wallAngle)
        {
            _angle = (float) Math.Atan2((float) Math.Sin(_angle - wallAngle), (float) -Math.Cos(_angle - wallAngle)) + wallAngle;
        }
    }
}