using RPGCore.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AirSeaBattleUnity.EntityRendering
{
	public class RendererPoolHandler<TValue> : IEventDictionaryHandler<Guid, TValue>
		where TValue : class
	{
		private readonly List<EntityRenderer<TValue>> reserved;
		private readonly List<EntityRenderer<TValue>> pool;
		private readonly EntityRenderer<TValue> prefab;

		public RendererPoolHandler(EntityRenderer<TValue> prefab, int preallocate = 0)
		{
			this.prefab = prefab;
			reserved = new List<EntityRenderer<TValue>>();
			pool = new List<EntityRenderer<TValue>>();

			// Instantiate objects at the start to prevent instantation during gameplay.
			for (int i = 0; i < preallocate; i++)
			{
				AllocateNewRenderer();
			}
		}

		public void OnAdd(Guid key, TValue value)
		{
			EntityRenderer<TValue> renderer = null;
			if (pool.Count > 0)
			{
				renderer = pool[pool.Count - 1];
				pool.RemoveAt(pool.Count - 1);
			}
			if (renderer == null)
			{
				renderer = AllocateNewRenderer();
			}
			renderer.gameObject.SetActive(true);

			renderer.RenderTarget = value;
			reserved.Add(renderer);
		}

		private EntityRenderer<TValue> AllocateNewRenderer()
		{
			var prefabGameObject = (MonoBehaviour)prefab;
			var rendererGameObject = UnityEngine.Object.Instantiate(prefabGameObject);
			rendererGameObject.gameObject.SetActive(false);
			return rendererGameObject.GetComponent<EntityRenderer<TValue>>();
		}

		public void OnRemove(Guid key, TValue value)
		{
			foreach (var renderer in reserved)
			{
				if (renderer.RenderTarget == value)
				{
					renderer.RenderTarget = null;
					renderer.gameObject.SetActive(true);
					reserved.Remove(renderer);
					pool.Add(renderer);
					break;
				}
			}
		}
	}
}
