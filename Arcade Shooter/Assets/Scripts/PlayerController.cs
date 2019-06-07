using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 0.2f;
    public float rotationSpeed = 180f;
    public Vector3 halfExtents = new Vector3(10, 0, 5.2f);
	public GameObject projectile;
	public AudioClip shoot;
	public AudioClip expl;
	private bool isDead = false;

	public int selectedButton = 1;

	public GameObject button;
	public GameObject joystick;
	public GameObject gameOverScreen;

    private void Start()
    {
        AsteroidsManager.Instance.RegisterPlayer(gameObject);
    }
    

	void Update () {
		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");

		if (Input.GetKeyDown(KeyCode.W) ){
			joystick.transform.RotateAround (joystick.transform.position, Vector3.left, 20f);
			if(isDead)selectedButton = 1;
		}
		if (Input.GetKeyUp (KeyCode.W)) {
			joystick.transform.RotateAround (joystick.transform.position, Vector3.left, -20f);
		}
		if (Input.GetKeyDown(KeyCode.A) ){
			joystick.transform.RotateAround (joystick.transform.position, Vector3.forward, -20f);
		}
		if (Input.GetKeyUp (KeyCode.A)) {
			joystick.transform.RotateAround (joystick.transform.position, Vector3.forward, 20f);
		}
		if (Input.GetKeyDown(KeyCode.S) ){
			joystick.transform.RotateAround (joystick.transform.position, Vector3.left, -20f);
			if(isDead)selectedButton = 2;
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			joystick.transform.RotateAround (joystick.transform.position, Vector3.left, 20f);
		}
		if (Input.GetKeyDown(KeyCode.D) ){
			joystick.transform.RotateAround (joystick.transform.position, Vector3.forward, 20f);
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			joystick.transform.RotateAround (joystick.transform.position, Vector3.forward, -20f);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			button.transform.position = button.transform.position - new Vector3 (0, 0.05f, 0);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			button.transform.position = button.transform.position + new Vector3 (0, 0.05f, 0);
		}

		if (!isDead) {
			Quaternion offset = Quaternion.Euler (0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
			transform.rotation = transform.rotation * offset;
			Vector3 displacement = new Vector3 (0f, 0f, verticalInput) * speed * Time.deltaTime;
			displacement = transform.rotation * displacement;
			Vector3 newPosition = transform.position + displacement;
			for (int i = 0; i < 3; ++i) {
				newPosition [i] = Mathf.Clamp (newPosition [i], -halfExtents [i], halfExtents [i]);
			}
			transform.position = newPosition;



			if (Input.GetKeyUp (KeyCode.Space)) {
				GetComponent<AudioSource> ().PlayOneShot (shoot, 0.1f);
				GameObject proj = Instantiate (projectile, transform.position, transform.rotation);
				proj.GetComponent<Rigidbody> ().velocity = proj.transform.forward * 20;
				Destroy (proj, 2);
			}

			if (verticalInput > 0) {
				gameObject.transform.GetChild (2).gameObject.SetActive (true);
				gameObject.transform.GetChild (3).gameObject.SetActive (true);
			} else {
				gameObject.transform.GetChild (2).gameObject.SetActive (false);
				gameObject.transform.GetChild (3).gameObject.SetActive (false);
			}
		}
    }

	void OnTriggerEnter(Collider col){
		if ((col.gameObject.tag == "Asteroid" || col.gameObject.tag == "EnemyProjectile") && !isDead) {
			gameObject.GetComponent<HealthPoints>().TakeDamage (10);
			if (col.gameObject.tag == "Asteroid") {
				col.gameObject.GetComponent<HealthPoints> ().TakeDamage (100);
				col.gameObject.GetComponent<AsteroidController> ().SetSmall ();
			}else
				Destroy (col.gameObject);
			if (gameObject.GetComponent<HealthPoints> ().isDead ()) {
				isDead = true;
				AsteroidsManager.Instance.UnregisterPlayer (gameObject);
				Destroy (col.gameObject);
				gameObject.tag = "Dead";
				GetComponent<Renderer> ().enabled = false;
				GetComponent<AudioSource> ().PlayOneShot (expl, 0.4f);
				GetComponent<ParticleSystem> ().Play ();
				gameObject.transform.GetChild (2).gameObject.SetActive (false);
				gameObject.transform.GetChild (3).gameObject.SetActive (false);
				//Destroy (gameObject, 2);
				GetComponent<Renderer>().enabled = false;
				Camera.main.GetComponent<AudioSource> ().Stop ();

				gameOverScreen.SetActive (true);
				gameOverScreen.transform.GetChild (0).GetComponent<TextMesh>().text = AsteroidsManager.Instance.score.ToString ();
			}
		}
	}
}
