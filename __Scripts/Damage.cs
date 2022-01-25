using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	// Player oyuncusunun hasar verme kodu

	public float damageAmount = 10.0f;
	
	public bool damageOnTrigger = true;
	public bool damageOnCollision = false;
	public bool continuousDamage = false;
	public float continuousTimeBetweenHits = 0;

	public bool destroySelfOnImpact = false;	// patlama ile ilgili değişken
	public float delayBeforeDestroy = 0.0f;
	public GameObject explosionPrefab;

	private float savedTime = 0;

	void OnTriggerEnter(Collider collision)						// tetikleyici olan mermiler gibi şeyler için
	{
		if (damageOnTrigger) {
			if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")	// Player kendi merileri ile vurulduysa yoksaymak için
				return;
		
			if (collision.gameObject.GetComponent<EnemyHealt> () != null) {	// vurulan gameObject in üstünde "EnemyHealt" scripti varsa hasar ver
				collision.gameObject.GetComponent<EnemyHealt> ().ApplyDamage (damageAmount);
		
				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  // bir şeye çarptığında nesneyi ortadan kaldır.
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionEnter(Collision collision) 						
		//Bu çarpışma anınında patlayan şeylerin tetiklenmemsi için kullanılır.
	{	
		if (damageOnCollision) {
			if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")	
				return;
		
			if (collision.gameObject.GetComponent<EnemyHealt> () != null) {	
				collision.gameObject.GetComponent<EnemyHealt> ().ApplyDamage (damageAmount);
			
				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionStay(Collision collision) // süreç içinde hasar vermek için kullanılır (zehirlenme gibi) 
	{
		if (continuousDamage) {
			if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<EnemyHealt> () != null) {	// vurulduğu şey Player ise tetklenir
				if (Time.time - savedTime >= continuousTimeBetweenHits) {
					savedTime = Time.time;
					collision.gameObject.GetComponent<EnemyHealt> ().ApplyDamage (damageAmount);
				}
			}
		}
	}
	
}