using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Opopnent", menuName = "Opponent Data")]
public class OpponentData : ScriptableObject
{
    public Sprite opponentImage;
    public string opponentName;
    public int opponentLevel;
}
