using System;
namespace GXPEngine
{
	public class Weather : GameObject
	{
		public Weather()
		{
			for (int i = 0; i < 10; i++)
			{
				AddChild(new Cloud());
			}
		}
	}
}
