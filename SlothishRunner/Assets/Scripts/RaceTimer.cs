using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class RaceTimer : MonoBehaviour {

	//public static bool onRace;
	public TextMesh[] TimeDisplay;
	//public TextMesh BestDisplay;
	public GameObject[] Players;
	public Transform[] Respawns;
	float TotalTime;
	public static bool running;
	private int winner;
	public GameObject StartWall;
	//float bestTime;
	//string path;
	// Use this for initialization
	void Start () {
		winner = -1;
		//onRace = false;
		/*path = "Highscore.txt";
		if (!File.Exists (path)) {
			File.WriteAllText(path, "50");
		} else {
			string pbParser = File.ReadAllText (path);
			Debug.Log (pbParser);
			bestTime = float.Parse(pbParser);
		}*/
		running = false;
		TotalTime = 0;
		for (int i = 0; i < TimeDisplay.Length; i++) {
			StartCoroutine (StartRace (i));
		}
		//BestDisplay.text = "PB: " + bestTime.ToString("#.##");
	}

	IEnumerator StartRace(int i)
	{

		TimeDisplay[i].text = "Time To Start: 3";
		yield return new WaitForSeconds (1f);
		TimeDisplay[i].text = "Time To Start: 2";
		yield return new WaitForSeconds (1f);
		TimeDisplay[i].text = "Time To Start: 1";
		yield return new WaitForSeconds (1f);
		TimeDisplay[i].text = "RUN!";
		yield return new WaitForSeconds (1f);
		StartWall.SetActive (false);
		running = true;


	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Players.Length; i++) {
			if(Input.GetButtonDown ("Respawn" + (i+1)))
			{
				Players[i].transform.position = Respawns[i].position;
			}
		}

		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel("RaceCircuitPrototype");
		}

		/*if (Input.GetKeyDown (KeyCode.Delete)) {
			File.WriteAllText(path, "50");
		}*/

		if (Input.GetKeyDown (KeyCode.Return)) {
			if(Application.loadedLevel == 0)
				Application.LoadLevel("Playground");
			else if(Application.loadedLevel == 1)
				Application.LoadLevel("RaceCircuitPrototype");
		}
		if (running) {
			TotalTime += Time.deltaTime;
		}
		for (int i = 0; i < TimeDisplay.Length; i++) {
			if(running)
			{
				TimeDisplay[i].text = "Time: " + TotalTime.ToString ("#.##");
			}else if(winner > 0){
				TimeDisplay[i].text = "Player " + winner.ToString() + " wins: " + TotalTime.ToString ("#.##");
			}
		}
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Player1") {
			running = false;
			winner = 1;
			Debug.Log ("End");
		}

		if (hit.gameObject.tag == "Player2") {
			running = false;
			winner = 2;
			Debug.Log("End");
			/*Debug.Log("Final");
			if(TotalTime < bestTime)
			{
				File.WriteAllText(path, TotalTime.ToString("#.##"));
				BestDisplay.text = TotalTime.ToString ("#.##") + " NEW PB!";
			}*/
		}
	}
}
