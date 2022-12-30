using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

	public int xPosition;
	public GameControl gameController;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		if (gameController.CheckPiece(xPosition) == true && gameController.Color == 1 && gameController.Playable == true && gameController.GameEnd == false) {
			gameController.UpdateArray (xPosition, gameController.Color);
			GameObject BluePiece = Instantiate (Resources.Load ("BluePiece")) as GameObject;
			BluePiece.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
			gameController.Color = 2;
			gameController.Playable = false;
			gameController.Wait(2);
		} else if (gameController.CheckPiece(xPosition) == true && gameController.Color == 2 && gameController.Playable == true && gameController.Gamemode == 0 && gameController.GameEnd == false) {
			gameController.UpdateArray (xPosition, gameController.Color);
			GameObject RedPiece = Instantiate (Resources.Load ("RedPiece")) as GameObject;
			RedPiece.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
			gameController.Color = 1;
			gameController.Playable = false;
			gameController.Wait (2);
		} else {
			Debug.Log ("Piece cannot be placed.");
		}
	}
}





/*
else {
				GameObject RedPiece = Instantiate (Resources.Load ("RedPiece")) as GameObject;
				RedPiece.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
				gameController.Color = 1;
			}  
 */
