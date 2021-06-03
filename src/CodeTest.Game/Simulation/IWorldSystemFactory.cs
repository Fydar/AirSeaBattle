namespace CodeTest.Game.Simulation
{
	public interface IWorldSystemFactory
	{
		IWorldSystem Build(World world);
	}
}
