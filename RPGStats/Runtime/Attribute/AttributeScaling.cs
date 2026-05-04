using System;
using System.Collections.Generic;
using UnityEngine;

namespace CupkekGames.RPGStats
{
    [Serializable]
    public class AttributeScalingEntry
    {
        public AttributeDefinitionSO Attribute;
        public AttributeScalingTierSO Scaling;
    }

    /// <summary>
    /// Per-attribute scaling table — maps each <see cref="AttributeDefinitionSO"/> to a
    /// <see cref="AttributeScalingTierSO"/> that drives level-based stat multipliers.
    /// Generic: works for any RPG-stats system, not just combat.
    /// </summary>
    [Serializable]
    public class AttributeScaling
    {
        [SerializeField] private List<AttributeScalingEntry> _entries = new();

        [NonSerialized] private Dictionary<AttributeDefinitionSO, AttributeScalingTierSO> _lookup;

        public float GetStatMultiplier(AttributeDefinitionSO attribute, int level, float tierMultiplier)
        {
            EnsureLookup();
            if (_lookup.TryGetValue(attribute, out AttributeScalingTierSO tier) && tier != null)
                return tier.GetMultiplier(level, tierMultiplier);
            return 1f;
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;
            _lookup = new Dictionary<AttributeDefinitionSO, AttributeScalingTierSO>();
            foreach (AttributeScalingEntry entry in _entries)
            {
                if (entry.Attribute != null) _lookup[entry.Attribute] = entry.Scaling;
            }
        }
    }
}
