using System;
namespace GXPEngine
{
	public class VictoryScreen : GameObject
	{
		private Sprite _background = new Sprite("sprites/win.png");
		private Sprite _continue = new Sprite("sprites/startButton.png");
		private Sprite _stopButten = new Sprite("sprites/StopButton.png");

		public VictoryScreen()
		{
			_continue.x = game.width * 0.25f - (_continue.width * 0.25f);
			_continue.y = game.height * 0.5f - (_continue.height * 0.5f);

			_stopButten.x = game.width * 0.75f - (_continue.width * 0.75f);
			_stopButten.y = game.height * 0.5f - (_continue.height * 0.5f);

			AddChild(_background);
			AddChild(_continue);
			AddChild(_stopButten);
		}

		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (_continue.HitTestPoint(Input.mouseX, Input.mouseY))
				{
					ReturnToStart();
				}
				if (_stopButten.HitTestPoint(Input.mouseX, Input.mouseY))
				{
					game.Destroy();
				}
			}
		}

		private void ReturnToStart()
		{
			Level level = new Level();
			game.AddChild(level);

			Destroy();
		}
	}
}