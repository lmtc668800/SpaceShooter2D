using UnityEngine; 
using System.Collections; 

public class Stars : MonoBehaviour { 
	public float Speed; 
	
	void Update () { 
		float outToMove = Speed * Time.deltaTime; 
		transform.Translate(Vector3.down * outToMove, Space.World); 
		
		if (transform.position.y < -12) { 
			transform.position = 
				new Vector3(transform.position.x,19.5f,transform.position.z); 
		} 
	}
}