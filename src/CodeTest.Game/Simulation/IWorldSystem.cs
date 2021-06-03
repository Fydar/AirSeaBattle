namespace CodeTest.Game.Simulation
{
	public interface IWorldSystem
	{
		void OnPlayerJoined(WorldPlayer worldPlayer);
		void OnPlayerRemoved(WorldPlayer worldPlayer);
		void OnUpdate(UpdateParameters parameters);
	}
}
