using CodeTest.Game.Simulation.Models;
using RPGCore.Events;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace CodeTestUnity
{
	public class HudScreen : MonoBehaviour, IRenderer<WorldPlayer>
	{
		public class TextUpdater<T> : IEventFieldHandler
		{
			private readonly EventField<T> field;
			private readonly Text text;
			private readonly Func<T, string> toStringImpl;

			public TextUpdater(EventField<T> field, Text text, Func<T, string> toStringImpl)
			{
				this.field = field;
				this.text = text;
				this.toStringImpl = toStringImpl;
			}

			public void OnAfterChanged()
			{
				text.text = toStringImpl == null
					? (field.Value?.ToString())
					: toStringImpl(field.Value);
			}

			public void OnBeforeChanged()
			{
			}
		}

		[SerializeField] private GameObject hudContainer;
		[SerializeField] private Text scoreText;
		[SerializeField] private GameObject timeRemainingContainer;
		[SerializeField] private Text timeRemainingText;
		[SerializeField] private Text highscoreText;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private WorldPlayer renderTarget;
		public WorldPlayer RenderTarget
		{
			get
			{
				return renderTarget;
			}
			set
			{
				if (renderTarget != null)
				{
					renderTarget.CurrentScore.Handlers[this].Clear();
					renderTarget.Player.HighScore.Handlers[this].Clear();
				}

				renderTarget = value;

				if (renderTarget == null)
				{
					scoreText.text = "-";
					timeRemainingText.text = "-:--";
					highscoreText.text = "-";
				}
				else
				{
					renderTarget.CurrentScore.Handlers[this].AddAndInvoke(new TextUpdater<int>(
						renderTarget.CurrentScore,
						scoreText,
						score => score.ToString("###,##0")));

					renderTarget.Player.HighScore.Handlers[this].AddAndInvoke(new TextUpdater<int>(
						renderTarget.Player.HighScore,
						highscoreText,
						score => score.ToString("###,##0")));
				}
			}
		}

		private void Update()
		{
			if (renderTarget == null)
			{
				return;
			}

			double timeRemainingSeconds = renderTarget.World.TimeRemaining.AsDouble;
			timeRemainingSeconds = Math.Max(0.0, timeRemainingSeconds);
			var timeRemaining = TimeSpan.FromSeconds(timeRemainingSeconds);
			timeRemainingText.text = $"{timeRemaining.Minutes}:{timeRemaining.Seconds:00}";
		}

		public void Show()
		{
			hudContainer.SetActive(true);
		}

		public void Hide()
		{
			hudContainer.SetActive(false);
		}

		public void HideTime()
		{
			timeRemainingContainer.SetActive(false);
		}

		public void ShowTime()
		{
			timeRemainingContainer.SetActive(true);
		}
	}
}
