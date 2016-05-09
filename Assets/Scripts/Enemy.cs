using UnityEngine; 
using System.Collections; 
public class Enemy : MonoBehaviour { 
	public float MinSpeed; 
	public float MaxSpeed; 
	private float currentRotationSpeed; 
	private float MinRotateSpeed=60.0f; 
	private float MaxRotateSpeed=120.0f;
	private float currentSpeed; 
	private float x, y, z; 
	void Start() { 
		SetPositionAndSpeed(); 
	} 
	void Update () { 
		float currentRotate = currentRotationSpeed * Time.deltaTime; 
		transform.Rotate(new Vector3(-1,0,0) * currentRotate); 
		float outToMove = currentSpeed * Time.deltaTime; 
		transform.Translate(Vector3.down * outToMove,Space.World); 
		if (transform.position.y <=-5) { 
			SetPositionAndSpeed(); 
			Player.Missed++;
		} 
	} 
	public void SetPositionAndSpeed() { 
		currentSpeed = Random.Range(MinSpeed,MaxSpeed); 
		currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed); 
		x = Random.Range(-7.0f, 7.0f); 
		y = 7.0f; 
		z = 0.0f;
		transform.position = new Vector3(x, y, z); 
	
	} 
}
