using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel2 : MonoBehaviour {

	public static StatsPanel2 panel;

	[SerializeField]
	private Text fastestTimeText;
	[SerializeField]
	private Dropdown inspectionDD;
	private double fastestTime = 1000;

	// Use this for initialization
	void Awake () {
		panel = this;
		fastestTimeText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetCountDownValue() {
		if (inspectionDD.value == 0) {
			return 0;
		}
		if (inspectionDD.value == 1) {
			return 5;
		}
		if (inspectionDD.value == 2) {
			return 10;
		}
		if (inspectionDD.value == 3) {
			return 15;
		} else {
			Debug.Log ("Something Went Wrong");
			return 1000;
		}
	}

	public void CheckFastestTime(double timeToCheck) {

		string newTime = timeToCheck.ToString ();

		if (newTime.Length == 3) {
			newTime += "0";
		} else if (timeToCheck >= 10 && newTime.Length == 4) {
			newTime += "0";
		}

		if (timeToCheck < fastestTime) {
			fastestTime = timeToCheck;
			fastestTimeText.text = newTime;
		}

	}

	public string GetFastestTime() {
		string text = " " + fastestTimeText.text;
		return text;
	}

	public void SetFastestTime(double timeToSet) {
		fastestTime = timeToSet;
		string newTime = timeToSet.ToString ();

		if (newTime.Length == 3) {
			newTime += "0";
		} else if (timeToSet >= 10 && newTime.Length == 4) {
			newTime += "0";
		}
		fastestTimeText.text = newTime;
	}

	public void ResetFastestTime() {

		string timeToRemove = fastestTimeText.text;

		TimeLogger.instance.FindTimeToRemove (timeToRemove);

		double nextTime = TimeLogger.instance.FindFastestTime ();
		string newTime = nextTime.ToString ();

		if (newTime.Length == 3) {
			newTime += "0";
		} else if (nextTime >= 10 && newTime.Length == 4) {
			newTime += "0";
		}

		if (nextTime == 1000) {
			fastestTimeText.text = "";
		} else {
			fastestTimeText.text = newTime;
		}
		fastestTime = TimeLogger.instance.FindFastestTime();
	}

} //StatsPanel2
