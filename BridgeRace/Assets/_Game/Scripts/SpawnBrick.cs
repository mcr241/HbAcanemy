using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBrick : MonoBehaviour
{
    [SerializeField] int numberBrick;
    [SerializeField] int numberColor;
    [SerializeField] float timeDelay;
    [SerializeField] List<GameObject> listPrefabBrick;


    [SerializeField] GameObject[,] bricksArray;
    // Start is called before the first frame update
    void OnEnable()
    {
        bricksArray = new GameObject[(int)(Mathf.Sqrt(numberBrick)), (int)(Mathf.Sqrt(numberBrick))];
        for (int i = 0; i < listPrefabBrick.Count; i++)
        {
            for (int j = 0; j < numberBrick/numberColor; j++)
            {
                Spawn(listPrefabBrick[i]);
            }
        }


        /*for (int i = -(int)(Mathf.Sqrt(numberBrick) / 2); i <= (int)(Mathf.Sqrt(numberBrick) / 2); i++)
        {
            for (int j = -(int)(Mathf.Sqrt(numberBrick) / 2); j <= (int)(Mathf.Sqrt(numberBrick) / 2); j++)
            {
                Spawn(i, j);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(int x, int z, float timeDelay = 0)
    {

        GameObject obj = Instantiate(listPrefabBrick[Random.RandomRange(0, listPrefabBrick.Count)], new Vector3(x, listPrefabBrick[Random.RandomRange(0, listPrefabBrick.Count)].transform.position.y, z), Quaternion.identity, transform);
        bricksArray[x, z] = obj;
        
    }

    void Spawn(GameObject prefab)
    {
        while (true)
        {
            int x = Random.RandomRange(0, (int)(Mathf.Sqrt(numberBrick)));
            int z = Random.RandomRange(0, (int)(Mathf.Sqrt(numberBrick)));
            //Debug.Log(x - (int)(Mathf.Sqrt(numberBrick)) / 2);
            //Debug.Log(z - (int)(Mathf.Sqrt(numberBrick)) / 2);
            //Debug.Log(bricksArray[x, z]);
            if (bricksArray[x, z] == null)
            {
                GameObject obj = Instantiate(prefab, new Vector3(x - (int)(Mathf.Sqrt(numberBrick)) / 2, 0.075f, z - (int)(Mathf.Sqrt(numberBrick)) / 2) + transform.position, Quaternion.identity, transform);
                bricksArray[x, z] = obj;
                return;
            }
        }
    }

    void Despawn(int posX, int posZ)
    {
        Destroy(bricksArray[posX, posZ]);
        bricksArray[posX, posZ] = null;
    }
}
