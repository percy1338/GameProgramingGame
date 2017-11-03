using System;
using System.Drawing;
namespace GXPEngine
{
	public class TextBoard : Canvas
	{
		public TextBoard(int width, int height) : base(width, height)
		{
			SetText("");
		}
		public void SetText(string text)
		{
			try
			{
				graphics.Clear(Color.White); // make text box 1 color
				this.alpha = 0.75f;
				graphics.DrawString(text, SystemFonts.DialogFont, Brushes.Black, 0, 0);
			}
			catch (Exception)
			{
				
			}
		}
	}
}
