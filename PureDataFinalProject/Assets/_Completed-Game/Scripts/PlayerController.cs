using UnityEngine;
using UnityEngine.SceneManagement;
// Include the namespace required to use Unity UI
using UnityEngine.UI;
using TMPro;

using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public Text countText;
	public Text winText;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	private int count;

	private GameObject questionWall;
	public GameObject gameFloor;
	public GameSystem gameSystem;

	private Vector3 curPos, lastPos;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
		questionWall = GameObject.Find("Walls/QuestionWall");

		//Initialize the pd
		OSCHandler.Instance.Init();
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthon", 1);
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthchange", 49);
	}

	// Each physics step..
	void FixedUpdate ()
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce (movement * speed);

		if(transform.position.y <  -30){
			if(GameSystem.currentlevel >= gameSystem.questions.Count){
            	SceneManager.LoadScene("WinScene");
        	}else{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			
		}


		curPos = transform.position;
		//check to see if object is moving
		if(Mathf.Abs(rb.velocity.x) < 1 && Mathf.Abs(rb.velocity.z) < 1){
			Debug.Log("no movement");
			OSCHandler.Instance.SendMessageToClient("pd", "/unity/rolling", 0);
		}
		else{
			OSCHandler.Instance.SendMessageToClient("pd", "/unity/rolling", 1);
			Debug.Log("moving");
		}
		lastPos = curPos;

		
	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			//SetCountText ();

			GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("Pick Up");
			for(var i = 0 ; i < gameObjects.Length ; i ++){
				Destroy(gameObjects[i]);
			}

			if(other.gameObject.GetComponent<Rotator>().answer.Correct){
				questionWall.transform.Find("questionText").GetComponent<TMP_Text>().text = "Correct!";
				OSCHandler.Instance.SendMessageToClient("pd", "/unity/right", 1);
				StartCoroutine(questionWall.GetComponent<QuestionWall>().LowerWall());
				GameSystem.currentlevel++;
				OSCHandler.Instance.SendMessageToClient("pd", "/unity/synthchange", ((GameSystem.currentlevel) + 49));
			}
			else{
				questionWall.transform.Find("questionText").GetComponent<TMP_Text>().text = "<br>WRONG!";
				OSCHandler.Instance.SendMessageToClient("pd", "/unity/wrong", 1);
				gameFloor.SetActive(false);
			}
		}
	}

	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 3) 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
			//StartCoroutine(questionWall.GetComponent<QuestionWall>().LowerWall());
		}
	}
}