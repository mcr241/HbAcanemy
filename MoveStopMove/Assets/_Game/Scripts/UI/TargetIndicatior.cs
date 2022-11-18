using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicatior : MonoBehaviour
{
    public Camera camera;
    List<GameObject> targetIndicatior;
    public GameObject prefab;

    private void Start()
    {
        
    }

    public void Spawn()
    {
        targetIndicatior.Add(Instantiate(prefab, transform));
    }

    public void DeSpawn()
    {
        Destroy(targetIndicatior[targetIndicatior.Count - 1]);
    }

    void Update()
    {
        for (int i = 0; i < targetIndicatior.Count; i++)
        {
            if (!CheckObjectInScreen(LevelManager.Instance.botsInMap[i].transform.position))
            {
                targetIndicatior[i].SetActive(true);
                targetIndicatior[i].transform.position = GetTargetIndicatior(LevelManager.Instance.botsInMap[i].transform.position);
            }
            else
            {
                targetIndicatior[i].SetActive(false);
            }
        }
    }

    bool CheckObjectInScreen(Vector3 pointObject)
    {
        return camera.WorldToViewportPoint(pointObject).x <= 1 && camera.WorldToViewportPoint(pointObject).x >= 0
            && camera.WorldToViewportPoint(pointObject).y <= 1 && camera.WorldToViewportPoint(pointObject).y >= 0;
    }

    public Vector2 GetTargetIndicatior(Vector3 pointObject)
    {
        Vector2 viewPoint = new Vector2(Mathf.Clamp(camera.WorldToViewportPoint(pointObject).x, 0.1f, 0.9f),
                                         Mathf.Clamp(camera.WorldToViewportPoint(pointObject).y, 0.05f, 0.95f));
        return (Vector2)camera.ViewportToScreenPoint(viewPoint);
    }
}
