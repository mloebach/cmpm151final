using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColor : MonoBehaviour

{
    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
    // Start is called before the first frame update
    void Start()
    {
        OSCHandler.Instance.Init();

        gameObject.GetComponent<Renderer> ().material.color = 
            GameObject.Find("Walls/QuestionWall").GetComponent<QuestionWall>().wallQuestion.FloorColor;
        //gameObject.GetComponent<Renderer>().material.color = new Color(0.5f,0.5f,1);
    }

    // Update is called once per frame
    void Update()
    {
        //************* Routine for receiving the OSC...
        /*
        OSCHandler.Instance.UpdateLogs();
        Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
        servers = OSCHandler.Instance.Servers;

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
                Debug.Log(item.Value.packets[lastPacketIndex].Data[0].ToString());
        */
            }
        }
        //*************
    }
}
