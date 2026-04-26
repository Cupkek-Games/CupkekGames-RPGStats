using UnityEngine;

namespace CupkekGames.RPGStats
{
    [CreateAssetMenu(fileName = "ElementType", menuName = "CupkekGames/RPGStats/Element Type Definition")]
    public class ElementTypeDefinitionSO : ScriptableObject
    {
        [SerializeField] private string _displayName;
        [SerializeField] private string _ussClassName;

        public string DisplayName => _displayName;
        public string USSClassName => _ussClassName;
    }
}
