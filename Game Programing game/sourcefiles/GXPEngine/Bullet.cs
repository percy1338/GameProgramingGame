using System;
namespace GXPEngine
{
	public class Bullet : Sprite
	{

		private float _speed = 15;
		private float _velocityX, _velocityY;
		private int _shooterId;

		public Bullet(float x, float y, float velocityX, float velocityY, int shooterId) : base("sprites/bullet.png")
		{
			this.x = x;
			this.y = y;
			_shooterId = shooterId;
			_velocityX = velocityX * _speed;
			_velocityY = velocityY * _speed;
		}

		void Update()
		{
			x += _velocityX;
			y += _velocityY;

			GameObject[] others = GetCollisions();//get all objects player is coliding with and puts it in a list
			foreach (GameObject other in others)
			{
				if (other is Enemy && _shooterId == 1)// check if colliderd object is an enemy and if the player shot the bullet
				{
					((Enemy)other).Destroy();
					this.Destroy();
				}
				if (other is Enemy2 && _shooterId == 1)// check if colliderd object is an enemy2 and if the player shot the bullet
				{
					((Enemy2)other).Destroy();
					this.Destroy();
				}
				if (other is SolidObject)
				{
					this.Destroy();
				}
				if (other is Player && _shooterId == 2)// check if colliderd object is an player and if the enemy shot the bullet
				{
					((Player)other).SubtractLives();
					this.Destroy();
				}
			}
		}

		public void stuff()
		{

		}
	}
}
