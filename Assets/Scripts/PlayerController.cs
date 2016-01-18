using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VR = UnityEngine.VR;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		count = 0;
		setCountText();
		winText.text = "";
	}
	
	void FixedUpdate(){
		//Use arrow keys to add force.
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement*speed*50);


		Vector3 headRotation = VR.InputTracking.GetLocalRotation(VR.VRNode.CenterEye).eulerAngles;
		float headPitch = headRotation.x; //Up&Down (Pitch)
		float headYaw = headRotation.y; //Left&Right (Yaw)
		float headRoll = headRotation.z; //Tilt of head to one side (Roll).

		Vector3 headForce = new Vector3 (unDisconnectAngle(headRoll), 0.0f, -1*unDisconnectAngle(headPitch));

		//If player was looking dead-ahead, then headForce would not need adjusting. But if they are looking East, then tilting head forward should
		// send them east. So rotate the force 
		Quaternion rotateBy = Quaternion.Euler (0, headYaw, 0); 

		rb.AddForce (rotateBy*headForce*speed);

		//Reset player position to the start if they press the back button.
		if( Input.GetKeyDown(KeyCode.Escape) ){
			SceneManager.LoadScene("VRMiniGame");
			/*this.transform.position = new Vector3 (0, 4, 0);
			count = 0;
			setCountText ();
			print (GameObject.FindGameObjectsWithTag ("Pick up"));
			foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("Pick up")) {
				pickup.SetActive (true);
			}*/
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick up")) {
			other.gameObject.SetActive (false);
			count++;
			setCountText ();
		}
	}

	void setCountText(){
		countText.text = "Count: " + count.ToString ();

		if (count >= 8) {
			winText.text = "You Win!";
		}
	}

	//Converts an angle to be in range [-180, 180] and puts 1 and 359 next to each other (at -1, +1) so the disconnect is behind the user at 180 and can't be "looked across".
	float unDisconnectAngle(float angle){
		if (angle >= 0 && angle < 180) {
			return -1 * angle;
		} else {
			return -1*angle+360;
		}
	}
}

