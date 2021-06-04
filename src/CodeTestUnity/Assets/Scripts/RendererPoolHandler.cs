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

		public RendererPoolHandler(IRenderer<TValue> prefab)
		{
			this.prefab = prefab;
			reserved = new List<IRenderer<TValue>>();
			pool = new List<IRenderer<TValue>>();
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
				var prefabGameObject = (MonoBehaviour)prefab;
				var rendererGameObject = UnityEngine.Object.Instantiate(prefabGameObject);

				renderer = rendererGameObject.GetComponent<IRenderer<TValue>>();
			}

			renderer.RenderTarget = value;
			reserved.Add(renderer);
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
