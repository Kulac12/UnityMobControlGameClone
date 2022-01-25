using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class Chaser : MonoBehaviour {
	
	public float speed = 20.0f;
	public float minDist = 1f;
	public Transform target;

	
	void Start () 
	{
		// özel bir hedef yoksa Bizim bölge (player) hedef olarak seçilsin
		if (target == null) {

			if (GameObject.FindWithTag ("DostKule")!=null)
			{
				target = GameObject.FindWithTag ("DostKule").GetComponent<Transform>();
			}
		}
	}
	
	
	void Update () 
	{
		if (target == null)
			return;

		// hedefe bak-odaklan
		transform.LookAt(target);

		//kovalayan ve hedef arasındaki mesafeyi al
		float distance = Vector3.Distance(transform.position,target.position);

		//kovalayan minimum mesafeden daha uzakta ise, ona doğru belirtilen hızda ilerleme kodu
		if(distance > minDist)	
			transform.position += transform.forward * speed * Time.deltaTime;	
	}

	// kovalayanın hedefi
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

}
