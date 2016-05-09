using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float ProjectileSpeed; 
	public GameObject ExplosionPrefab; 
	private Transform myTransform; 
	private Enemy enemy; 
	void Start () { 
		myTransform = transform; 
		enemy = (Enemy)GameObject.Find("Enemy").GetComponent("Enemy"); 
	} 
	void Update () { 
		float outToMove = ProjectileSpeed * Time.deltaTime; 
		myTransform.Translate(Vector3.up * outToMove); 
		if (myTransform.position.y > 6.25f) 
			Destroy(gameObject); 

	}
	void OnTriggerEnter(Collider otherObject) { 
		if(otherObject.tag == "enemy") { 
			Instantiate(ExplosionPrefab, enemy.transform.position, enemy.transform.rotation); 
			enemy.MinSpeed += 1.2f; 
			enemy.MaxSpeed += 1.6f;
			if(enemy.MaxSpeed>=40f){
				enemy.MinSpeed=4.0f;
				enemy.MaxSpeed=6.0f;
			
			}
			enemy.SetPositionAndSpeed(); 
			Destroy(gameObject); 
			Player.Score+=100;
			if (Player.Score > 20000) Application.LoadLevel(2);
		} 
	} 

}
