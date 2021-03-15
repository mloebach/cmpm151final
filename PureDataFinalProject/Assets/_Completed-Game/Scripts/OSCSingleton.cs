using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake(){
        int OSCSingletonCount = FindObjectsOfType<OSCSingleton>().Length;
        if(OSCSingletonCount > 1){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }else{
            intializeOSC();
            /*OSCHandler.Instance.Init();
		    OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthon", 1);
		    OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthchange", 49);*/
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start(){
        /*OSCHandler.Instance.Init();
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthon", 1);
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthchange", 49);
        Debug.Log("pass");*/
        //intializeOSC();
        Debug.Log("pass");
    }

    public void intializeOSC(){
        OSCHandler.Instance.Init();
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthon", 1);
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthchange", 49);
    }
}
