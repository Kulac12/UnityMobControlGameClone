using UnityEngine;
using System.Collections;

public class EnemyHealt : MonoBehaviour
{

	public enum deathAction { loadLevelWhenDead, doNothingWhenDead };

	public float healthPoints = 1f;
	public float respawnHealthPoints = 1f;      //temel sa�l�k de�eri

	public int numberOfLives = 1;                   //yeniden canlanma i�in de�i�ken
	public bool isAlive = true;   //ya��yor mu �l� m�? sorgusu i�in

	public GameObject explosionPrefab;

	public deathAction onLivesGone = deathAction.doNothingWhenDead;

	public string LevelToLoad = "";

	private Vector3 respawnPosition;
	private Quaternion respawnRotation;


	
	void Start()
	{
		// ba�lang�� konumu yeniden do�ma konumu i�in sakl�ypruz
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;

		if (LevelToLoad == "") // ge�erli sahneyi / varsay�lan� al 
		{
			LevelToLoad = Application.loadedLevelName;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (healthPoints <= 0)
		{               // diyelim ki �ld�k
			numberOfLives--;                    // azalt # dirilme hakk�m�z�, git Lives Gui ekran�n� g�ncelle

			if (explosionPrefab != null)
			{
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			}

			if (numberOfLives > 0)
			{ // yeniden canlanma
				transform.position = respawnPosition;   // konumumjzu s�f�rla
				transform.rotation = respawnRotation;
				healthPoints = respawnHealthPoints; // sa�l���(health) fullle
			}
			else
			{ //canlanma hakk�m�z kalmad�ysa
				isAlive = false;

				switch (onLivesGone)
				{
					case deathAction.loadLevelWhenDead:
						Application.LoadLevel(LevelToLoad);
						break;
					case deathAction.doNothingWhenDead:
						//hi�bir �ey yapma
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
