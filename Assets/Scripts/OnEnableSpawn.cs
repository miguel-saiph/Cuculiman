using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableSpawn : MonoBehaviour {

	public GameObject enemy;

	void OnEnable() {

		Instantiate (enemy, transform.position, transform.rotation);
	}
}
