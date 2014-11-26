using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

	public GameObject cell;
	public float speed = 1f;
    public bool updated = true;

    public static ArrayList foods;
    public static ArrayList cells;

	private int food = 0;
	private float minv = 1000000;
	private Vector3 minPos = Vector3.zero;
    private GameObject minObj = null;

	// Use this for initialization
	void Start () {
		if (foods == null) {
			foods = new ArrayList ();
		}
        if (cells == null)
        {
            cells = new ArrayList();
            cells.Add(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (food > 0) {
			cells.Add((GameObject)Instantiate(cell, transform.position, transform.rotation));
			food = 0;
		}

		if (!updated) {
			minv = 1000000;
			minPos = Vector3.zero;
			foreach (GameObject o in foods) {
				Vector3 v = transform.position - o.transform.position;
				float d = v.sqrMagnitude;
				if (d < minv)
				{
					minv = d;
					minPos = o.transform.position;
                    minObj = o;
				}
			}
            updated = true;
		}

        if (minObj != null)
        {
            Vector3 dir = minPos - transform.position;
            rigidbody2D.AddForce(speed * dir.normalized);
        }
        else
        {
            updated = false;
        }
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Food") {
			foods.Remove(other.gameObject);
			Destroy (other.gameObject);
			food++;
		}
	}
}
