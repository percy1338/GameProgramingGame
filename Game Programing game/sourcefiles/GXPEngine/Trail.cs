using System;
namespace GXPEngine
{
	public class Trail : AnimSprite
	{
		private int _duration;


		public Trail(Sprite sprite) : base(sprite.name, 2, 2, -1)
		{
			_duration = 20;

			x = sprite.x;
			y = sprite.y;
			rotation = sprite.rotation;
		}

		public void Update()
		{
			alpha = _duration / 100.0f;
			_duration--;
			if (_duration < 0)
			{
				Destroy();
			}
		}
	}
}
