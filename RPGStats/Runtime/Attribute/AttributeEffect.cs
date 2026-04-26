using System;
using System.Collections.Generic;
using CupkekGames.Data;
using UnityEngine;

namespace CupkekGames.RPGStats
{
    [Serializable]
    public class AttributeEffectEntry
    {
        [CatalogKeyConstraint(RpgStatsConstants.AttributesCatalogId, typeof(ScriptableObject))]
        public CatalogKey AttributeKey;

        [Header("Add")] public float Additive;
        [Header("Multiply")] public float Multiplier = 1f;

        public AttributeEffectEntry() { }

        public AttributeEffectEntry(AttributeEffectEntry other)
        {
            if (other == null)
                return;
            AttributeKey = other.AttributeKey;
            Additive = other.Additive;
            Multiplier = other.Multiplier;
        }
    }

    [Serializable]
    public class AttributeEffect
    {
        [SerializeField] private List<AttributeEffectEntry> _entries = new();

        public AttributeEffect() { }

        public IReadOnlyList<AttributeEffectEntry> Entries => _entries;

        public float GetAdditive(string attributeKey)
        {
            int i = FindFirstEntryIndex(attributeKey);
            return i >= 0 ? _entries[i].Additive : 0f;
        }

        public float GetMultiplier(string attributeKey)
        {
            int i = FindFirstEntryIndex(attributeKey);
            return i >= 0 ? _entries[i].Multiplier : 1f;
        }

        public float GetAdditive(AttributeDefinitionSO attribute)
        {
            return attribute != null ? GetAdditive(attribute.name) : 0f;
        }

        public float GetMultiplier(AttributeDefinitionSO attribute)
        {
            return attribute != null ? GetMultiplier(attribute.name) : 1f;
        }

        public bool DoesChange(string attributeKey)
        {
            return GetAdditive(attributeKey) != 0f || !Mathf.Approximately(GetMultiplier(attributeKey), 1f);
        }

        public bool DoesChange(AttributeDefinitionSO attribute)
        {
            return attribute != null && DoesChange(attribute.name);
        }

        public AttributeEffect Combine(AttributeEffect other)
        {
            var result = new AttributeEffect();
            var seen = new HashSet<CatalogKey>();

            foreach (AttributeEffectEntry entry in _entries)
            {
                if (entry.AttributeKey.IsEmpty)
                    continue;
                seen.Add(entry.AttributeKey);
                string k = entry.AttributeKey.Key;
                result._entries.Add(new AttributeEffectEntry
                {
                    AttributeKey = entry.AttributeKey,
                    Additive = entry.Additive + other.GetAdditive(k),
                    Multiplier = entry.Multiplier * other.GetMultiplier(k),
                });
            }

            foreach (AttributeEffectEntry entry in other._entries)
            {
                if (entry.AttributeKey.IsEmpty || seen.Contains(entry.AttributeKey))
                    continue;
                result._entries.Add(new AttributeEffectEntry
                {
                    AttributeKey = entry.AttributeKey,
                    Additive = entry.Additive,
                    Multiplier = entry.Multiplier,
                });
            }

            return result;
        }

        public AttributeEffect MultiplyAll(float value)
        {
            var result = new AttributeEffect();
            foreach (AttributeEffectEntry entry in _entries)
            {
                AttributeDefinitionSO def = AttributeDefinitionResolver.TryGet(entry.AttributeKey.Key);
                float additive = def != null && def.RoundToInteger
                    ? Mathf.RoundToInt(entry.Additive * value)
                    : entry.Additive * value;

                result._entries.Add(new AttributeEffectEntry
                {
                    AttributeKey = entry.AttributeKey,
                    Additive = additive,
                    Multiplier = MultiplyMultiplier(entry.Multiplier, value),
                });
            }

            return result;
        }

        public static float MultiplyMultiplier(float multiplier, float value)
        {
            return 1 + (Mathf.Sign(multiplier - 1) * (Mathf.Abs(1 - multiplier) * value));
        }

        public List<string> GetStringEffectList()
        {
            var result = new List<string>();
            foreach (AttributeEffectEntry entry in _entries)
            {
                if (entry.AttributeKey.IsEmpty)
                    continue;
                string effect = GetStringEffect(entry.AttributeKey.Key);
                if (effect != null)
                    result.Add(effect);
            }

            return result;
        }

        public string GetStringEffect(string attributeKey)
        {
            AttributeDefinitionSO attribute = AttributeDefinitionResolver.TryGet(attributeKey);
            if (attribute == null)
                return null;

            var effect = new AttributeEffectSingle
            {
                Add = GetAdditive(attributeKey),
                Multiply = GetMultiplier(attributeKey)
            };

            string prefix = attribute.DisplayName + ": ";
            return effect.ToString(prefix);
        }

        public string GetStringEffect(AttributeDefinitionSO attribute)
        {
            return attribute != null ? GetStringEffect(attribute.name) : null;
        }

        public bool IsEmpty()
        {
            if (_entries == null || _entries.Count == 0)
                return true;
            foreach (AttributeEffectEntry entry in _entries)
            {
                if (entry.Additive != 0f || !Mathf.Approximately(entry.Multiplier, 1f))
                    return false;
            }

            return true;
        }

        private int FindFirstEntryIndex(string attributeKey)
        {
            if (string.IsNullOrEmpty(attributeKey))
                return -1;

            for (int i = 0; i < _entries.Count; i++)
            {
                if (string.Equals(_entries[i].AttributeKey.Key, attributeKey, StringComparison.Ordinal))
                    return i;
            }

            return -1;
        }

        public AttributeEffect(AttributeEffect other)
        {
            if (other?._entries == null)
                return;
            for (int i = 0; i < other._entries.Count; i++)
                _entries.Add(new AttributeEffectEntry(other._entries[i]));
        }
    }
}
