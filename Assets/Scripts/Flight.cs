using UnityEngine; 
using System.Collections; 
public class Flight : MonoBehaviour { 
	public float MinSpeed; 
	public float MaxSpeed; 
	private float currentDownSpeed; 
	private float currentRightSpeed;
	private float x, y, z; 
	private Player player; 
	private Projectile projectile;
	public GameObject ExplosionPrefab;
	public GameObject FlightPrefab;

	void Start() { 
		SetPositionAndSpeed(); 
		player = (Player)GameObject.Find ("Player").GetComponent ("Player");
	} 

	void Update () {  
		float outToMove = currentDownSpeed * Time.deltaTime; 
		transform.Translate(Vector3.down * outToMove,Space.World); 
		if (transform.position.y <=-5) { 
			DestorySelf();
			Player.Missed++;
		} 
		float horizontalMove = currentRightSpeed * Time.deltaTime;
		transform.Translate(Vector3.right * horizontalMove,Space.World); 
		if (transform.position.x < -7) {
			SetRightSpeed();
		}
		if (transform.position.x > 7) {
			SetRightSpeed();
			currentRightSpeed=currentRightSpeed*-1;
		}

	} 

	void OnTriggerEnter(Collider otherObject) { 
		if(otherObject.tag == "Player") {  
			if(player.PlayerDie("flight")!=-1) {
				DestorySelf();
				Instantiate(ExplosionPrefab, transform.position, transform.rotation);
			}
			
		} 
		if (otherObject.tag == "projectile") {
			Instantiate(ExplosionPrefab,  transform.position, transform.rotation); 
			DestorySelf();  
			Destroy(otherObject.gameObject);
			Player.Score+=200;
			if (Player.Score > 20000) Application.LoadLevel(2);
		} 
	
		}



	public void DestorySelf(){
		Destroy (gameObject);
	}

	public void SetPositionAndSpeed() { 
		currentDownSpeed = Random.Range(MinSpeed,MaxSpeed);
		SetRightSpeed ();
		x = Random.Range(-7.0f, 7.0f); 
		y = 7.0f; 
		z = 0.0f;
		transform.position = new Vector3(x, y, z); 
	} 
	public void SetRightSpeed(){
		currentRightSpeed = Random.Range(2,10); 
	}
}
