using UnityEngine;
using System.Collections;

public class SphereController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		// transform.Translate(x * 0.1f , y * 0.1f ,0.0f);
		transform.Translate(x * 0.1f , 0.0f ,y * 0.1f);
	}
}
