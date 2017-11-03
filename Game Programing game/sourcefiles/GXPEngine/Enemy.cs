using System;
namespace GXPEngine
{
	public class Enemy : AnimSprite
	{
		private float _range = 300;
		private float _cooldown = 60;

		private int _step;
		private int _animDrawsBetweenFrames = 5;
		int _maxFramesInAnim = 2;

		private Player _player;
		Bullet bullet;

		public Enemy(Player player) : base("sprites/bee.png", 2, 1, -1)
		{
			_player = player;
		}

		public void Update()
		{
			_step = _step + 1;
			if (_step > _animDrawsBetweenFrames)
			{
				NextFrame();
				_step = 0;

				if (currentFrame > _maxFramesInAnim)
				{
					SetFrame(-1);
				}
			}

			ShootPlayer();
			_cooldown--;
		}

		void ShootPlayer()
		{
			float deltaX = _player.x - this.x;
			float deltaY = _player.y - this.y;
			float length = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);

			float velocityX = deltaX / length;
			float velocityY = deltaY / length;

			if (length <= _range)
			{
				if (_cooldown <= 0)
				{
					bullet = new Bullet(this.x, this.y, velocityX, velocityY, 2);
					parent.AddChild(bullet);
					_cooldown = 60;
				}
			}
		}
	}
}
