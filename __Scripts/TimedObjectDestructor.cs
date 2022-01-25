using UnityEngine;
using System.Collections;

public class TimedObjectDestructor : MonoBehaviour {

	public float timeOut = 1.0f;
	public bool detachChildren = false;

	// Use this for initialization
	void Awake () {
		// nesenyi ortadan kaldirma islemini timeOut saniyesinde sonra deam ettir
		Invoke ("DestroyNow", timeOut);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void DestroyNow ()
	{
		if (detachChildren) { // varsayalım ki oyun nesnemizin cocuklari var
			transform.DetachChildren ();
		}
		DestroyObject (gameObject);
	}
}
