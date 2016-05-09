using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour {
	public GameObject FlightPrefab;
	
	void Start () {
		PutFlight ();
		PutFlight ();
		PutFlight ();
		PutFlight ();
	}

	void Update () {
		int ran = Random.Range (0, 48);
		if(ran==3) PutFlight ();

	}

	void PutFlight(){
		Instantiate(FlightPrefab);
	}

}
