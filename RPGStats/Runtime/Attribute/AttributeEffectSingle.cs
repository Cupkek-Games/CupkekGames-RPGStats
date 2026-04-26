using System;
using UnityEngine;
using CupkekGames.Luna;

namespace CupkekGames.RPGStats
{
  [Serializable]
  public class AttributeEffectSingle
  {
    public float Add = 0;
    public float Multiply = 1;
    public bool ReversePositiveColor = false;
    public float Effect(float value)
    {
      return (value * Multiply) + Add;
    }

    public override string ToString()
    {
      return ToString("");
    }

    public string ToString(string prefix)
    {
      string raw = ToStringRaw(prefix, out bool isPositive);
      if (raw == null) return null;

      string color = isPositive ? RichTextColor.AQUA : RichTextColor.RED;
      return color + raw + "</color>";
    }

    public string ToString(string prefix, AttributeDisplayConfigSO config)
    {
      string raw = ToStringRaw(prefix, out bool isPositive);
      if (raw == null) return null;

      string color = isPositive ? config.PositiveRichTextTag : config.NegativeRichTextTag;
      return color + raw + "</color>";
    }

    private string ToStringRaw(string prefix, out bool isPositive)
    {
      string result = "";
      isPositive = false;

      if (Add != 0 || Multiply != 1)
      {
        result = "";

        if (Add != 0)
        {
          if (Add > 0)
          {
            result += "+";
            isPositive = true;
          }

          result += Add;
        }

        if (Multiply != 1)
        {
          if (result.Length > 0)
          {
            result += " & ";
          }

          if (Multiply > 1)
          {
            result += "+";
            isPositive = true;
          }
          else
          {
            result += "-";
          }

          float localMultiply = Mathf.Abs(1f - Multiply);
          float absolute = Mathf.Abs(localMultiply * 100);
          float rounded = Mathf.Round(absolute * 10f) / 10f;

          result += rounded + "%";
        }
      }

      if (result.Length == 0)
      {
        return null;
      }

      if (ReversePositiveColor)
      {
        isPositive = !isPositive;
      }

      return prefix + result;
    }

    public string GetRichTextColor()
    {
      if (Add > 0 || Multiply > 1)
      {
        return RichTextColor.AQUA;
      }

      return RichTextColor.RED;
    }

    public string GetRichTextColor(AttributeDisplayConfigSO config)
    {
      if (Add > 0 || Multiply > 1)
      {
        return config.PositiveRichTextTag;
      }

      return config.NegativeRichTextTag;
    }

    public AttributeEffectSingle MultiplyAll(float value)
    {
      return new AttributeEffectSingle
      {
        Add = Add * value,
        Multiply = AttributeEffect.MultiplyMultiplier(Multiply, value)
      };
    }
  }
}
