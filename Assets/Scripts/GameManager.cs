using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public Canvas mainCanvas;
	private GameObject[] bars; 
	private List<string> barritas = new List<string> ();

	public GameObject explosionPrefab;
	private GameObject player;

	public int playerHp = 29;
	public GameObject hurtedThing;
	private int maxHp;

	public static Vector2 defaultPosition;
	public static Vector3 defaultCameraPos;
	private Vector2 respawnPosition;
	public bool checkpoint1 = false;
	public bool checkpoint2 = false;
	public bool checkpoint3 = false;

	public GameObject[] zones;

	public AudioClip victoryMusic;

	public bool ronquidomanStage;

	public Text lifeCounter;


	void Start () {

		//Importante para poder usar las funciones desde afuera
		if (gm == null) {
			gm = this.gameObject.GetComponent<GameManager> ();
		}

		player = GameObject.FindGameObjectWithTag ("Player");
		//For some reason I had to add a number to y axis
		defaultPosition = new Vector2 (player.transform.position.x, player.transform.position.y + 1);
		defaultCameraPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

		//Healthbar
		bars = GameObject.FindGameObjectsWithTag ("Bar"); //Las cuenta de abajo hacia arriba
		foreach (GameObject temp in bars)
		{
			barritas.Add(temp.name);
		}
		barritas.Sort ();
		maxHp = playerHp;

		lifeCounter.text = "X " + GlobalOptions.lives.ToString();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SubstractHealth(int healthAmount) {
		
		if (healthAmount > playerHp) {
			healthAmount = playerHp;
		}
			
		//Por cada unidad de daño saca una barra de energía
		for (int i = 0; i < healthAmount; i++) {
			//bars [i].SetActive (false);
			GameObject.Find(barritas[playerHp]).GetComponent<Image>().enabled = false;
			//barritas.Remove (barritas [playerHp]);
			playerHp -= 1;
		}

		if (playerHp > 0) {
			Instantiate (hurtedThing, player.transform.position, player.transform.rotation, player.transform);
			player.GetComponent<AudioSource> ().Play ();
		}

		if (playerHp == 0) {
			GameObject.Find(barritas[playerHp]).GetComponent<Image>().enabled = false;
			PlayerDeath ();

		}

	}

	public void HealthUp(int healthAmount) {

		if (playerHp + healthAmount > maxHp) {
			healthAmount = maxHp - playerHp;
		}

		/* Old way
		 for (int i = 0; i < healthAmount; i++) {
			playerHp += 1;
			GameObject.Find(barritas[playerHp]).GetComponent<Image>().enabled = true;

		}*/

		//Enable bars with freeze and delay
		if (healthAmount > 0) {
			Time.timeScale = 0f;
			float time = 0.0f;
			int counter = healthAmount; //To know where to restore normal time
			for (int i = 0; i < healthAmount; i++) {
				playerHp += 1;
				counter--;
				StartCoroutine (EnableBar(playerHp, time, counter));
				time += 0.1f;
			}
		}


		if (playerHp > maxHp) {
			playerHp = maxHp;
		}

	}

	//Enable bars with freeze and delay
	IEnumerator EnableBar(int pos, float delay, int counter) {

		yield return new WaitForSecondsRealtime (delay);

		GameObject.Find(barritas[pos]).GetComponent<Image>().enabled = true;
		GameObject.Find (barritas [pos]).GetComponent<AudioSource> ().Play ();
		
		if (counter == 0) {
			Time.timeScale = 1f;
		}
	}


	public void PlayerDeath() {
		
		ChangeLife(-1);

		Instantiate (explosionPrefab, player.transform.position, player.transform.rotation);
		player.GetComponent<JpControl> ().TurnRight ();
		Camera.main.GetComponent<Camera2D> ().enabled = false;
		player.GetComponent<SpriteRenderer> ().enabled = false;
		player.GetComponent<JpControl> ().enabled = false;
		player.GetComponent<Invincibility> ().enabled = false;
		player.GetComponent<BoxCollider2D> ().enabled = false;
		Camera.main.GetComponent<AudioSource> ().Stop ();
		if (GlobalOptions.lives > 0) {
			Invoke ("Respawn", 2f);
		} else {
			Invoke ("GameOver", 2f);
		}
		//Destroy (gameObject);

	}

	public void ChangeLife(int life) {

		GlobalOptions.lives += life;
		lifeCounter.text = "X " + GlobalOptions.lives.ToString();
	}

	/*
	public static Vector2 GetActiveCheckpointPosition() {
		
		Vector2 result = defaultPosition;
		if (Checkpoint.checkpointsList != null) {
			foreach (GameObject temp in Checkpoint.checkpointsList) {
				if (temp.GetComponent<Checkpoint> ().activated) {
					result = temp.transform.position;
					break;
				}
			}
		}
		return result;

	} */

	public void Respawn() {

		Camera.main.GetComponent<AudioSource> ().Play ();

		if (GameObject.Find("JPExplosion(Clone)")) {
			Destroy (GameObject.Find ("JPExplosion(Clone)"));
			
		}
		//Show black screen
		GameObject.Find ("BlackScreen").GetComponent<Image> ().enabled = true;
		GameObject.Find ("BlackScreen").GetComponent<BlackScreenDisabler> ().enabled = true;

		//Set the respawn to default values
		respawnPosition = defaultPosition;
		Vector3 cameraPos = defaultCameraPos;
		Camera.main.GetComponent<Camera2D> ().isXLocked = true;

		//Search if any checkpoint is enabled to take the respawn and camera position
		if (Checkpoint.checkpointsList != null) {
			foreach (GameObject temp in Checkpoint.checkpointsList) {
				if (temp.GetComponent<Checkpoint> ().activated) {
					
					respawnPosition = temp.GetComponent<Checkpoint> ().respawnPosition;
					cameraPos = temp.GetComponent<Checkpoint> ().cameraPosition;

					if (temp.name.Equals ("Checkpoint (1)")) {
						//Disable every zone
						for (int i = 0; i < zones.Length; i++) {
							zones [i].SetActive (false);
						}

						//Enable first zone
						zones[0].SetActive (true);


						//Restart all the transitions
						if (CameraTransition.transitionList != null) {
							foreach (GameObject item in CameraTransition.transitionList) {
								item.GetComponent<CameraTransition> ().RestartTransition ();
							}
						}
							

						if (ronquidomanStage) {
							GameObject.Find ("WorldLimit (1)").GetComponent<EdgeCollider2D> ().enabled = true;
							Camera.main.GetComponent<Camera2D> ().xLimitLeft = -0.06f;
							Camera.main.GetComponent<Camera2D> ().xLimitRight = 59.318f;
						}

					} else if (temp.name.Equals ("Checkpoint (2)")) {
						
						for (int i = 0; i < zones.Length; i++) {
							zones [i].SetActive (false);
						}

						if (!ronquidomanStage) {
							zones [2].SetActive (true);
						} else {
							zones [4].SetActive (true);
						}

						if (CameraTransition.transitionList != null) {
							foreach (GameObject item in CameraTransition.transitionList) {
								item.GetComponent<CameraTransition> ().RestartTransition ();
							}
						}
						if (!ronquidomanStage) {
							//Set new camera limits
							Camera.main.GetComponent<Camera2D> ().xLimitRight = 
							GameObject.Find ("Transition (1)").GetComponent<CameraTransition> ().newXLimitR;
							Camera.main.GetComponent<Camera2D> ().xLimitLeft = 
							GameObject.Find ("Transition (1)").GetComponent<CameraTransition> ().newXLimitL;
						} else {
							GameObject.Find ("WorldLimit (2)").GetComponent<EdgeCollider2D> ().enabled = true;
							Camera.main.GetComponent<Camera2D> ().xLimitRight = 
								GameObject.Find ("Transit (3)").GetComponent<CameraTransition> ().newXLimitR;
							Camera.main.GetComponent<Camera2D> ().xLimitLeft = 
								GameObject.Find ("Transit (3)").GetComponent<CameraTransition> ().newXLimitL;
						}
					} else if (temp.name.Equals ("Checkpoint (3)")) {
						/*
						zone3.SetActive (true);
						zone4.SetActive (false);
						GameObject.Find ("Transition (2)").GetComponent<EdgeCollider2D> ().enabled = true;
						GameObject.Find ("Transition (2)").GetComponent<BoxCollider2D> ().enabled = false;
						*/

						//ChangeLife (-1);
						if (GlobalOptions.lives > 0) {
							if (ronquidomanStage) {
								//Debug.Log (GlobalOptions.lives);
								SceneManager.LoadScene ("RonquidoMan Battle");	
							} else {
								SceneManager.LoadScene ("CelesteMan Battle");	
							}
						} else {
							
							//SceneManager.LoadScene ("Stage Selection");
						}

					}
					//break;
				} else {

					/*
					Debug.Log ("Holi");
					if (ronquidomanStage) {
						SceneManager.LoadScene ("RonquidoMan Stage");
					} else {
						SceneManager.LoadScene ("CelesteManStage");
					}*/


					zones[0].SetActive (false);
					zones[0].SetActive (true);

				}
			}
		}  else {
			zones[0].SetActive (false);
			zones[0].SetActive (true);
		}

		//Restore hp
		for (int i = 0; i <= maxHp; i++) {
			GameObject.Find(barritas[i]).GetComponent<Image>().enabled = true;
		}
		playerHp = maxHp;

		/*
		//Destroy enemies
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject en in enemies) {
			Destroy (en);
		}
		*/

		//Respawn
		player.transform.position = respawnPosition;
		player.GetComponent<SpriteRenderer> ().enabled = true;
		player.GetComponent<Animator> ().enabled = true;
		player.GetComponent<JpControl> ().enabled = true;
		player.GetComponent<BoxCollider2D> ().enabled = true;
		player.tag = "Player";

		Camera.main.transform.position = cameraPos;
		if (!Camera.main.GetComponent<Camera2D> ().enabled) {
			Camera.main.GetComponent<Camera2D> ().enabled = true;
		}
			
	}

	public void GameOver() {

		lifeCounter.text = "X " + GlobalOptions.lives.ToString();
		GlobalOptions.lives = 3;
		SceneManager.LoadScene ("Stage Selection");
	}

	//Winning a boss fight
	public void Victory() {

		Invoke ("VictoryDance", 4.5f);

	}

	void VictoryDance() {

		if (GameObject.Find ("JP")) {
			GameObject.Find ("JP").GetComponent<Animator> ().SetTrigger ("victory");
			GameObject.Find ("JP").GetComponent<Animator> ().Play("JpRock");
			Camera.main.GetComponent<AudioSource>().Stop();
			Camera.main.GetComponent<AudioSource>().clip = victoryMusic;
			Camera.main.GetComponent<AudioSource>().Play();

			Invoke ("EnableStageSelect", 3f);
		}
	}

	void EnableStageSelect() {

		GetComponent<ToStageSelect> ().enabled = true;
	}

	public void EnableBossTransition() {
		GameObject.Find ("Boss Transition").GetComponent<EdgeCollider2D> ().isTrigger = true;
	}
		
}
