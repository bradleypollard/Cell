using UnityEngine;
using System.Collections;

public class AdvancedFoodScript : MonoBehaviour
{
  public GameObject fud;

  // Use this for initialization
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    // Player click
    if (Input.GetMouseButtonDown(0))
    {
      Vector3 v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
      v3 = Camera.main.ScreenToWorldPoint(v3);
      GameObject f = (GameObject)Instantiate(fud, v3, Quaternion.identity);
      AdvancedLife.foods.Add(f);

      foreach (GameObject o in AdvancedLife.cells)
      {
        AdvancedLife l = o.GetComponent<AdvancedLife>();
        l.updated = false;
      }
    }
  }
}
