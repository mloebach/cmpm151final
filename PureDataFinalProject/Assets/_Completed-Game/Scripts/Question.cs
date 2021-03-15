using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Question")]
[System.Serializable]
public class Question : ScriptableObject
{
    [SerializeField] string question;
    [SerializeField] Color floorColor;

    [SerializeField] Answer[] answers;

    public string WallQuestion{
        get{return question;}
    }

    public Color FloorColor{
        get{return floorColor;}
    }

    public Answer[] Answers{
        get{return answers;}
    }
}

[System.Serializable]
public class Answer{
    public string answerText;
    public bool correct;

    public string AnswerText{
        get{ return answerText;}
    }
    public bool Correct{
        get{return correct;}
    }
}