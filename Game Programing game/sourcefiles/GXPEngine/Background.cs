using System;
namespace GXPEngine
{
	public class Background : Sprite
	{
		private float _speed = 512.0f;

		public Background(): base("sprites/background.png")
		{
			this.scale = 2;
		}

		public void Update()
		{
			//this.x -= _speed * (Time.deltaTime / 1000.0f);
			if (this.x < -width * 0.5f)
			{
				x += width * 0.5f;
			}
		}
	}
}
