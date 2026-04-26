using System;
using System.Collections.Generic;
using UnityEngine;

namespace CupkekGames.RPGStats
{
    [Serializable]
    public class ElementRelationship
    {
        public ElementTypeDefinitionSO Attacker;
        public ElementTypeDefinitionSO Defender;
        public float DamageMultiplier = 1f;
    }

    [CreateAssetMenu(fileName = "ElementRelationshipTable", menuName = "CupkekGames/RPGStats/Element Relationship Table")]
    public class ElementRelationshipTableSO : ScriptableObject
    {
        [SerializeField] private List<ElementRelationship> _relationships = new();

        [NonSerialized] private Dictionary<(ElementTypeDefinitionSO, ElementTypeDefinitionSO), float> _lookup;

        public float GetMultiplier(ElementTypeDefinitionSO attacker, ElementTypeDefinitionSO defender)
        {
            if (attacker == null || defender == null) return 1f;

            EnsureLookup();
            if (_lookup.TryGetValue((attacker, defender), out float multiplier))
                return multiplier;
            return 1f;
        }

        /// <summary>
        /// Returns elements that deal extra damage to the given element (multiplier > 1).
        /// </summary>
        public List<ElementTypeDefinitionSO> GetWeaknesses(ElementTypeDefinitionSO defender)
        {
            var result = new List<ElementTypeDefinitionSO>();
            if (defender == null) return result;
            foreach (var r in _relationships)
            {
                if (r.Defender == defender && r.Attacker != null && r.DamageMultiplier > 1f)
                    result.Add(r.Attacker);
            }
            return result;
        }

        /// <summary>
        /// Returns elements that this element deals extra damage to (multiplier > 1).
        /// </summary>
        public List<ElementTypeDefinitionSO> GetStrengths(ElementTypeDefinitionSO attacker)
        {
            var result = new List<ElementTypeDefinitionSO>();
            if (attacker == null) return result;
            foreach (var r in _relationships)
            {
                if (r.Attacker == attacker && r.Defender != null && r.DamageMultiplier > 1f)
                    result.Add(r.Defender);
            }
            return result;
        }

        private void EnsureLookup()
        {
            if (_lookup == null)
            {
                _lookup = new Dictionary<(ElementTypeDefinitionSO, ElementTypeDefinitionSO), float>();
                foreach (var r in _relationships)
                {
                    if (r.Attacker != null && r.Defender != null)
                        _lookup[(r.Attacker, r.Defender)] = r.DamageMultiplier;
                }
            }
        }
    }
}
