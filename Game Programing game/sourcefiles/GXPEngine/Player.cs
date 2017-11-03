using System;


namespace GXPEngine
{
	public class Player : AnimSprite
	{
		private int _health = 3;
		private int _points;
		private float _speedX;
		private float _speedY;
		private float _bulletVelocityX = 1;
		private float _cooldown = 30;
		private float _gravity = 15;
		private float _invulnerability = 60;
		private bool _isHit = false;
		private bool _isGrounded = true;
		private bool _isMoveing = false;

		private int _step;
		private int _animDrawsBetweenFrames = 5;
		int _maxFramesInAnim = 2;

		Bullet bullet;

		public Player() : base("sprites/player.png", 2, 2, -1)
		{
			this.x = game.width / 2;
			this.y = game.height / 2;
			_speedX = 0.0f;
			_speedY = 0.0f;
		}

		public void Update()
		{

			if (Input.GetKey(Key.LEFT))
			{
				_speedX = _speedX - 1.0f;
				_bulletVelocityX = -1;
				this.Mirror(true, false);
			}
			if (Input.GetKey(Key.RIGHT))
			{
				_speedX = _speedX + 1.0f;
				_bulletVelocityX = 1;
				this.Mirror(false, false);
			}
			if (Input.GetKeyDown(Key.UP) && _isGrounded)
			{
				_speedY = -60.0f;
				_isGrounded = false;
			}

			if (Input.GetKey(Key.LEFT) || Input.GetKey(Key.RIGHT))
			{
				_isMoveing = true;
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
			}
			else
			{
				_isMoveing = false;
				SetFrame(-1);
			}

			if (Input.GetKeyDown(Key.SPACE))
			{
				Shoot();
			}

			if (_isHit)
			{
				_invulnerability--;
				if (_invulnerability <= 0)
				{
					_invulnerability = 60;
					_isHit = false;
				}
			}


			this.x += _speedX;
			CheckCollision(_speedX, 0);


			this.y += _speedY;
			y += _gravity;
			CheckCollision(0, _speedY + _gravity);

			_speedX = _speedX * 0.9f;
			_speedY = _speedY * 0.9f;
			_cooldown--;
			addTrail();
		}

		void CheckCollision(float mx, float my)
		{
			GameObject[] others = GetCollisions();//get all objects player is coliding with and puts it in a list
			foreach (GameObject other in others)
			{
				if (other is SolidObject)
				{
					this.x -= mx;
					this.y -= my;
					if (mx == 0 && my >= 0)
					{
						_isGrounded = true;
					}
					return;
				}
				if (other is Enemy || other is Enemy2)
				{
					// lose health
					if (!_isHit)
					{
						SubtractLives();
						other.Destroy();
						_isHit = true;
					}
				}
				if (other is Spike)
				{
					// lose health
					if (!_isHit)
					{
						SubtractLives();
						_isHit = true;
					}
				}

				if (other is PickUp)
				{
					// increse score
					((PickUp)other).Collect();
					//pickup.Collect();
					Console.WriteLine(_points);
				}
				if (other is Finish)
				{
					((Finish)other).FinishLevel();
				}
			}
		}
		void Shoot()
		{
			Console.WriteLine(this.x + "   " + this.y);
			bullet = new Bullet(this.x + this.width * 0.5f, this.y + this.height * 0.25f, _bulletVelocityX, 0, 1);
			parent.AddChild(bullet);
		}


		public int GetPoints()
		{
			return _points;
		}
		public void SetPoints(int points)
		{
			_points = points;
		}
		public void AddPoints(int points)
		{
			_points += points;
		}
		public int GetLives()
		{
			return _health;
		}
		public void SubtractLives()
		{
			_health--;
		}
		public float getXpos()
		{
			return this.x;
		}

		public float getYpos()
		{
			return this.y;
		}

		public bool IsMoving()
		{
			return _isMoveing;
		}

		private void addTrail()
		{
			try
			{
				Trail trail = new Trail(this);
				parent.AddChildAt(trail, 1);
			}
			catch
			{

			}
		}

	}
}
