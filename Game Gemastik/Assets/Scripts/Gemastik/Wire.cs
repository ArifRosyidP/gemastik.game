using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint= transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 20;

        transform.position = newPosition;

        Vector3 direction = newPosition- startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist  = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
