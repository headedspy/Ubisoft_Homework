using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

	public bool isMachineGun = false;
	public bool isHealth = false;
	public bool isShield = false;

	public AudioClip powerUpSound;

	void Awake(){
		Destroy (gameObject, 7);
	}

	void OnDestroy(){
		AsteroidsManager.Instance.SetBonusFalse();
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "PlayerShip") {
			if (isMachineGun) {
				col.gameObject.GetComponent<Weapon> ().ActivateMachineGun ();
			} else if (isHealth) {
				col.gameObject.GetComponent<HealthService> ().DealDamage (-50);
			}
			else {
				col.gameObject.GetComponent<PlayerController> ().ActivateShield ();
			}
			col.gameObject.GetComponent<AudioSource> ().PlayOneShot (powerUpSound);
			Destroy (gameObject);
		}
	}
}
