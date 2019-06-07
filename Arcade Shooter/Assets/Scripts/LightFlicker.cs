﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

	Light testLight;

	void Start () {
		testLight = GetComponent<Light>();
		StartCoroutine(Flashing());
	}

	IEnumerator Flashing ()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.001f);
			testLight.enabled = ! testLight.enabled;

		}
	}
}
