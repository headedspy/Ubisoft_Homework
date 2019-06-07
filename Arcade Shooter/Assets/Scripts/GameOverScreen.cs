using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

	public GameObject selector;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerShip");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerController> ().selectedButton == 1) {
			selector.transform.localPosition = new Vector3 (20.5f, 0, 5.5f);
			if(Input.GetKeyDown(KeyCode.Space))SceneManager.LoadScene("Main");
		} else {
			selector.transform.localPosition = new Vector3 (20.5f, 0, 4.2f);
			if(Input.GetKeyDown(KeyCode.Space))SceneManager.LoadScene("Menu");
		}
	}
}
