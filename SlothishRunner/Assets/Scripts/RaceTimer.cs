using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class RaceTimer : MonoBehaviour {

	public TextMesh TimeDisplay;
	public TextMesh BestDisplay;
	float TotalTime;
	bool running;
	float bestTime;
	string path;
	// Use this for initialization
	void Start () {
		path = "Highscore.txt";
		if (!File.Exists (path)) {
			File.WriteAllText(path, "50");
		} else {
			string pbParser = File.ReadAllText (path);
			Debug.Log (pbParser);
			bestTime = float.Parse(pbParser);
		}
		running = false;
		TotalTime = 0;
		BestDisplay.text = "PB: " + bestTime.ToString("#.##");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel("RaceCircuitPrototype");
		}

		if (Input.GetKeyDown (KeyCode.Delete)) {
			File.WriteAllText(path, "50");
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			if(Application.loadedLevel == 0)
				Application.LoadLevel("Playground");
			else if(Application.loadedLevel == 1)
				Application.LoadLevel("RaceCircuitPrototype");
		}
		if (running) {
			TotalTime += Time.deltaTime;
		}
		TimeDisplay.text = "Time: " + TotalTime.ToString("#.##");
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
			if(TotalTime < bestTime)
			{
				File.WriteAllText(path, TotalTime.ToString("#.##"));
				BestDisplay.text = TotalTime.ToString ("#.##") + " NEW PB!";
			}
		}
	}
}
