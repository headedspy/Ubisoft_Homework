using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float projectileVelocity = 15f;
    public float timeToLive = 3f;
    public string tagToDamage = "Enemy";
	public GameObject exp;
	
	void Start () {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * projectileVelocity;
        Destroy(gameObject, timeToLive);
	}
    void OnCollisionEnter(Collision collision)
    {
		GameObject pew = Instantiate (exp, collision.contacts [0].point, Quaternion.Euler(90, 0, 0));
		Destroy (pew, 0.2f);
        Destroy(gameObject);
    }

}
