using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager gm;

	[Tooltip("Eger ayarlanmazsa varsayilan olarak tagi Player olan gameObject gececek")]
	public GameObject player;

	public enum gameStates {Playing, Death, GameOver, BeatLevel};
	public gameStates gameState = gameStates.Playing;

	public int score=0;
	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public GameObject mainCanvas;
	public Text mainScoreDisplay;
	public GameObject gameOverCanvas;
	public Text gameOverScoreDisplay;

	[Tooltip("sadece canBeatlevel true olarak ayarlanmis ise bu canvasa ihtiyac duyulur")]
	public GameObject beatLevelCanvas;

	public AudioSource backgroundMusic;
	public AudioClip gameOverSFX;

	[Tooltip("SAdece canBeatLevel true olarak ayarlanmıssa ihtiyac duyulur")]
	public AudioClip beatLevelSFX;

	private Health playerHealth;

	void Start () {
		if (gm == null) 
			gm = gameObject.GetComponent<GameManager>();
		

		if (player == null) {
			player = GameObject.FindWithTag("DostKule");
		}

		playerHealth = player.GetComponent<Health>();

		// setup puanı ekranı
		Collect (0);

		// diğer kullanci arayuzlerini devre disi birak
		gameOverCanvas.SetActive (false);
		if (canBeatLevel)
			beatLevelCanvas.SetActive (false);
	}

	void Update () {
		switch (gameState)
		{
			case gameStates.Playing:
				if (playerHealth.isAlive == false)
				{
					// güncelle gameState
					gameState = gameStates.Death;

					// oyun sonu scorunu ayarla
					gameOverScoreDisplay.text = mainScoreDisplay.text;

					// gui ile yapilan geçis	
					mainCanvas.SetActive (false);
					gameOverCanvas.SetActive (true);
				} else if (canBeatLevel && score>=beatLevelScore) {
					// güncelle gameState
					gameState = gameStates.BeatLevel;

					// oyuncuyu gizeriz boylece oun devam edemez
					player.SetActive(false);

							
					mainCanvas.SetActive (false);
					beatLevelCanvas.SetActive (true);
				}
				break;
			case gameStates.Death:
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume<=0.0f) {
					AudioSource.PlayClipAtPoint (gameOverSFX,gameObject.transform.position);

					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.BeatLevel:
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume<=0.0f) {
					AudioSource.PlayClipAtPoint (beatLevelSFX,gameObject.transform.position);
					
					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.GameOver:
				
				break;
		}

	}


	public void Collect(int amount) {
		score += amount;
		if (canBeatLevel) {
			mainScoreDisplay.text = score.ToString () + " of "+beatLevelScore.ToString ();
		} else {
			mainScoreDisplay.text = score.ToString ();
		}

	}
}
