using UnityEngine; 

using System.Collections; 

public class Player : MonoBehaviour { 

	public float PlayerSpeed;
	public int playernumber;
	public GameObject ProjectilePrefab;
	public int shipInvisibleTime; 
	public GameObject ExplosionPrefab;
	private Enemy enemy; 
	public VoiceBox box001;
	
	public static int Score = 0; 
	public static int Lives = 10; 
	public static int Missed = 0;

	void OnGUI() { 
		GUI.Label(new Rect(10, 10, 150, 20),"Score: " + Player.Score.ToString()); 
		GUI.Label(new Rect(10, 30, 60, 20),"Lives: " + Player.Lives.ToString()); 
		GUI.Label(new Rect(10, 50, 120, 20),"Missed: " +Player.Missed.ToString()); 
	} 

	enum State { 
		Playing, Explosion, Invincible 
	} 
	
	private State state = State.Playing; 
	private float blinkRate=0.1f; 
	private int numberOfTimesToBlink = 10; 
	private int blinkCount=0;
	private int autoShoot=-1;

	
	public int GetState(){
		if (state == State.Playing) {
			return 1;
		} else{
			return 0;
		}
	}

	void Start() { 
		enemy = (Enemy)GameObject.Find("Enemy").GetComponent("Enemy"); 
	} 



	void Update () { 
		float horizontalMove = Input.GetAxisRaw("Horizontal")* PlayerSpeed * Time.deltaTime; 
		float verticalMove = Input.GetAxisRaw("Vertical") * PlayerSpeed *playernumber* Time.deltaTime;
		if(state!=State.Explosion) transform.Translate(Vector3.up * verticalMove);
		if(state!=State.Explosion) transform.Translate(Vector3.right * horizontalMove); 
		if (transform.position.x <= -9.25f) 
			transform.position = new Vector3(9.25f, transform.position.y, 
			                                 transform.position.z); 
		else if (transform.position.x >= 9.25f) 
			transform.position = new Vector3(-9.25f, transform.position.y, 
			                                 transform.position.z);

		if (transform.position.y <= -3f) 
			transform.position = new Vector3(transform.position.x , -3f, 
			                                 transform.position.z); 
		else if (transform.position.y >= 7f) 
			transform.position = new Vector3(transform.position.x, 7f, 
			                                 transform.position.z);

		if (Input.GetKeyDown("space") && state!= State.Explosion) { 
			Vector3 position = new Vector3(transform.position.x, 
			                               transform.position.y + (transform.localScale.y/1),transform.position.z); 
			Instantiate(ProjectilePrefab, position, Quaternion.identity); 
		}

		if (Input.GetKeyDown("g")) { 
			autoShoot=autoShoot*-1;
			if(autoShoot==1 && transform.position.z!=300){
				box001.CastVoice();
			}
		}

		if (Input.GetKeyDown("c")) { 
			if(transform.position.z==300) 
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, 
			                                 0.0f);
			}else {
				transform.position = new Vector3(transform.position.x, transform.position.y, 
				                                 300);
			}
		}

		if (autoShoot == 1) {
						Vector3 autoPosition = new Vector3 (transform.position.x, 
		                               transform.position.y + 
			                                    (transform.localScale.y / 1),transform.position.z); 
						Instantiate (ProjectilePrefab, autoPosition, Quaternion.identity); 
				}
	} 
	void OnTriggerEnter(Collider otherObject) { 
		if(otherObject.tag == "enemy" && state == State.Playing) { 
			PlayerDie("enemy");
		} 
	} 

	public int PlayerDie(string playerdie){
		if (state == State.Playing ) {
			if(playerdie=="flight") {
				Player.Score=Player.Score-1000;
				}
			if(playerdie=="enemy") {
				Player.Lives--;
				Instantiate (ExplosionPrefab, enemy.transform.position, enemy.transform.rotation); 
				enemy.SetPositionAndSpeed ();  
			}
			StartCoroutine ("DestroyShip"); 
			return 0;
		} else {
			return -1;
		}
	}



	public IEnumerator DestroyShip() { 
				if (playernumber == 1) {
						state = State.Explosion; 
						gameObject.renderer.enabled = false;
						transform.position = new Vector3 (0.0f, 0.0f, transform.position.z); 
						yield return new WaitForSeconds (shipInvisibleTime);  
						gameObject.renderer.enabled = true; 

						if (Player.Lives > 0) {
								state = State.Invincible; 
								while (blinkCount < numberOfTimesToBlink) { 
										gameObject.renderer.enabled = !gameObject.renderer.enabled; 
			
										if (gameObject.renderer.enabled == true)
												blinkCount++; 
			
										yield return new WaitForSeconds (blinkRate); 
								} 
								blinkCount = 0; 
								state = State.Playing; 
						} else {
								Application.LoadLevel (3);
						}
				} else if (playernumber == -1) {

					audio.Play();
					state = State.Explosion; 
					//gameObject.renderer.enabled = false;
					transform.position = new Vector3 (0.0f, 0.0f, transform.position.z); 
					yield return new WaitForSeconds (shipInvisibleTime);  
					gameObject.renderer.enabled = true; 
					
					if (Player.Lives > 0) {
						/*state = State.Invincible; 
						while (blinkCount < numberOfTimesToBlink) { 
							gameObject.renderer.enabled = !gameObject.renderer.enabled; 
					
							if (gameObject.renderer.enabled == true)
								blinkCount++; 
					
							yield return new WaitForSeconds (blinkRate); 
						} 
						blinkCount = 0; */
						state = State.Playing; 
					} else {
						Application.LoadLevel (3);
					}
				}
		}
}
