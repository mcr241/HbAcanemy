using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] GameObject brick;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    List<GameObject> listBrick = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int numberBrick = (int)(Mathf.Abs(startPoint.position.y - endPoint.position.y) / brick.GetComponent<BoxCollider>().size.y);
        Vector3 vector = (endPoint.position - startPoint.position) / numberBrick;
        for (int i = 0; i < numberBrick; i++)
        {
            Instantiate(brick, startPoint.position + vector * i, Quaternion.identity, transform);
        }
    }

}
