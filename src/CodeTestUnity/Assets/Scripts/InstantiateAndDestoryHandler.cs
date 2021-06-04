using RPGCore.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeTestUnity
{
	public class InstantiateAndDestoryHandler<TValue> : IEventDictionaryHandler<Guid, TValue>
		where TValue : class
	{
		private readonly List<IRenderer<TValue>> renderers;
		private readonly IRenderer<TValue> prefab;

		public InstantiateAndDestoryHandler(IRenderer<TValue> prefab)
		{
			this.prefab = prefab;
			renderers = new List<IRenderer<TValue>>();
		}

		public void OnAdd(Guid key, TValue value)
		{
			var prefabGameObject = (MonoBehaviour)prefab;
			var rendererGameObject = UnityEngine.Object.Instantiate(prefabGameObject);

			var renderer = rendererGameObject.GetComponent<IRenderer<TValue>>();
			renderer.RenderTarget = value;

			renderers.Add(renderer);
		}

		public void OnRemove(Guid key, TValue value)
		{
			foreach (var renderer in renderers)
			{
				if (renderer.RenderTarget == value)
				{
					renderer.RenderTarget = null;
				}
			}
		}
	}
}
