using System;

namespace GXPEngine
{
	public class HUD : GameObject
	{
		private Player _player;
		private TextBoard _textBoardScore;
		private TextBoard _textBoardLives;
	
		public HUD(Player player)
		{
			_player = player;

			_textBoardScore = new TextBoard(128, 32);
			AddChild(_textBoardScore);

			_textBoardLives = new TextBoard(128, 32);
			_textBoardLives.x += 128 + 4;
			AddChild(_textBoardLives);
		}

		void Update()
		{
			_textBoardScore.SetText("SCORE: " + _player.GetPoints());
			_textBoardLives.SetText("LIVES: " + _player.GetLives());
		}
	}
}
