using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

	public Piecefall piecefall;
	public AudioSource buttonSource;

	// Use this for initialization
	void Start () {
		buttonSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		buttonSource.Play ();
		piecefall.Wait ();
	}
}
