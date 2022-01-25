using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//d�s�man askerleri player tagine sahip �eylere hasar vermesi i�in -- kendi kendilerini �ld�rmesinler diye.
public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 10.0f;

    public bool damageOnTrigger = true;
    public bool damageOnCollision = false;
    public bool continuousDamage = false;
    public float continuousTimeBetweenHits = 0;
    public float continuousDamageTime = 0;
    public bool destroySelfOnImpact = false;    
    public float delayBeforeDestroy = 0.0f;
    public GameObject explosionPrefab;

    private float savedTime = 0;



	void OnTriggerEnter(Collider collision)         // tetikleyici olan mermiler gibi �eyler i�in
	{
		if (damageOnTrigger)
		{
			if (this.tag == "EnemyBullet" && collision.gameObject.tag == "EnemyKule") // D��man kendi mermileri ile kendini vurduysa bu durumu yoksaymak i�in
				return;

			if (collision.gameObject.GetComponent<Health>() != null)
			{   // vurulan gameObjectin �st�nde "Health" scripti varsa
				collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);

				if (destroySelfOnImpact)
				{
					Destroy(gameObject, delayBeforeDestroy);      // bir �eye �arpt���nda nesneyi ortadan kald�r.
				}

				if (explosionPrefab != null)
				{
					Instantiate(explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionEnter(Collision collision)
	//Bu �arp��ma an�n�nda patlayan �eylerin tetiklenmemsi i�in kullan�l�r.
	{
		if (damageOnCollision)
		{
			if (this.tag == "EnemyBullet" && collision.gameObject.tag == "EnemyKule") 
				return;

			if (collision.gameObject.GetComponent<Health>() != null)
			{   
				collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);

				if (destroySelfOnImpact)
				{
					Destroy(gameObject, delayBeforeDestroy);      
				}

				if (explosionPrefab != null)
				{
					Instantiate(explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionStay(Collision collision) // s�re� i�inde hasar vermek i�in kullan�l�r (zehirlenme gibi) 
	{
		if (continuousDamage)
		{
			if (collision.gameObject.tag == "EnemyKule" && collision.gameObject.GetComponent<Health>() != null)
			{   // vuruldu�u �ey sadece "EnemyKule" ise tetiklenir.
				if (Time.time - savedTime >= continuousTimeBetweenHits)
				{
					savedTime = Time.time;
					collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);
				}
			}
		}
	}

}
