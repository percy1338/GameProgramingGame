using System;
namespace GXPEngine
{
	public class StartScreen : GameObject
	{
		private Sprite _startButton = new Sprite("sprites/startButton.png");
		private Sprite _background = new Sprite("sprites/menuscreen.png");

		public StartScreen()
		{
			_startButton.x = game.width * 0.5f - (_startButton.width * 0.5f);
			_startButton.y = game.height * 0.5f - (_startButton.height * 0.5f);

			AddChild(_background);
			AddChild(_startButton);
		}

		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (_startButton.HitTestPoint(Input.mouseX, Input.mouseY))
				{
					StartGame();
				}
			}
		}

		private void StartGame()
		{
			Level level = new Level();
			game.AddChild(level);

			Destroy();
		}
	}
}
