using System;
namespace GXPEngine
{
	public class Finish: Sprite
	{
		Level level; 

		public Finish(Level level): base("sprites/flagBlue.png")
		{
			this.level = level;
		}

		public void FinishLevel()
		{
			level.setLevel(1);
		}
	}
}
