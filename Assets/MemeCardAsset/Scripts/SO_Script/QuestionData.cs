using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Question Data")]
public class QuestionData : ScriptableObject
{
    public string question;
    public EmotionalType questionCardEmotionalType;
}
