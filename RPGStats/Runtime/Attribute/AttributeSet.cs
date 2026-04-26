using System;
using System.Collections.Generic;
using CupkekGames.Data;
using UnityEngine;

namespace CupkekGames.RPGStats
{
    [Serializable]
    public class AttributeSetEntry
    {
        [CatalogKeyConstraint(RpgStatsConstants.AttributesCatalogId, typeof(ScriptableObject))]
        public CatalogKey AttributeKey;
        public float Value;
    }

    [Serializable]
    public class AttributeSet
    {
        [SerializeField] private List<AttributeSetEntry> _entries = new();

        public IReadOnlyList<AttributeSetEntry> Entries => _entries;

        public AttributeSet() { }

        public float GetValue(string attributeKey)
        {
            int i = FindFirstEntryIndex(attributeKey);
            return i >= 0 ? _entries[i].Value : 0f;
        }

        public float GetValue(AttributeDefinitionSO attribute)
        {
            return attribute != null ? GetValue(attribute.name) : 0f;
        }

        public void SetValue(string attributeKey, float value)
        {
            if (string.IsNullOrEmpty(attributeKey))
                return;

            int i = FindFirstEntryIndex(attributeKey);
            if (i >= 0)
            {
                _entries[i].Value = value;
            }
            else
            {
                _entries.Add(new AttributeSetEntry
                {
                    AttributeKey = new CatalogKey
                    {
                        Catalog = RpgStatsConstants.AttributesCatalogId,
                        Key = attributeKey
                    },
                    Value = value
                });
            }
        }

        public void SetValue(AttributeDefinitionSO attribute, float value)
        {
            if (attribute != null)
                SetValue(attribute.name, value);
        }

        public AttributeSet Clone()
        {
            var clone = new AttributeSet();
            foreach (AttributeSetEntry entry in _entries)
            {
                clone._entries.Add(new AttributeSetEntry
                {
                    AttributeKey = entry.AttributeKey,
                    Value = entry.Value
                });
            }

            return clone;
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
    }
}
