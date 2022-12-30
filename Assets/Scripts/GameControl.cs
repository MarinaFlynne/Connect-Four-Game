using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	public int[,] Board = new int[7, 6];	//int array of the board which stores all the positions of the pieces
	public int Color = 1;						//int that controls whose turn it is. 1 is blue, 2 is red
	public GameObject BlueWin;					//GameObject for the text for when Blue wins
	public GameObject RedWin;					//GameObjecct for the text for when Red wins
	public GameObject Draw;
	public bool GameEnd = false;				//Boolean that tracks whether or not the game has ended
	public int lastPosx;						//int that stores the x position of the last piece that was played
	public int lastPosy;						//int that stores the y position of the last piece that was played
	private int Diagonalx = -1;
	private int Diagonaly = -1;
	private bool Diagonal = false;
	public int Gamemode;
	public bool Playable = true;				//bool that stores whether or not the player can play right now

	private void Awake () {
		Gamemode = MenuManage.GetGameMode ();
		Debug.Log (Gamemode);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AiMove () {
		bool a = true;
		int placement = -1;

		int newx = lastPosx;
		int newy = lastPosy;

		int Counter = 1;
		int prevColor = -2;	//tracks the color of the previously checked position
		int color = Board [lastPosx, lastPosy];

			//Checks if there is a vertical stack of three pieces of the same colour. If there is, then it plays on top of that stack.
			for (int i = 0; i < 7; i++) {
				for (int j = 0; j < 6; j++) {
					if ((Board [i, j] == prevColor) && Board [i, j] != 0) {
						Counter++;
					} else {
						Counter = 1;
					}
					prevColor = Board [i, j];
					if (Counter >= 3 && j + 1 <= 5) {
						if (Board [i, j + 1] == 0) {
							placement = i;
							//Debug.Log ("Test" + placement);
							if (CheckPiece (placement) != true) {
								placement = -1;
								//Debug.Log ("Vertical Place");
								Counter = 1;
							}
						}
					}
				}
				Counter = 1;
			}

		Counter = 1;
		prevColor = -2;
		//Checks if there are three pieces of the same colour in a row. If there is, then it plays beside that stack.
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 7; j++) {
				if ((Board [j, i] == prevColor) && Board [j, i] != 0) {
					Counter++;
				} else {
					Counter = 1;
				}
				prevColor = Board [j, i];
				if (Counter >= 3 && CheckValueInArray(j + 1,i) == 0 && CheckValueInArray(j + 1,i) !=-1 && CheckYPos(j + 1) == i) {
					placement = j + 1;
					if (CheckPiece (placement) != true) {
						placement = -1;
						Counter = 1;
					}
					
				} else if (Counter >= 3 && CheckValueInArray(j - 3,i) == 0 && CheckValueInArray(j - 3,i) !=-1 && CheckYPos(j - 3) == i) {
					placement = j - 3;
					if (CheckPiece (placement) != true) {
						placement = -1;
						Counter = 1;
					}
				}
			}
			Counter = 1;
			prevColor = -2;
		}

		//DIAGONAL CHECKING
		Counter = 1;
		prevColor = -2;

		//diagonal left down
		if (Diagonal == false) {
			newx = lastPosx;
			newy = lastPosy;
			//counter = 1;
			for (int i = 0; i < 2; i++) {
				newx--;
				newy--;
				if (newx >= 7 || newy >= 6 || newx < 0 || newy < 0) {
					i = 3;
				} else {
					if (Board [newx, newy] == color) {
						Counter++;
					} else {
						i = 3;
					}
				}
				if (Counter >= 3 && CheckValueInArray (newx - 1, newy - 1) != -1 && newx >= 0 && newy >= 0) {
					Diagonalx = newx - 1;
					Diagonaly = newy - 1;
					Diagonal = true;
					Debug.Log ("DIAGONAL AT X = " + newx + " Y = " + newy);
				}
			}
		}


		if (Diagonal == false) {
			newx = lastPosx;
			newy = lastPosy;
			//diagonal right up
			for (int i = 0; i < 2; i++) {
				newx++;
				newy++;
				if (newx >= 7 || newy >= 6 || newx < 0 || newy < 0) {
					i = 3;
				} else {
					if (Board [newx, newy] == color) {
						Counter++;
					} else {
						i = 3;
					}
				}
				if (Counter >= 3 && CheckValueInArray (newx, newy) != -1 && newx >= 0 && newy >= 0) {
					Diagonalx = newx;
					Diagonaly = newy;
					Diagonal = true;
					Debug.Log ("DIAGONAL AT X = " + newx + " Y = " + newy);
				}
			}
		}

		Counter = 1;
		//diagonal right down
		if (Diagonal == false) {
			newx = lastPosx;
			newy = lastPosy;
			Counter = 1;
			for (int i = 0; i < 2; i++) {
				newx++;
				newy--;
				if (newx >= 7 || newy >= 6 || newx < 0 || newy < 0) {
					i = 3;
				} else {
					if (Board [newx, newy] == color) {
						Counter++;
					} else {
						i = 3;
					}
				}
				if (Counter >= 3 && CheckValueInArray (newx + 1, newy - 1) != -1 && newx >= 0 && newy >= 0) {
					Diagonalx = newx + 1;
					Diagonaly = newy - 1;
					Diagonal = true;
					Debug.Log ("DIAGONAL AT X = " + newx + " Y = " + newy);
				}
			}
		}
		//diagonal left up
		if (Diagonal == false) {
			newx = lastPosx;
			newy = lastPosy;
			for (int i = 0; i < 2; i++) {
				newx--;
				newy++;
				if (newx >= 7 || newy >= 6 || newx < 0 || newy < 0) {
					i = 3;
				} else {
					if (Board [newx, newy] == color) {
						Counter++;
					} else {
						i = 3;
					}
				}
				if (Counter >= 3 && CheckValueInArray (newx, newy) != -1 && newx >= 0 && newy >= 0) {
					Diagonalx = newx;
					Diagonaly = newy;
					Diagonal = true;
					Debug.Log ("DIAGONAL AT X = " + newx + " Y = " + newy);
				}
			}
		}

		if (Diagonalx != -1 && CheckYPos(Diagonalx) == Diagonaly) {
			placement = Diagonalx;
			Debug.Log ("Diagonal Placed");
			Diagonalx = -1;
			Diagonaly = -1;
			Diagonal = false;
		}

		Counter = 1;
		prevColor = -2;
		//Checks if there are two pieces of the same colour in a row. If there is, then it plays beside that stack.
		if (placement == -1) {
			for (int i = 0; i < 6; i++) {
				for (int j = 0; j < 7; j++) {
					if ((Board [j, i] == prevColor) && Board [j, i] != 0) {
						Counter++;
					} else {
						Counter = 1;
					}
					prevColor = Board [j, i];
					if (Counter >= 2 && CheckValueInArray (j + 1, i) == 0 && CheckValueInArray (j + 1, i) != -1 && CheckYPos (j + 1) == i) {
						placement = j + 1;
						if (CheckPiece (placement) != true) {
							placement = -1;
							Counter = 1;
						}

					} else if (Counter >= 2 && CheckValueInArray (j - 2, i) == 0 && CheckValueInArray (j - 2, i) != -1 && CheckYPos (j - 2) == i) {
						placement = j - 2;
						if (CheckPiece (placement) != true) {
							placement = -1;
							Counter = 1;
						}
					}
				}
				Counter = 1;
				prevColor = -2;
			}
		}

		//if no other piece placement has been decided, play randomly.
		if (placement == -1) {
			while (a == true) {
				placement = (Random.Range (0, 7));
				if (CheckPiece (placement) == true) {
					PlacePiece (placement);
					Debug.Log ("Random");
					a = false;
				}
			}
		} else {
			PlacePiece (placement);
		}
	}

	//this method starts the waiter method
	public void Wait (int time) {
		StartCoroutine (waiter (time));
	}

	//this method waits in between player turns
	IEnumerator waiter(int time) {
		if (GameEnd == false) {
			if (Gamemode == 1) {
				yield return new WaitForSeconds (time);
				AiMove ();
				yield return new WaitForSeconds (1f);
				Playable = true;
				Color = 1;
			} else {
				yield return new WaitForSeconds (1f);
				Playable = true;
			}
		}
	}

	//this method checks the value of a position in the Board array. It returns -1 if the value does not exist in the array
	private int CheckValueInArray(int xpos, int ypos) {
		if (xpos >= 0 && xpos <= 6 && ypos >= 0 && ypos <= 5) {
			return Board [xpos, ypos];
		} else {
			return -1;
		}
	}

	//this method is for the piece placement of the ai
	public void PlacePiece (int posx) {
		if(CheckPiece(posx) == true) {
			float position = -1;
			if (posx == 0) {
				position = -9f;
			} else if (posx == 1) {
				position = -6f;
			} else if (posx == 2) {
				position = -3f;
			} else if (posx == 3) {
				position = 0f;
			} else if (posx == 4) {
				position = 3f;
			} else if (posx == 5) {
				position = 6f;
			} else if (posx == 6) {
				position = 9f;
			} else {
				Debug.Log("ERROR Invalid Piece Position.");
			}
			GameObject RedPiece = Instantiate (Resources.Load ("RedPiece")) as GameObject;
			RedPiece.transform.position = new Vector3 (1.5f, 15.43f, position);
			UpdateArray (posx, 2);
		}
	}

	//places the piece in the array and returns whether or not the piece can go there
	public void UpdateArray (int posx, int color) {
		int posy = CheckYPos (posx);

		//color: nothing is 0, blue is 1, red is 2
		Board[posx, posy] = color;
		lastPosx = posx;
		lastPosy = posy;
		WinCheck (posx, posy);
	}

	private bool FullArrayCheck() {
		for (int i = 0; i < 6; i++) {
			if (CheckYPos (i) != -1) {
				return false;
			}
		}
		return true;
	}


	//Checks for the first empty y value for a given x value
	public int CheckYPos(int posx) {
		int i = 0;
		while (true) {
			if (posx > 6 || posx < 0) {
				return -1;
			} else {
				if (Board [posx, i] == 0) {
					return i;
				} else {
					i++;
				}
				if (i >= 6 || i < 0) {
					return -1;
				}
			}
		}
	}
	//checks if a piece can be placed in a position.
	public bool CheckPiece(int xpos) {
		if (CheckYPos (xpos) == -1) {
			return false;
		} else {
			return true;
		}
	}

	//Checks if anyone has won, takes the value for the position of the last placed piece
	private bool WinCheck(int posx, int posy) {

		if (CheckPositions (posx, posy) == 1) {
			Debug.Log ("Blue Wins!");
			BlueWin.SetActive (true);
			GameEnd = true;
		} else if (CheckPositions (posx, posy) == 2) {
			Debug.Log ("Red Wins!");
			RedWin.SetActive (true);
			GameEnd = true;
		} else if (FullArrayCheck() == true) {
			Debug.Log ("Draw.");
			Draw.SetActive (true);
			GameEnd = true;
		}
		return false;
	}

	//Checks the positions on the board to see if anyone has won
	private int CheckPositions(int posx, int posy) {
		int Counter = 1;
		int prevColor = -2;	//tracks the color of the previously checked position

		//Vertical Check
		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < 6; j++) {
				if ((Board [i, j] == prevColor) && Board [i, j] != 0) {
					Counter++;
				} else {
					Counter = 1;
				}
				prevColor = Board [i, j];
				if (Counter >= 4) {
					Debug.Log ("Vertical Win");
					return Board [i, j];
				}
			}
			Counter = 1;
			prevColor = -2;
		}
	
		//Horizontal Check
		prevColor = -2;
		Counter = 1;
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 7; j++) {
				if ((Board [j, i] == prevColor) && Board [j, i] != 0) {
					Counter++;
				} else {
					Counter = 1;
				}
				prevColor = Board [j, i];
				if (Counter >= 4) {
					Debug.Log ("Horizontal Win");
					return Board [j, i];
				}
			}
			Counter = 1;
			prevColor = -2;
		}

		//Diagonal Check
		if (CheckDiagonal(posx, posy) == true) {
			return Board [posx, posy];
		}

		return 0;
	}

	//Checks the diagonal positions on the board to see if anyone has won. takes the values for the position of the last placed piece as input
	private bool CheckDiagonal (int posx, int posy) {

		int newx = posx;
		int newy = posy;
		int color = Board [posx, posy];
		int counter = 1;

		//diagonal right up
		for (int i = 0; i < 3; i++) {
			newx++;
			newy++;
			if (newx >= 7 || newy >= 6 || newx < 0 || newy < 0) {
				i = 3;
			} else {
				if (Board [newx, newy] == color) {
					counter++;
				} else {
					i = 3;
				}
			}
			if (counter >= 4) {
				Debug.Log ("DRU");
				return true;
			}
		}

		//diagonal left down
		newx = posx;
		newy = posy;
		for (int i = 0; i < 3; i++) {
			newx--;
			newy--;
			if (newx >= 7 || newy >= 6 || newx < 0 || newy < 0) {
				i = 3;
			} else {
				if (Board [newx, newy] == color) {
					counter++;
				} else {
					i = 3;
				}
			}
			if (counter >= 4) {
				Debug.Log ("DLD");
				return true;
			}
		}
			
		//diagonal right down
		newx = posx;
		newy = posy;
		counter = 1;
		for (int i = 0; i < 3; i++) {
			newx++;
			newy--;
			if (newx >= 7 || newy >= 6 || newx < 0 || newy < 0) {
				i = 3;
			} else {
				if (Board [newx, newy] == color) {
					counter++;
				} else {
					i = 3;
				}
			}
			if (counter >= 4) {
				Debug.Log ("DRD");
				return true;
			}
		}

		//diagonal left up
		newx = posx;
		newy = posy;
		for (int i = 0; i < 3; i++) {
			newx--;
			newy++;
			if (newx >= 7 || newy >= 6 || newx < 0 || newy < 0) {
				i = 3;
			} else {
				if (Board [newx, newy] == color) {
					counter++;
				} else {
					i = 3;
				}
			}
			if (counter >= 4) {
				Debug.Log ("DLU");
				return true;

			}
		}


		return false;

	}
}