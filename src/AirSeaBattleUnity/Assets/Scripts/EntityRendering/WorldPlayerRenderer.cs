using AirSeaBattle.Game.Simulation.Models;
using AirSeaBattleUnity.EntityRendering;
using RPGCore.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AirSeaBattleUnity
{
	public class WorldPlayerRenderer : EntityRenderer<WorldPlayer>
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

		public GameObject TimeRemainingContainer => timeRemainingContainer;

		protected override void OnStartRendering(WorldPlayer target)
		{
			if (target == null)
			{
				scoreText.text = "-";
				timeRemainingText.text = "-:--";
				highscoreText.text = "-";
			}
			else
			{
				target.CurrentScore.Handlers[this].AddAndInvoke(new TextUpdater<int>(
					target.CurrentScore,
					scoreText,
					score => score.ToString("###,##0")));

				target.Player.Highscore.Handlers[this].AddAndInvoke(new TextUpdater<int>(
					target.Player.Highscore,
					highscoreText,
					score => score.ToString("###,##0")));
			}
		}

		protected override void OnStopRendering(WorldPlayer target)
		{
			target.CurrentScore.Handlers[this].Clear();
			target.Player.Highscore.Handlers[this].Clear();
		}

		private void Update()
		{
			if (RenderTarget == null)
			{
				return;
			}

			double timeRemainingSeconds = RenderTarget.World.TimeRemaining.AsDouble;
			timeRemainingSeconds = Math.Max(0.0, timeRemainingSeconds);
			var timeRemaining = TimeSpan.FromSeconds(timeRemainingSeconds);
			timeRemainingText.text = $"{timeRemaining.Minutes}:{timeRemaining.Seconds:00}";
		}
	}
}
