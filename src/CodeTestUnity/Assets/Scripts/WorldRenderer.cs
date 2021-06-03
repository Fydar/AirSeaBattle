using CodeTest.Game.Simulation;
using System.Collections.Specialized;
using UnityEngine;

namespace CodeTestUnity
{
	public class WorldRenderer : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer background;
		[SerializeField] private WorldGunRenderer gunRendererPrefab;
		[SerializeField] private WorldEnemyRenderer enemyRendererPrefab;

		public World World { get; private set; }

		public void Render(World world)
		{
			World = world;

			World.Guns.CollectionChanged += GunCollectionChanged;
		}

		private void GunCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			foreach (object newItem in e.NewItems)
			{
				var worldGun = (WorldGun)newItem;
				var gunRenderer = Instantiate(gunRendererPrefab);

				gunRenderer.Render(worldGun);
			}
		}
	}
}
