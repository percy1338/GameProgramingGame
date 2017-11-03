using System;
namespace GXPEngine
{
	public class PickUp : Sprite
	{
		Player player;

		public PickUp( Player player) : base("sprites/coinGold.png")
		{
			this.player = player;
		}

		public void Collect()
		{
			player.AddPoints(1);
			Destroy();
		}
	}
}
