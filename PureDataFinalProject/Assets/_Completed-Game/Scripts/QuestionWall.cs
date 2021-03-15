using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionWall : MonoBehaviour
{
    public Question wallQuestion;
    [SerializeField] TMP_Text questionText;
    [SerializeField] GameObject answerToken;
    [SerializeField] float arenaLength;

    public void Start(){
        questionSetup();
        //StartCoroutine(LowerWall());
    }

    public void questionSetup(){
        questionText.text = wallQuestion.WallQuestion;
        makeAnswers();
    }

    public void makeAnswers(){
        Answer[] newAnswers = randomizeAnswers(wallQuestion.Answers);
        for(int i =0; i < newAnswers.Length; i++){

            float offset = (arenaLength*2f)/ (float) (newAnswers.Length-1);
            offset = offset * i;
            Debug.Log(offset);
            offset -= arenaLength;
            GameObject newAnswer = Instantiate(answerToken, new Vector3(offset, 1, transform.position.z-4), Quaternion.identity) as GameObject;
            newAnswer.GetComponent<Rotator>().answer = newAnswers[i];
            newAnswer.transform.GetChild(0).GetComponent<TMP_Text>().text = newAnswers[i].AnswerText;
            newAnswer.transform.parent = GameObject.Find("Pick Ups").transform;

            
            Color objectColor = new Color(
                (float)Random.Range(0, 255)/255f, 
                (float)Random.Range(0, 255)/255f, 
                (float)Random.Range(0, 255)/255f
            );
            Debug.Log(objectColor);
            newAnswer.GetComponent<Renderer>().material.color = objectColor;
        }
    }

    public IEnumerator LowerWall(){
        while(transform.position.y > -8){
            transform.Translate(new Vector3(0,-4,0) * Time.deltaTime);
            yield return new WaitForSeconds(0.003f);
        }
        
    }

    public Answer[] randomizeAnswers(Answer[] answers){
        Answer[] newAnswers = answers;
        System.Random rand = new System.Random();
        for(int i = 0; i < newAnswers.Length-1; i++){
            int j = rand.Next(i, newAnswers.Length);
            Answer temp = newAnswers[i];
            newAnswers[i] = newAnswers[j];
            newAnswers[j] = temp;
        }
        return newAnswers;
    }
    
   
}
