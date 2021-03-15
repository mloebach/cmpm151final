using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer> ().material.color = 
            GameObject.Find("Walls/QuestionWall").GetComponent<QuestionWall>().wallQuestion.FloorColor;
        //gameObject.GetComponent<Renderer>().material.color = new Color(0.5f,0.5f,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
