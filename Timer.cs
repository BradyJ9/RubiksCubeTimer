using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public static Timer instance;

	private string[] scrambleMoves = {"R", "R'", "R2", "L", "L'", "L2", "U", "U'", "U2", "D", "D'", "D2",
		"F", "F'", "F2", "B", "B'", "B2"
	};
	private double time = 0.0f;
	private bool timerGoing = false;
	private bool inspectionOver = true;
	private bool spaceDelayOff = true;
	private double timeBetweenPresses;
	private int inspectionSecs = 0;
	public Text timerText;
	public Text scrambleText;

	// Use this for initialization
	void Awake () {
		instance = this;
		timerText.text = "0.0";
		GetScramble ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) { //spacebar starts/stops timer
			if (spaceDelayOff) {
				if (!timerGoing) { //reset timer if it already has a number on it
					InspectionCountdown (StatsPanel2.panel.GetCountDownValue ());
					time = 0.0f;
				} else { //timer stops
					spaceDelayOff = false;
					Invoke ("SpaceDelay", 1f);
					if (inspectionOver) {
						TimeLogger.instance.LogATime (timerText.text);
						TimeLogger.instance.RecordTime (time);
						StatsPanel2.panel.CheckFastestTime (time);
						GetScramble ();
					} else {
						inspectionSecs = 0;
						StopCoroutine (OneSecondPause ());
						GetScramble ();
					}
				}

				timerGoing = !timerGoing;
			}
		}

		if (timerGoing && inspectionOver) {
			CountUpTimer ();
		}
	}

	void SpaceDelay() {
		spaceDelayOff = true;
	}

	void InspectionCountdown(int secondsForInspection) {
		inspectionOver = false;
		inspectionSecs = secondsForInspection;
		StartCoroutine (OneSecondPause ());
	}

	IEnumerator OneSecondPause() {
		if (inspectionSecs != 0) {
			timerText.text = inspectionSecs.ToString ();
			--inspectionSecs;
			yield return new WaitForSeconds (1f);
			StartCoroutine (OneSecondPause ());
		} else {
			yield return null;
			inspectionOver = true;
			InitializeTimeBetweenPresses ();
		}

	}

	void InitializeTimeBetweenPresses () {
		timeBetweenPresses = Time.time;
	}


	void CountUpTimer() {
		time = Time.time - timeBetweenPresses;

		time = System.Math.Round (time, 2);
		string newTime = time.ToString ();
		if (newTime.Length == 3) {
			newTime += "0";
		} else if (time >= 10 && newTime.Length == 4) {
			newTime += "0";
		}
		timerText.text = newTime;
	}

	void GetScramble() {
		string scramble = ScrambleGenerator ();
		scrambleText.text = scramble;

	}

	string ScrambleGenerator() {
		string scramb = "";
		string prevMove = "";
		string nextMove = "";

		for (int i = 0; i < 25; ++i) {
		
			if (i != 0) {
				prevMove = nextMove;
				do {
					nextMove = scrambleMoves [Random.Range (0, scrambleMoves.Length - 1)];

				} while (!NextMoveIsValid (prevMove, nextMove));

				scramb += nextMove + " ";
			}
		} 
		return scramb;
	}

	bool NextMoveIsValid(string prevMove, string potentialMove) {
		if (potentialMove == "R" || potentialMove == "R'" || potentialMove == "R2") {
			if (prevMove == "R" || prevMove == "R'" || prevMove == "R2") {
				return false;
			} else { 
				return true;
			}
		} else if (potentialMove == "L" || potentialMove == "L'" || potentialMove == "L2") {
			if (prevMove == "L" || prevMove == "L'" || prevMove == "L2") {
				return false;
			} else {
				return true;
			}
		} else if (potentialMove == "U" || potentialMove == "U'" || potentialMove == "U2") {
			if (prevMove == "U" || prevMove == "U'" || prevMove == "U2") {
				return false;
			} else {
				return true;
			}
		} else if (potentialMove == "D" || potentialMove == "D'" || potentialMove == "D2") {
			if (prevMove == "D" || prevMove == "D'" || prevMove == "D2") {
				return false;
			} else {
				return true;
			}
		} else if (potentialMove == "B" || potentialMove == "B'" || potentialMove == "B2") {
			if (prevMove == "B" || prevMove == "B'" || prevMove == "B2") {
				return false;
			} else {
				return true;
			}
		} else if (potentialMove == "F" || potentialMove == "F'" || potentialMove == "F2") {
			if (potentialMove == "F" || potentialMove == "F'" || potentialMove == "F2") {
				return false;
			} else {
				return true;
			}
		} else {
			Debug.Log ("Something went wrong");
			return false;
		}
	}


}  //Timer
