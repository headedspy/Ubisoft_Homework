using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthService : MonoBehaviour {

    public int health = 100;
    protected int currentHealth = 100;

    public uint splitChunksCount = 0;
    public GameObject chunkPrefab;
    public Vector3 spawnOffset;

	public GameObject healthText;

	private GameObject go;
    
    void Start () {
		currentHealth = health;
		go = GameObject.Find ("GameOver");
		go.SetActive (false);
	}

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDeath();
        }
		if (gameObject.tag == "Player") {
			if (currentHealth <= 0) {
				go.SetActive (true);
				go.GetComponent<GameOver> ().EndGame ();
			}
			healthText.GetComponent<TextMesh> ().text = currentHealth.ToString ();
		}
        
    }

    protected void OnDeath()
    {
        if (chunkPrefab != null && splitChunksCount > 0)
        {
			ScoreManager.Instance.AddPoints (10);
            float step = 360f / splitChunksCount;
            for (int i = 0; i < splitChunksCount; i++)
            {
                float angle = step * i;
                Vector3 spawnPosition = gameObject.transform.position;
                spawnPosition += Quaternion.Euler(0, angle, 0) * spawnOffset;
                Instantiate(chunkPrefab, spawnPosition, gameObject.transform.rotation);
            }
        }

		if (splitChunksCount == 0) {
			ScoreManager.Instance.AddPoints (5);
		}

        Destroy(gameObject);
    }

	public int CurrentHealth(){
		return currentHealth;
	}
    
}
