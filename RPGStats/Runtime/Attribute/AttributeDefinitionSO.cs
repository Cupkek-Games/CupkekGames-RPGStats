using UnityEngine;

namespace CupkekGames.RPGStats
{
    /// <summary>
    /// Defines a single combat attribute (e.g. Health, Attack, Crit Chance).
    /// Create one SO asset per attribute.
    /// Replaces the old <c>CombatAttributeType</c> enum — any game can define
    /// its own set of attributes without code changes.
    /// </summary>
    [CreateAssetMenu(fileName = "CombatAttribute", menuName = "CupkekGames/RPGStats/Attribute Definition")]
    public class AttributeDefinitionSO : ScriptableObject
    {
        [Tooltip("Full display name (e.g. 'Health', 'Critical Strike Chance')")]
        [SerializeField] private string _displayName;

        [Tooltip("Short display name (e.g. 'HP', 'CRIT%')")]
        [SerializeField] private string _displayNameShort;

        [Tooltip("Icon shown in UI")]
        [SerializeField] private Sprite _icon;

        [Header("Display Format")]
        [Tooltip(".NET format string (e.g. 'N0' for integer, 'N1' for one decimal)")]
        [SerializeField] private string _formatString = "N0";

        [Tooltip("Multiplied with the raw value before formatting (e.g. 100 for percentage)")]
        [SerializeField] private float _displayMultiplier = 1f;

        [Tooltip("Appended after the formatted number (e.g. '%')")]
        [SerializeField] private string _suffix = "";

        [Tooltip("Whether computed additive values should be rounded to integers")]
        [SerializeField] private bool _roundToInteger = true;

        [Tooltip("Global base value added to every unit (e.g. 1.5 for base crit multiplier). 0 for most attributes.")]
        [SerializeField] private float _defaultBaseValue;

        public string DisplayName => _displayName;
        public string DisplayNameShort => _displayNameShort;
        public Sprite Icon => _icon;
        public bool RoundToInteger => _roundToInteger;
        public float DefaultBaseValue => _defaultBaseValue;

        /// <summary>
        /// Format a raw attribute value for display using the configured format string,
        /// display multiplier, and suffix.
        /// </summary>
        public string Beautify(float value)
        {
            return (value * _displayMultiplier).ToString(_formatString) + _suffix;
        }
    }
}
