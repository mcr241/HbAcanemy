using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject brickInPlayer;
    [SerializeField] GameObject brickInBridge;
    [SerializeField] Transform human;
    [SerializeField] List<GameObject> listBlocks = new List<GameObject>();
    [SerializeField] LayerMask layerBrick;
    [SerializeField] LayerMask layerBrickBridge;
    [SerializeField] Material material;
    [SerializeField] float disBrickBridge;
    public bool isWin;
    bool isOnBridge;
    GameObject bridge;

    private void Update()
    {
        if (isOnBridge)
        {
            if (listBlocks.Count > 0 && !Physics.Raycast(transform.position, Vector2.down, 2f, layerBrickBridge))
                SubBrick();
        }
    }

    void AddBrick()
    {
        GameObject obj = Instantiate(brickInPlayer, human.transform.position /*+ new Vector3(0, -0.46f, 0)*/, brickInPlayer.transform.rotation, transform);
        human.transform.localPosition = human.transform.localPosition + new Vector3(0, 0.25f, 0);
        //human.transform.DOLocalMove(human.transform.position + Vector3.up, timeJump);
        //ChangeAnim("jump");
        listBlocks.Add(obj);
    }

    void SubBrick()
    {
        human.transform.localPosition = human.transform.localPosition + new Vector3(0, -0.25f, 0);
        //human.transform.DOLocalMove(human.transform.position + Vector3.down, timeJump);
        //ChangeAnim("jump");
        GameObject obj = listBlocks[listBlocks.Count - 1];
        listBlocks.Remove(listBlocks[listBlocks.Count - 1]);
        Destroy(obj);
        obj = Instantiate(brickInBridge, transform.position + Vector3.down, brickInBridge.transform.rotation, bridge.transform);
    }

    public void Win()
    {
        while (listBlocks.Count > 0)
        {
            human.transform.localPosition = human.transform.localPosition + new Vector3(0, -0.25f, 0);
            GameObject obj = listBlocks[listBlocks.Count - 1];
            listBlocks.Remove(listBlocks[listBlocks.Count - 1]);
            Destroy(obj);
        }
        //ChangeAnim("win");
        human.transform.rotation = Quaternion.identity;
        isWin = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.layer);
        if ((layerBrick.value & (1 << collision.gameObject.layer)) > 0)
        {
            //Debug.Log("hit");
            /*if (collision.gameObject.CompareTag("Brick"))
            {
                AddBrick();
                Destroy(collision.gameObject);
            }
            else */
            if (collision.gameObject.CompareTag("UnBrick"))
            {
                isOnBridge = true;
                bridge = collision.transform.parent.gameObject;
                //collision.gameObject.GetComponent<MeshRenderer>().material = material;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((layerBrick.value & (1 << collision.gameObject.layer)) > 0)
        {
            if (collision.gameObject.CompareTag("UnBrick"))
            {
                isOnBridge = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((layerBrick.value & (1 << other.gameObject.layer)) > 0)
        {
            if (other.CompareTag(gameObject.tag))
            {
                AddBrick();
                Destroy(other.gameObject);
            }
            /*else if (other.CompareTag("UnBrick"))
            {
                SubBrick();
                //collision.gameObject.GetComponent<MeshRenderer>().material = material;
            }*/
        }
    }
}
