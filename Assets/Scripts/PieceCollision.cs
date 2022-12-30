using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollision : MonoBehaviour {

	private int Collision = 0;
	public AudioSource Piecesource;


	// Use this for initialization
	void Start () {
		Piecesource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter () {
		if (Collision < 2) {
			Piecesource.Play ();
			Collision++;
		}
	}
}
