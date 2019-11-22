using System;

using Microsoft.Xna.Framework;

namespace BallsGame
{
    public class Wall
    {
        public Wall(Vector2 point1, Vector2 point2)
        {
            Point1 = point1;
            Point2 = point2;

            Angle = (float) Math.Atan2(point2.Y - point2.Y, point2.X - point1.X);
        }

        public Vector2 Point1 { get; }
        public Vector2 Point2 { get; }

        public float Angle { get; }

        public Vector2? Intersection(Vector2 beamStart, float beamDirection)
        {
            float a1 = Point2.Y - Point1.Y;
            float b1 = Point1.X - Point2.X;
            float c1 = Point1.X * Point2.Y - Point2.X * Point1.Y;

            var a2 = (float) Math.Tan(beamDirection);
            float c2 = beamStart.Y - a2 * beamStart.X;

            float divider = a1 + a2 * b1;

            if (Math.Abs(divider) < 1e-6)
            {
                return null;
            }

            float intX = (-c1 - b1*c2) / divider;
            float intY = (a1 * c2 - a2 * c1) / divider;

            Vector2 beamNext = beamStart + Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationZ(beamDirection));

            if ((Point1.X <= intX && intX <= Point2.X || Point2.X <= intX && intX <= Point1.X) &&
                (Point1.Y <= intY && intY <= Point2.Y || Point2.Y <= intY && intY <= Point1.Y) &&
                beamNext.X > beamStart.X == intX > beamStart.X &&
                beamNext.Y > beamStart.Y == intY > beamStart.Y)
            {
                return new Vector2(intX, intY);
            }

            return null;
        }
    }
}