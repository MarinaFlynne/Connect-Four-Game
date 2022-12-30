using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour {

	private static int GameMode = 0;
	public static int GetGameMode () { 
		return GameMode; 
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CPUButton() {
		GameMode = 1;
		SceneManager.LoadScene(1);
	}

	public void PlayerButton () {
		GameMode = 0;
		SceneManager.LoadScene(1);
	}
}
