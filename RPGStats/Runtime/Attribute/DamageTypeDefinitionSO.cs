using UnityEngine;

namespace CupkekGames.RPGStats
{
    /// <summary>
    /// Defines a damage type (e.g. Physical, Magical).
    /// Create one SO asset per damage type.
    /// Replaces the old <c>DamageType</c> enum — any game can define
    /// its own damage types without code changes.
    /// </summary>
    [CreateAssetMenu(fileName = "DamageType", menuName = "CupkekGames/RPGStats/Damage Type Definition")]
    public class DamageTypeDefinitionSO : ScriptableObject
    {
        [Tooltip("Display name (e.g. 'Physical', 'Magical')")]
        [SerializeField] private string _displayName;

        [Tooltip("Rich-text sprite tag for inline icon (e.g. '<sprite name=\"attr_atk\">')")]
        [SerializeField] private string _iconRichText;

        [Tooltip("Rich-text color tag (e.g. '<color=#FF0000>')")]
        [SerializeField] private string _richTextColor;

        [Tooltip("The attack attribute that scales this damage type (e.g. ATK for Physical, MATK for Magical)")]
        [SerializeField] private AttributeDefinitionSO _attackAttribute;

        [Tooltip("The defense attribute that resists this damage type (e.g. DEF for Physical, MDEF for Magical)")]
        [SerializeField] private AttributeDefinitionSO _defenseAttribute;

        public string DisplayName => _displayName;
        public string IconRichText => _iconRichText;
        public string RichTextColor => _richTextColor;
        public AttributeDefinitionSO AttackAttribute => _attackAttribute;
        public AttributeDefinitionSO DefenseAttribute => _defenseAttribute;
    }
}
