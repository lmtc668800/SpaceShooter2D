using UnityEngine; 
using System.Collections; 

public class Lose : MonoBehaviour 
{ 
	public Texture backgroundTexture; 
	private string instructionText = "You lose...\n Press 'R' to restart."; 
	
	void OnGUI() { 
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),backgroundTexture); 
		GUI.Label(new Rect(0, 0, 250, 200), instructionText); 
		
		if (Input.GetKeyDown("r")) { 
			
			Player.Score = 0; 
			Player.Lives = 300; 
			Player.Missed = 0; 
			
			Application.LoadLevel(1); 
		} 
		
	} 
}