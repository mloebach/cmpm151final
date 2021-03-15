using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColor : MonoBehaviour

{
    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer> ().material.color = 
        GameObject.Find("Walls/QuestionWall").GetComponent<QuestionWall>().wallQuestion.FloorColor;
        //gameObject.GetComponent<Renderer>().material.color = new Color(0.5f,0.5f,1);
        OSCHandler.Instance.Init();
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthon", 1);
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthchange", 49);
    }

    // Update is called once per frame
    void Update()
    {
        //************* Routine for receiving the OSC...
        
        
        OSCHandler.Instance.UpdateLogs();
        Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
        servers = OSCHandler.Instance.Servers;
        Debug.Log("oschandlerinstance:" + OSCHandler.Instance.Servers.Count);

        foreach (KeyValuePair<string, ServerLog> item in servers)
        {
            
            // If we have received at least one packet,
            // show the last received from the log in the Debug console
            if (item.Value.log.Count > 0)
            {
                int lastPacketIndex = item.Value.packets.Count - 1;

                //get address and data packet
                //countText.text = item.Value.packets[lastPacketIndex].Address.ToString();
                //countText.text += item.Value.packets[lastPacketIndex].Data[0].ToString();
                Debug.Log("floorcolor.cs:" + item.Value.packets[lastPacketIndex].Data[0].ToString());

                colorChange(item.Value.packets[lastPacketIndex].Data[0].ToString());
        
            }
        }
        
        //*************
    }

    public void colorChange(string change){
        float multi = 1;
        if(change == "1"){
            multi = 1.5f;
        }else if(change == "0"){
            multi = 0.5f;
        }
        Color tempColor = GameObject.Find("Walls/QuestionWall").GetComponent<QuestionWall>().wallQuestion.FloorColor;
        Color newColor = new Color (tempColor.r * multi, tempColor.g * multi, tempColor.b * multi );
        gameObject.GetComponent<Renderer> ().material.color = newColor;
    }
}
