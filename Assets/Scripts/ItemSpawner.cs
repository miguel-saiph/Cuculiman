using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

	public GameObject itemPrefab;

	void OnDestroy() {

		Instantiate (itemPrefab, transform.position, transform.rotation);
	}
}
