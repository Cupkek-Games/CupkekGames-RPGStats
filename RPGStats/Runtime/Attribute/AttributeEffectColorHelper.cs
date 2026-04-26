using CupkekGames.Luna;

namespace CupkekGames.RPGStats
{
    public static class AttributeEffectColorHelper
    {
        public static UIColor GetColor(AttributeEffect effect)
        {
            return GetColor(effect,
                new UIColor("sky", UIColorValue.V_400),
                new UIColor("red", UIColorValue.V_400),
                new UIColor("orange", UIColorValue.V_400));
        }

        public static UIColor GetColor(AttributeEffect effect, AttributeDisplayConfigSO config)
        {
            return GetColor(effect, config.PositiveColor, config.NegativeColor, config.MixedColor);
        }

        private static UIColor GetColor(AttributeEffect effect, UIColor positive, UIColor negative, UIColor mixed)
        {
            if (effect == null || effect.IsEmpty())
            {
                return positive;
            }

            int positiveCount = 0;
            int negativeCount = 0;
            const float epsilon = 0.001f;

            foreach (var entry in effect.Entries)
            {
                if (entry.AttributeKey.IsEmpty) continue;

                if (entry.Additive > 0) positiveCount++;
                else if (entry.Additive < 0) negativeCount++;

                if (entry.Multiplier > 1f + epsilon) positiveCount++;
                else if (entry.Multiplier < 1f - epsilon) negativeCount++;
            }

            if (positiveCount > 0 && negativeCount > 0)
                return mixed;
            else if (negativeCount > 0)
                return negative;
            else
                return positive;
        }
    }
}
