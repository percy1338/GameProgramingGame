using System;
using System.Collections.Generic;
namespace GXPEngine
{
	public class Level : GameObject
	{
		const int MAP_Height = 10;
		const int MAP_WIDTH = 50;

		public int enemyCount = 0;

		private float _resetPlayerY;
		private float _resetPlayerX;

		private Player _player = new Player();
		private Enemy _enemy;
		private Enemy2 _enemy2;
		private Finish _finish;
		private Spike _spike;
		private PickUp _pickup;
		private HUD _hud;

		List<SolidObject> solidObjects = new List<SolidObject>();
		List<Finish> finishes = new List<Finish>();
		List<Enemy> enemies = new List<Enemy>();
		List<Enemy2> enemies2 = new List<Enemy2>();
		List<Spike> spikes = new List<Spike>();
		List<PickUp> pickups = new List<PickUp>();

		private Background _background = new Background();
		private Weather _weather = new Weather();

		private GameOverScreen _gameover = new GameOverScreen();
		private VictoryScreen _victory = new VictoryScreen();

		private Sound _backgroundMusic;
		private SoundChannel _backgroundChanel;

		private int _levelIndex = 1;

		private int[,] _level1 = new int[MAP_Height, MAP_WIDTH]
		{
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,6,0,0,0,0,0,0,6,7,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,1,0,0,0,0,0,1,1,1,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,6,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,6,0,0,1,1,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,6,0,0,0,0,1,1,0,0,0,1,1,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,3,0,7,0,0,1,1,0,0,8,0,2,2,9,9,9,2,2,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,1,1,1,1,1,1,2,2,1,1,1,1,2,2,1,1,1,2,2,1,1,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2}

		};
		private int[,] _level2 = new int[MAP_Height, MAP_WIDTH]
		{
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,4,0,0,4,0,0,0,4,0,0,4,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
			{2,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,8,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,2},
			{2,1,1,1,1,1,1,2,2,1,1,1,1,2,2,1,1,1,2,2,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2}

		};

		private int[,] _level3 = new int[MAP_Height, MAP_WIDTH]
		{
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,0,0,1,1,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,4,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,1,1,1,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,0,3,0,0,0,0,0,0,0,7,0,2,2,0,0,0,0,0,8,0,0,0,0,0,0,8,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{2,1,1,1,1,1,1,2,2,1,1,1,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
		};

		public Level() : base()
		{
			_backgroundMusic = new Sound("audio/Farm Frolics.ogg", true, true);
			_backgroundChanel = _backgroundMusic.Play(); 
			DrawLevel(_level1);
			DrawHud();
		}

		public void Update()
		{
			MoveCamera();
			if (_player.GetLives() == 0)
			{
				game.AddChild(_gameover);
				this.Destroy();
			}
			if (_player.y > 700)
			{
				_player.x = _resetPlayerX;
				_player.y = _resetPlayerY;
				_player.SubtractLives();
				ResetCamera();
			}

			if (Input.GetKeyDown(Key.N))
			{
				ResetCamera();
			}

			Console.WriteLine(_levelIndex);
		}

		void DrawLevel(int[,] level)
		{
			for (int column = 0; column < MAP_WIDTH; column++)
			{
				for (int row = 0; row < MAP_Height; row++)
				{
					int tile = level[row, column];
					if (tile > 0)
					{
						switch (tile)
						{
							case 1:
								SolidObject solid1 = new SolidObject(1);
								solid1.x = column * solid1.width;
								solid1.y = row * solid1.height;
								AddChild(solid1);
								solidObjects.Add(solid1);
								break;
							case 2:
								SolidObject solid2 = new SolidObject(2);
								solid2.x = column * solid2.width;
								solid2.y = row * solid2.height;
								AddChild(solid2);
								solidObjects.Add(solid2);
								break;
							case 3:
								_player.x = column * _player.width;
								_player.y = row * _player.height;
								_resetPlayerX = _player.x;
								_resetPlayerY = _player.y;
								AddChild(_player);
								break;
							case 4:
								_enemy = new Enemy(_player);
								_enemy.x = column * _enemy.width;
								_enemy.y = row * _enemy.height;
								AddChild(_enemy);
								enemies.Add(_enemy);
								break;
							case 5:
								InvisibleBlock block = new InvisibleBlock();
								block.x = column * block.width;
								block.y = row * block.height;
								AddChild(block);
								break;
							case 6:
								_pickup = new PickUp(_player);
								_pickup.x = column * _pickup.width;
								_pickup.y = row * _pickup.width;
								AddChild(_pickup);
								pickups.Add(_pickup);
								break;
							case 7:
								_finish = new Finish(this);
								_finish.x = column * _finish.width;
								_finish.y = row * _finish.height;
								AddChild(_finish);
								finishes.Add(_finish);
								break;
							case 8:
								_enemy2 = new Enemy2();
								_enemy2.x = column * _enemy2.width;
								_enemy2.y = row * _enemy2.height;
								AddChild(_enemy2);
								enemies2.Add(_enemy2);
								break;
							case 9:
								_spike = new Spike();
								_spike.x = column * _spike.width;
								_spike.y = row * _spike.height;
								AddChild(_spike);
								spikes.Add(_spike);
								break;
						}
					}
				}
			}
		}

		private void DestroyLevel(int[,] level)
		{
			for (int i = 0; i < solidObjects.Count; i++)
			{
				solidObjects[i].Destroy();
			}
			for (int i = 0; i < finishes.Count; i++)
			{
				finishes[i].Destroy();
			}
			for (int i = 0; i < enemies2.Count; i++)
			{
				enemies2[i].Destroy();
			}
			for (int i = 0; i < enemies.Count; i++)
			{
				enemies[i].Destroy();
			}
			for (int i = 0; i < spikes.Count; i++)
			{
				spikes[i].Destroy();
			}
			for (int i = 0; i < pickups.Count; i++)
			{
				pickups[i].Destroy();
			}

		}

		void DrawHud()
		{
			_hud = new HUD(_player);
			AddChild(_hud);
		}

		void MoveCamera()
		{
			if (_player.x >= 400 && _player.x <= (MAP_WIDTH * 64) - 400)
			{
				this.x = -_player.x + 400;
				_hud.x = _player.x - 400;
				_background.x = _player.x - 400;
				_weather.x = _player.x - 400;
			}
		}

		void ResetCamera()
		{
			this.x = 0;
			this.y = 0;
			_background.x = 0;
			_background.y = 0;
			_hud.x = 0;
			_hud.y = 0;
			_weather.x = 0;
			_weather.y = 0;
		}

		public void setLevel(int increment)
		{
			_levelIndex = _levelIndex + increment;
			switch (_levelIndex)
			{
				case 1:
					{
						DrawLevel(_level1);
						ResetCamera();
						break;
					}
				case 2:
					{
						DestroyLevel(_level1);
						DrawLevel(_level2);
						ResetCamera();
						break;
					}
				case 3:
					{
						DestroyLevel(_level2);
						DrawLevel(_level3);
						ResetCamera();
						break;
					}
				case 4:
					{
						game.AddChild(_victory);
						_backgroundChanel.Stop();
						this.Destroy();
						break;
					}
			}
		}
	}
}


