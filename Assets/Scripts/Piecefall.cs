using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Piecefall : MonoBehaviour {
	public float Time;


	//this script will make the pieces fall when you press the menu button
	IEnumerator waiter() {
		(gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
		yield return new WaitForSeconds (Time);
		SceneManager.LoadScene(0);
	}


	public void Wait() {
		StartCoroutine (waiter ());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
