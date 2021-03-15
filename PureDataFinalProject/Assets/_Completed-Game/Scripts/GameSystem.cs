using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Question> questions;
    public static int currentlevel = 0;
    public GameObject QuestionWall;
    void Start()
    {
        //gameObject.GetComponent<Renderer> ().material.color = QuestionWall.GetComponent<QuestionWall>().wallQuestion.FloorColor;
        if(currentlevel >= questions.Count){
            currentlevel = 0;
        }
        QuestionWall.GetComponent<QuestionWall>().wallQuestion = questions[currentlevel];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
