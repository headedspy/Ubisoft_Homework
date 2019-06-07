using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScroll : MonoBehaviour {

	public float speed;
	private Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();
	}

	void Update () {
		rend.material.SetTextureOffset ("_MainTex", new Vector2 (0, Time.time * -1 * speed));
	}
}
