using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIButtonLevelLoad : MonoBehaviour {
	
	public string LevelToLoad;
	
	public void loadLevel() {
		//mevcut seviyeyi LevelToLoad dan çek
		SceneManager.LoadScene(LevelToLoad);
	}
}
