using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Data", menuName = "List Card Data")]
public class ListData : ScriptableObject
{
    public List<CardData> cardDatas = new List<CardData>();
}
