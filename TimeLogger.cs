using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLogger : MonoBehaviour {

	private double[] times = {0.0, 0.0, 0.0, 0.0, 0.0,};

	public static TimeLogger instance;
	private int numOfTimes = 0;

	[SerializeField]
	private Text[] timeLogs;

	[SerializeField]
	private Text averageOfFive;

	private int numEntries = 0;

	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LogATime(string timeToLog) {
		if (numEntries != 5)
			++numEntries;

		timeLogs [numEntries - 1].gameObject.SetActive (true);

		if (numEntries > 1) {
			for (int i = numEntries - 1; i > 0; i--) {
				timeLogs[i].text = timeLogs [i - 1].text;
			}
		}

		timeLogs [0].text = " " + timeToLog;

	}

	public void RecordTime(double time) {
		if (numOfTimes != 5)
			++numOfTimes;

		if (times.Length > 1) {
			for (int i = times.Length - 1; i > 0; --i) {
				times [i] = times [i - 1];
			}
			times [0] = time;
		}
		if (times.Length == 5) {
			double sum = 0;
			for (int i = 0; i < times.Length; ++i) {
				sum += times [i];
			}

			if (numOfTimes == 5)
				CalculateAverageOfFive (sum);
		}
	}

	public double FindFastestTime() {
		double nextFastest = 1000;
		if (numOfTimes == 0) {
			return 1000;
		}
		for (int i = 0; i < numOfTimes; ++i) {
			if (times [i] < nextFastest) {
				nextFastest = times [i];
			}
		}
		return nextFastest;
	}

	public void FindTimeToRemove(string timeToRemove) {
		int indexToRemove = 1000;
		string timeToCompare = " " + timeToRemove;

		if (numEntries != 0) {
			for (int i = 0; i < times.Length; ++i) {
				if (timeToCompare == timeLogs [i].text) {
					indexToRemove = i;
				}
			}
		}

		if (indexToRemove == 0) {
			XOut1 ();
		}
		if (indexToRemove == 1) {
			XOut2 ();
		}
		if (indexToRemove == 2) {
			XOut3 ();
		}
		if (indexToRemove == 3) {
			XOut4 ();
		}
		if (indexToRemove == 4) {
			XOut5 ();
		}

	}

	public void CalculateAverageOfFive(double sum) {

		double result = System.Math.Round (sum / numEntries, 2);

		averageOfFive.text = "5 Average: " + result.ToString();

	}

	public void XOut1() {
		XOutRemoveTimes (1);
	}

	public void XOut2() {
		XOutRemoveTimes (2);

	}

	public void XOut3() {
		XOutRemoveTimes (3);

	}

	public void XOut4() {
		XOutRemoveTimes (4);

	}

	public void XOut5() {
		XOutRemoveTimes (5);
	}

	public void XOutRemoveTimes(int numberXOut) {
		bool fastTimeFlag = false; //bool is set if fastest time is X'd out and needs to be replaced
		--numEntries;
		--numOfTimes;
		timeLogs [numEntries].gameObject.SetActive (false);

		if (timeLogs [numberXOut - 1].text == StatsPanel2.panel.GetFastestTime ()) {
			fastTimeFlag = true;
		}

		for (int i = numberXOut - 1; i < numEntries; ++i) {
			timeLogs [i].text = timeLogs [i + 1].text;
			times [i] = times [i + 1];
		}

		if (fastTimeFlag) {
			StatsPanel2.panel.SetFastestTime (FindFastestTime ());
		}


		averageOfFive.text = "5 Average: ";

	}


} //TimeLogger
