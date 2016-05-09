using UnityEngine; 
using System.Collections; 

public class MainMenu: MonoBehaviour { 
	public Texture backgroundTexture; 
	private string instructionText= "Use WSAD or arrow key to move. \nHold space key for fire. " +
		"\nPress 'C' to change charactor \nPress 'G' for auto-fire. " +
		"\nAny key to start."; 

	void OnGUI() {
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backgroundTexture); 
		GUI.Label(new Rect(10,10,250,200),instructionText); 
		
		if (Input.anyKeyDown) 
			Application.LoadLevel(1); 
	} 
} 