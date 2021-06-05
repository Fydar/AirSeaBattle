using System.Diagnostics;
using UnityEngine;

namespace CodeTestUnity.EntityRendering
{
	public abstract class EntityRenderer<TTarget> : MonoBehaviour
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TTarget renderTarget;
		public TTarget RenderTarget
		{
			get
			{
				return renderTarget;
			}
			set
			{
				if (renderTarget != null)
				{
					OnStopRendering(renderTarget);
				}

				renderTarget = value;

				OnStartRendering(value);
			}
		}

		protected abstract void OnStopRendering(TTarget target);
		protected abstract void OnStartRendering(TTarget target);
	}
}
