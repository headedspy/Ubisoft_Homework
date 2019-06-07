using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    public float appliedForce = 20f;
	public GameObject proj;
	public AudioClip shoot;
	public AudioClip explode;
	private bool isHit = false;
	public bool isSmall = false;

    void Start()
    {
        //The Asteroid Manager will handle all movement of the asteroids
        //AsteroidsManager.Instance.RegisterAsteroid(gameObject);

        RadomizeDirection(transform.position);
    }

	public void SetSmall(){
		isSmall = true;
	}

    private void OnDestroy()
    {
        AsteroidsManager.Instance.UnregisterAsteroid(gameObject);
    }

    public void RadomizeDirection(Vector3 newPosition)
	{
        //newPosition.y = 0;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

        Vector3 newDirection = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
        newDirection.Normalize();

        //Caching the Rigid Body to avoid the slowness of getting it every time
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(newDirection * appliedForce);
	}

	void Update(){
		if (!isHit) {
			transform.RotateAround (transform.position, transform.up, Time.deltaTime * 90f);
			if (Random.Range (1, 151) == 1) {
				GetComponent<AudioSource> ().PlayOneShot (shoot, 0.2f);
				GameObject projectile = Instantiate (proj, transform.position, transform.rotation);
				projectile.GetComponent<Rigidbody> ().velocity = projectile.transform.forward * 20;
				Destroy (projectile, 2);
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player") && !isHit) {
			if(col.gameObject.tag == "Enemy")Destroy (col.gameObject);
			gameObject.GetComponent<HealthPoints> ().TakeDamage (10);
			if (gameObject.GetComponent<HealthPoints> ().isDead ()) {
				isHit = true;
				Rigidbody rb = GetComponent<Rigidbody> ();
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				GetComponent<AudioSource> ().PlayOneShot (explode, 0.4f);
				GetComponent<Renderer> ().enabled = false;
				GetComponent<ParticleSystem> ().Play ();

				if (!isSmall) {
					GameObject.Find ("AsteroidsManager").GetComponent<AsteroidsManager> ().SpawnNewAsteroids (true, gameObject.transform.position);
					if(col.gameObject.tag == "Enemy")AsteroidsManager.Instance.score += 1;
				} else {
					if(col.gameObject.tag == "Enemy")AsteroidsManager.Instance.score += 2;
				}

				Destroy (gameObject, 1);
			}
		}
	}
}
