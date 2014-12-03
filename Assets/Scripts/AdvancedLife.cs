using UnityEngine;
using System.Collections;

public class AdvancedLife : MonoBehaviour
{

  public GameObject cell;
  public float speed = 1f;
  public float maxTTL = 5f;
  public int hunger = 3;
  public bool updated = true;

  public static ArrayList foods;
  public static ArrayList cells;

  private int food = 0;
  private float ttl = 0f;
  private float minv = 1000000;
  private Vector3 minPos = Vector3.zero;
  private GameObject minObj = null;

  // Use this for initialization
  void Start()
  {
    ttl = maxTTL;
    if (foods == null)
    {
      foods = new ArrayList();
    }
    if (cells == null)
    {
      cells = new ArrayList();
      cells.Add(gameObject);
    }
  }

  // Update is called once per frame
  void Update()
  {
    /* Death by old age */
    ttl -= Time.deltaTime;
    if (ttl < 0)
    {
      cells.Remove(gameObject);
      Destroy(gameObject);
    }

    /* Division */
    if (food > hunger)
    {
      cells.Add((GameObject)Instantiate(cell, transform.position, transform.rotation));
      food = 0;
      ttl = maxTTL;
    }

    /* Movement */
    if (!updated)
    {
      // Calculate target once only
      minv = 1000000;
      minPos = Vector3.zero;
      foreach (GameObject o in foods)
      {
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
    // Apply movement
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


  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Food")
    {
      /* Eating */
      foods.Remove(other.gameObject);
      Destroy(other.gameObject);
      food++;
    }
    else if (other.gameObject.tag == "Kill")
    {
      cells.Remove(gameObject);
      Destroy(gameObject);
    }
  }
}