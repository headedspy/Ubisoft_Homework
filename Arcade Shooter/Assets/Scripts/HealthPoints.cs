using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour {

	private GameObject healthBar;

	private GameObject redBackground;

	private Color red = Color.red;
	private Color green = Color.green;

	private int health = 100;

	// Use this for initialization
	void Start () {

		red.a = 0.2f;
		green.a = 0.5f;

		healthBar = GameObject.CreatePrimitive (PrimitiveType.Cube);
		healthBar.GetComponent<BoxCollider> ().isTrigger = true;
		healthBar.transform.position = transform.position + new Vector3(0,0,1);
		healthBar.transform.rotation = Quaternion.Euler(0,0,0);
		healthBar.transform.localScale = new Vector3 (1.5f, 0.1f, 0.2f);
		healthBar.GetComponent<Renderer> ().material.color = green;
		healthBar.GetComponent<Renderer> ().material.shader = Shader.Find("GUI/Text Shader");

		redBackground = GameObject.CreatePrimitive (PrimitiveType.Cube);
		redBackground.GetComponent<BoxCollider> ().isTrigger = true;
		redBackground.transform.position = transform.position + new Vector3(0,-0.5f,1);
		redBackground.transform.rotation = Quaternion.Euler(0,0,0);
		redBackground.transform.localScale = new Vector3 (1.5f, 0.1f, 0.2f);
		redBackground.GetComponent<Renderer> ().material.color = red;
		redBackground.GetComponent<Renderer> ().material.shader = Shader.Find("GUI/Text Shader");

	}
	
	// Update is called once per frame
	void Update () {
		if (health > 0) {
			healthBar.transform.rotation = Quaternion.Euler (0, 0, 0);
			healthBar.transform.position = transform.position + new Vector3 (0, 0, 1);
			redBackground.transform.rotation = Quaternion.Euler (0, 0, 0);
			redBackground.transform.position = transform.position + new Vector3 (0, -0.1f, 1);
		}
	}

	public void TakeDamage(int damageDealt){
		if (health > 0) {
			healthBar.transform.localScale = new Vector3 (1.5f * ((float)health / 100f), 0.1f, 0.2f);
			health -= damageDealt;
			if (health <= 0) {
				Destroy (healthBar);
				Destroy (redBackground);
			}
		}
	}

	public bool isDead(){
		return health <= 0;
	}
}
