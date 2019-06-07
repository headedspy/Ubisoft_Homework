using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager Instance { get; private set; }
	public int points = 0;
	public GameObject textObject;

	private void Awake(){
		textObject = GameObject.Find ("Room/Laptop/TextScore");
		if (Instance == null){
			Instance = this;
		}
		else{
			Destroy(gameObject);
		}
	}

	public void AddPoints(int givenPoints){
		points += givenPoints;
		textObject.GetComponent<TextMesh> ().text = points.ToString ();
	}
}
