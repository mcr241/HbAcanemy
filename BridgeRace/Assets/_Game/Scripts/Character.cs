using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TypeColor
{
    Red, Green, Blue, Yellow, Null
}

public class Character : MonoBehaviour
{
    public TypeColor typeColor;
    [SerializeField] GameObject brickInPlayer;
    [SerializeField] GameObject brickInBridge;
    [SerializeField] Transform human;
    [SerializeField] List<GameObject> listBlocks = new List<GameObject>();
    [SerializeField] LayerMask layerBrick;
    [SerializeField] LayerMask layerBrickBridge;
    public Material material;
    [SerializeField] float disBrickBridge;
    public bool isWin;
    public bool isOnBridge;
    GameObject bridge;

    void AddBrick()
    {
        GameObject obj = Instantiate(brickInPlayer, human.transform.position /*+ new Vector3(0, -0.46f, 0)*/, human.transform.rotation, transform.GetChild(0));
        human.transform.localPosition = human.transform.localPosition + new Vector3(0, 0.22f, 0);
        //human.transform.DOLocalMove(human.transform.position + Vector3.up, timeJump);
        //ChangeAnim("jump");
        listBlocks.Add(obj);
    }

    public void SubBrick()
    {
        if (listBlocks.Count > 0)
        {
            human.transform.localPosition = human.transform.localPosition + new Vector3(0, -0.22f, 0);
            //human.transform.DOLocalMove(human.transform.position + Vector3.down, timeJump);
            //ChangeAnim("jump");
            GameObject obj = listBlocks[listBlocks.Count - 1];
            listBlocks.Remove(listBlocks[listBlocks.Count - 1]);
            Destroy(obj);
        }
    }

    public bool CanSubBrick()
    {
        return listBlocks.Count > 0;
    }

    public void Win()
    {
        while (listBlocks.Count > 0)
        {
            human.transform.localPosition = human.transform.localPosition + new Vector3(0, -0.22f, 0);
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
            if (other.GetComponent<Brick>().typeColor == typeColor)
            {
                AddBrick();
                Destroy(other.gameObject);
            }
        }
    }
}
