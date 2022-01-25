using UnityEngine;
using System.Collections;

public class EnemyHealt : MonoBehaviour
{

	public enum deathAction { loadLevelWhenDead, doNothingWhenDead };

	public float healthPoints = 1f;
	public float respawnHealthPoints = 1f;      //temel saðlýk deðeri

	public int numberOfLives = 1;                   //yeniden canlanma için deðiþken
	public bool isAlive = true;   //yaþýyor mu ölü mü? sorgusu için

	public GameObject explosionPrefab;

	public deathAction onLivesGone = deathAction.doNothingWhenDead;

	public string LevelToLoad = "";

	private Vector3 respawnPosition;
	private Quaternion respawnRotation;


	
	void Start()
	{
		// baþlangýç konumu yeniden doðma konumu için saklýypruz
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;

		if (LevelToLoad == "") // geçerli sahneyi / varsayýlaný al 
		{
			LevelToLoad = Application.loadedLevelName;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (healthPoints <= 0)
		{               // diyelim ki öldük
			numberOfLives--;                    // azalt # dirilme hakkýmýzý, git Lives Gui ekranýný güncelle

			if (explosionPrefab != null)
			{
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			}

			if (numberOfLives > 0)
			{ // yeniden canlanma
				transform.position = respawnPosition;   // konumumjzu sýfýrla
				transform.rotation = respawnRotation;
				healthPoints = respawnHealthPoints; // saðlýðý(health) fullle
			}
			else
			{ //canlanma hakkýmýz kalmadýysa
				isAlive = false;

				switch (onLivesGone)
				{
					case deathAction.loadLevelWhenDead:
						Application.LoadLevel(LevelToLoad);
						break;
					case deathAction.doNothingWhenDead:
						//hiçbir þey yapma
						break;
				}
				Destroy(gameObject);
			}
		}
	}

	public void ApplyDamage(float amount)
	{

		healthPoints = healthPoints - amount;
	}

	public void ApplyHeal(float amount)
	{
		healthPoints = healthPoints + amount;
	}

	public void ApplyBonusLife(int amount)
	{
		numberOfLives = numberOfLives + amount;
	}

	public void updateRespawn(Vector3 newRespawnPosition, Quaternion newRespawnRotation)
	{
		respawnPosition = newRespawnPosition;
		respawnRotation = newRespawnRotation;
	}
}
