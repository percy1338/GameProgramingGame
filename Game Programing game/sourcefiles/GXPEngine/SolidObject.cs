using System;
namespace GXPEngine
{
	public class SolidObject : AnimSprite
	{
		public SolidObject(int index) : base("sprites/level1tiles.png", 3, 2, -1)
		{
			SetFrame(index);
		}
	}
}
