using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFire : MonoBehaviour {

	public GameObject lilFirePrefab;
	public float timeToDrop = 4;
    private List<GameObject> minions = new List<GameObject>();
    [SerializeField] private int maxMinions = 4;

	private float savedTime;

	// Use this for initialization
	void Start () {

		savedTime = timeToDrop;
		
	}
	
	// Update is called once per frame
	void Update () {
        
		if (Time.time >= savedTime) {

			gameObject.GetComponent<Animator> ().SetTrigger ("dropFire");
			savedTime = Time.time + timeToDrop;
		}
        
	}

	public void SpawnLilFire() {

        bool random = Random.Range(0, 2) == 1 ? true : false;

        GameObject newMinion = Instantiate(lilFirePrefab, new Vector2(transform.position.x - 0.4f, transform.position.y), transform.rotation);
        minions.Add(newMinion);
        if (minions.Count > maxMinions)
        {
            Destroy(minions[0]);
            minions.Remove(minions[0]);
        }
        if (random) newMinion.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 200));

    }
}
