namespace CodeTest.Game
{
	public class WorldEngine
	{
		internal readonly IWorldSystemFactory[] worldSystems;

		internal WorldEngine(IWorldSystemFactory[] worldSystems)
		{
			this.worldSystems = worldSystems;
		}

		public WorldBuilder ConstructWorld()
		{
			return new WorldBuilder(this);
		}
	}
}
