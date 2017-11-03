using System;
namespace GXPEngine
{
	public class Cloud : Sprite
	{
		private float _speed;

		public Cloud() : base("sprites/cloud4.png")
		{
			_speed = Utils.Random(0.5f, 1.0f);
			alpha = Utils.Random(0.1f, 0.8f);
			scale = Utils.Random(0.04f, 0.64f);
			x = Utils.Random(-width, game.width + width);
			y = Utils.Random(-height, game.height*0.5f + height);
		}

		private void reset()
		{
			x = game.width + width;
		}

		public void Update()
		{
			Console.WriteLine(x);

			x -= _speed;
			if (x < 0)
			{
				reset();
			}
		}
	}
}
