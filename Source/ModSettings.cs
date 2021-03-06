﻿using Verse;
using UnityEngine;

namespace RTMadSkills
{
	public class ModSettings : Verse.ModSettings
	{
		public static bool tiered = false;
		private static int multiplierPercentage = 0;
		public static float multiplier
		{
			get
			{
				return multiplierPercentage / 100.0f;
			}
			set
			{
				multiplierPercentage = Mathf.RoundToInt(multiplier * 100);
			}
		}
		public static float dailyXPSaturationThreshold = 4000.0f;
		public static float saturatedXPMultiplier
		{
			get
			{
				return saturatedXPmultiplierPercentage / 100.0f;
			}
			set
			{
				saturatedXPmultiplierPercentage = Mathf.RoundToInt(multiplier * 100);
			}
		}
		private static int saturatedXPmultiplierPercentage = 20;

		public override void ExposeData()
		{
			float multiplier_shadow = multiplier;
			float saturatedXPMultiplier_shadow = saturatedXPMultiplier;
			base.ExposeData();
			Scribe_Values.Look(ref tiered, "tiered");
			Scribe_Values.Look(ref multiplier_shadow, "multiplier");
			Scribe_Values.Look(ref dailyXPSaturationThreshold, "dailyXPSaturationThreshold");
			Scribe_Values.Look(ref saturatedXPMultiplier_shadow, "saturatedXPMultiplier");
			Log.Message("[MadSkills]: settings initialized, multiplier is " + multiplier_shadow
				+ ", " + (tiered ? "tiered" : "not tiered")
				+ ", daily XP threshold is " + dailyXPSaturationThreshold
				+ ", saturated XP multiplier is " + saturatedXPMultiplier);
			multiplierPercentage = Mathf.RoundToInt(multiplier_shadow * 100);
			saturatedXPmultiplierPercentage = Mathf.RoundToInt(saturatedXPMultiplier_shadow * 100);
		}

		public string SettingsCategory()
		{
			return "MadSkills_SettingsCategory".Translate();
		}

		public void DoSettingsWindowContents(Rect rect)
		{
			Listing_Standard list = new Listing_Standard(GameFont.Small);
			list.ColumnWidth = rect.width / 3;
			list.Begin(rect);
			list.Gap();
			{
				string buffer = dailyXPSaturationThreshold.ToString();
				Rect rectLine = list.GetRect(Text.LineHeight);
				Rect rectLeft = rectLine.LeftHalf().Rounded();
				Rect rectRight = rectLine.RightHalf().Rounded();
				Widgets.DrawHighlightIfMouseover(rectLine);
				TooltipHandler.TipRegion(rectLine, "MadSkills_DailyXPSaturationThresholdTip".Translate());
				TextAnchor anchorBuffer = Text.Anchor;
				Text.Anchor = TextAnchor.MiddleLeft;
				Widgets.Label(rectLeft, "MadSkills_DailyXPSaturationThresholdLabel".Translate());
				Text.Anchor = anchorBuffer;
				Widgets.TextFieldNumeric(rectRight, ref dailyXPSaturationThreshold, ref buffer, 0, 100000);
			}
			{
				string buffer = saturatedXPmultiplierPercentage.ToString();
				Rect rectLine = list.GetRect(Text.LineHeight);
				Rect rectLeft = rectLine.LeftHalf().Rounded();
				Rect rectRight = rectLine.RightHalf().Rounded();
				Rect rectPercent = rectRight.RightPartPixels(Text.LineHeight);
				rectRight = rectRight.LeftPartPixels(rectRight.width - Text.LineHeight);
				Widgets.DrawHighlightIfMouseover(rectLine);
				TooltipHandler.TipRegion(rectLine, "MadSkills_SaturatedXPMultiplierTip".Translate());
				TextAnchor anchorBuffer = Text.Anchor;
				Text.Anchor = TextAnchor.MiddleLeft;
				Widgets.Label(rectLeft, "MadSkills_SaturatedXPMultiplierLabel".Translate());
				Text.Anchor = anchorBuffer;
				Widgets.TextFieldNumeric(rectRight, ref saturatedXPmultiplierPercentage, ref buffer, 0, 10000);
				Widgets.Label(rectPercent, "%");
			}
			{
				string buffer = multiplierPercentage.ToString();
				Rect rectLine = list.GetRect(Text.LineHeight);
				Rect rectLeft = rectLine.LeftHalf().Rounded();
				Rect rectRight = rectLine.RightHalf().Rounded();
				Rect rectPercent = rectRight.RightPartPixels(Text.LineHeight);
				rectRight = rectRight.LeftPartPixels(rectRight.width - Text.LineHeight);
				Widgets.DrawHighlightIfMouseover(rectLine);
				TooltipHandler.TipRegion(rectLine, "MadSkills_MultiplierTip".Translate());
				TextAnchor anchorBuffer = Text.Anchor;
				Text.Anchor = TextAnchor.MiddleLeft;
				Widgets.Label(rectLeft, "MadSkills_MultiplierLabel".Translate());
				Text.Anchor = anchorBuffer;
				Widgets.TextFieldNumeric(rectRight, ref multiplierPercentage, ref buffer, 0, 10000);
				Widgets.Label(rectPercent, "%");
			}
			list.CheckboxLabeled(
				"MadSkills_TieredLabel".Translate(),
				ref tiered,
				"MadSkills_TieredTip".Translate());
			list.End();
		}
	}
}
