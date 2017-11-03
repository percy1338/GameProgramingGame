using System;
namespace GXPEngine
{
	public class Enemy2 : AnimSprite
	{
		//private float _range = 300;
		//private float _cooldown = 60;
		private float _speedX = 5;
		private float _speedY;
		private float _gravity = 4.2f;
		private bool _turn = false;

		private int _step;
		private int _animDrawsBetweenFrames = 5;
		int _maxFramesInAnim = 1;

		//private Player _player;
		//Bullet bullet;

		public Enemy2() : base("sprites/enemy2Sheet.png",3,1,-1)
		{
			Mirror(true, false);
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
				Movement();
			}

			//ShootPlayer();
			//_cooldown--;
		}



		private void Movement()
		{
			this.x += _speedX;
			CheckCollision(_speedX, 0);
			if (_turn)
			{
				_speedX = -5;
			}
			if (!_turn)
			{
				_speedX = 5;
			}
			//this.y += _gravity;
			this.y += _speedY;
			CheckCollision(0, _speedY + _gravity);
		}

		void CheckCollision(float mx, float my)
		{
			GameObject[] others = GetCollisions();//get all objects player is coliding with and puts it in a list
			foreach (GameObject other in others)
			{
				if (other is SolidObject)//check if object is solid
				{
					this.x -= mx;//return to previous non coliding position
					this.y -= my;
					_turn = !_turn;
					if (mx > 0)
					{
						if (_turn)
						{
							_speedX = 5;
							Mirror(false, false);
						}
					}
					else
					{
						Mirror(true, false);
					}
					return;
				}
				if (other is InvisibleBlock)
				{
					this.x -= mx;//return to previous non coliding position
					this.y -= my;
					_turn = !_turn;
					if (mx > 0)
					{
						if (_turn)
						{
							_speedX = 5;
						}
					}
					return;
				}
			}
		}

		/*void ShootPlayer()
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
			}*/
	}
}
