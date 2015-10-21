using UnityEngine;
using System.Collections;

public class RaceTimer : MonoBehaviour {

	public TextMesh TimeDisplay;
	float TotalTime;
	bool running;

	// Use this for initialization
	void Start () {
		running = false;
		TotalTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (running) {
			TotalTime += Time.deltaTime;
		}
		TimeDisplay.text = TotalTime.ToString("#.##");
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.name == "Start") {
			running = true;
			Debug.Log ("Start");
		}

		if (hit.gameObject.name == "Finish") {
			running = false;
			Debug.Log("Final");
		}
	}
}
