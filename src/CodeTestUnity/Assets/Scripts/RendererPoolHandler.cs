using RPGCore.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeTestUnity
{
	public class RendererPoolHandler<TValue> : IEventDictionaryHandler<Guid, TValue>
		where TValue : class
	{
		private readonly List<IRenderer<TValue>> reserved;
		private readonly List<IRenderer<TValue>> pool;
		private readonly IRenderer<TValue> prefab;

		public RendererPoolHandler(IRenderer<TValue> prefab, int preallocate = 0)
		{
			this.prefab = prefab;
			reserved = new List<IRenderer<TValue>>();
			pool = new List<IRenderer<TValue>>();

			// Instantiate objects at the start to prevent instantation during gameplay.
			for (int i = 0; i < preallocate; i++)
			{
				AllocateNewRenderer();
			}
		}

		public void OnAdd(Guid key, TValue value)
		{
			IRenderer<TValue> renderer = null;
			if (pool.Count > 0)
			{
				renderer = pool[pool.Count - 1];
				pool.RemoveAt(pool.Count - 1);
			}
			if (renderer == null)
			{
				renderer = AllocateNewRenderer();
			}

			renderer.RenderTarget = value;
			reserved.Add(renderer);
		}

		private IRenderer<TValue> AllocateNewRenderer()
		{
			var prefabGameObject = (MonoBehaviour)prefab;
			var rendererGameObject = UnityEngine.Object.Instantiate(prefabGameObject);

			return rendererGameObject.GetComponent<IRenderer<TValue>>();
		}

		public void OnRemove(Guid key, TValue value)
		{
			foreach (var renderer in reserved)
			{
				if (renderer.RenderTarget == value)
				{
					renderer.RenderTarget = null;
					reserved.Remove(renderer);
					pool.Add(renderer);
					break;
				}
			}
		}
	}
}
