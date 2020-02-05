using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Create EffectsTable")]
public class EffectsTable : ScriptableObject
{
    public List<GameEffect> gameEffectList = new List<GameEffect>();
}