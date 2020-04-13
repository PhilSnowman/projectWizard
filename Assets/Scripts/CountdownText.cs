using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownText : MonoBehaviour {

	public static CountdownText Instance;

	[SerializeField] TMP_Text text;

	float nextTimeToCount;

	bool counting;

	int secondsLeft;

	public delegate void Callback();
	Callback callback;

	void Awake() {
		Instance = this;
	}

	public void StartCounting(int time, Callback _callback) {
		nextTimeToCount = 0;
		callback = _callback;
		secondsLeft = time;
		counting = true;
	}

	public void StopCounting() {
		text.text = "";
		counting = false;
	}

	void Update() {
		if(!counting)
			return;
		if(Time.time >= nextTimeToCount) {
			if(secondsLeft <= -1) {
				callback.Invoke();
				StopCounting();
				return;
			}
			CountSecond();
			secondsLeft--;
		}
	}

	void CountSecond() {
		text.text = secondsLeft.ToString();
		nextTimeToCount = Time.time + 1f;
	}
}