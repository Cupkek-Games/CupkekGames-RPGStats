using System;
using System.Collections.Generic;
using UnityEngine;

namespace CupkekGames.RPGStats
{
  [CreateAssetMenu(fileName = "AttributeEffectSO", menuName = "CupkekGames/RPGStats/Buff")]
  public class AttributeEffectSO : ScriptableObject
  {
    public AttributeEffect Data;
  }
}