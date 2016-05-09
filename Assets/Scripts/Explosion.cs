using UnityEngine; 
using System.Collections; 

public class Explosion : MonoBehaviour { 
	
	void Update () { 
		if (!particleSystem.IsAlive()) Destroy(gameObject); 
	} 
} 