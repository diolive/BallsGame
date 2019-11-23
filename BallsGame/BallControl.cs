using BallsGame.Control;

namespace BallsGame
{
	public class BallControl
	{
		public IControl TurnLeft { get; set; }
		public IControl TurnRight { get; set; }
		public IControl Enlarge { get; set; }
		public IControl Reduce { get; set; }
	}
}