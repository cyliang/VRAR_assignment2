using UnityEngine;
using System.Collections;

public class CatScript : MonoBehaviour {
	
	public GameObject butterfly;
	public ButterflyHandler butterflyHandler;
	public CatHandler catHandler;
	public Animator catAnimator;
	public float minDistance;
	public float speed, runSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (catHandler.isReleased) {
			if (butterflyHandler.exist && Vector3.Distance (transform.position, butterfly.transform.position) > minDistance) {
				catAnimator.SetBool ("WalkBool", true);
				transform.LookAt (butterfly.transform);
				transform.position = Vector3.MoveTowards (transform.position, butterfly.transform.position, Time.deltaTime * speed);
			} else {
				catAnimator.SetBool ("WalkBool", false);
			}

			catAnimator.SetBool ("RunBool", false);
		} else {
			catAnimator.SetBool ("WalkBool", false);
			if (catHandler.exist && Vector3.Distance (transform.position, catHandler.transform.position) > minDistance) {
				catAnimator.SetBool ("RunBool", true);
				transform.position = Vector3.MoveTowards (transform.position, catHandler.transform.position, Time.deltaTime * runSpeed);
				transform.LookAt (catHandler.transform);
			} else {
				catAnimator.SetBool ("RunBool", false);
			}
		}
	}
}
