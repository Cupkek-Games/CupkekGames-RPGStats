using UnityEngine;

namespace CupkekGames.RPGStats
{
    /// <summary>
    /// Generic per-tier scaling profile (Low / Mid / High / etc.) — pure level-scaling math, no combat coupling.
    /// </summary>
    [CreateAssetMenu(fileName = "AttributeScalingTier", menuName = "CupkekGames/RPGStats/Attribute Scaling Tier")]
    public class AttributeScalingTierSO : ScriptableObject
    {
        [SerializeField] private float _growthRate;
        public float GrowthRate => _growthRate;

        public float GetMultiplier(int level, float tierMultiplier)
        {
            if (_growthRate == 0f) return 1f;
            return 1 + ((level - 1) * _growthRate * tierMultiplier);
        }
    }
}
