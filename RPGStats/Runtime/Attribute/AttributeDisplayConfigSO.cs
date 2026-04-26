using CupkekGames.Luna;
using UnityEngine;

namespace CupkekGames.RPGStats
{
    [CreateAssetMenu(menuName = "CupkekGames/RPGStats/Attribute Display Config")]
    public class AttributeDisplayConfigSO : ScriptableObject
    {
        [Header("UI Element Colors")]
        [SerializeField] UIColor _positiveColor = new("sky", UIColorValue.V_400);
        [SerializeField] UIColor _negativeColor = new("red", UIColorValue.V_400);
        [SerializeField] UIColor _mixedColor = new("orange", UIColorValue.V_400);

        [Header("Rich Text Colors")]
        [SerializeField] Color _positiveRichTextColor = new(0.569f, 0.922f, 0.851f, 1f);
        [SerializeField] Color _negativeRichTextColor = new(0.918f, 0.322f, 0.322f, 1f);

        public UIColor PositiveColor => _positiveColor;
        public UIColor NegativeColor => _negativeColor;
        public UIColor MixedColor => _mixedColor;

        public string PositiveRichTextTag => $"<color=#{ColorUtility.ToHtmlStringRGB(_positiveRichTextColor)}>";
        public string NegativeRichTextTag => $"<color=#{ColorUtility.ToHtmlStringRGB(_negativeRichTextColor)}>";
    }
}
