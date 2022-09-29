using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum TypeDirection
{
    Null, Right, Left, Back, Forward
}

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody rb;

    [SerializeField] string currentAnimName;
    [SerializeField] IState currentState;
    [SerializeField] float moveSpeed;
    [SerializeField] float timeJump;

    [SerializeField] LayerMask layerGround;

    [SerializeField] GameObject block;
    [SerializeField] GameObject human;

    List<GameObject> listBlocks = new List<GameObject>();

    public bool canMove;
    bool moving = false;
    public TypeDirection directionMoving;
    Vector3 firstMousePoint;

    private void OnEnable()
    {
        ChangeAnim("idle");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canMove = true;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                firstMousePoint = raycastHit.point;
            }
        }

        if (canMove && !moving)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (Vector3.Distance(firstMousePoint, raycastHit.point) >= 5f)
                {
                    float distanceX = raycastHit.point.x - firstMousePoint.x;
                    float distanceZ = raycastHit.point.z - firstMousePoint.z;
                    Vector3 direction;
                    if (Mathf.Abs(distanceX) > Mathf.Abs(distanceZ))
                    {
                        if (distanceX > 0)
                        {
                            directionMoving = TypeDirection.Right;
                            direction = Vector3.right;
                        }
                        else
                        {
                            directionMoving = TypeDirection.Left;
                            direction = Vector3.left;
                        }
                    }
                    else
                    {
                        if (distanceZ > 0)
                        {
                            directionMoving = TypeDirection.Forward;
                            direction = Vector3.forward;
                        }
                        else
                        {
                            directionMoving = TypeDirection.Back;
                            direction = Vector3.back;
                        }
                    }
                    firstMousePoint = raycastHit.point;
                    Vector3 posRay = transform.position + Vector3.up;
                    //Debug.Log(posRay);
                    while (Physics.Raycast(posRay, Vector3.down, 2, layerGround))
                    {
                        posRay += direction;
                    }
                    //Debug.Log(posRay - direction);
                    Moving(posRay - direction - Vector3.up);
                    

                }
                else
                {
                    directionMoving = TypeDirection.Null;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false; 
        }
    }

    protected void ChangeAnim(string animName)
    {
        //if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void Moving(Vector3 endPoint)
    {
        moving = true;
        //rb.velocity = moveSpeed * GetMoveDirection();
        transform.DOMove(endPoint, Vector3.Distance(endPoint, transform.position) / moveSpeed)
            .OnComplete(delegate { StopMoving(); });
    }

    public void StopMoving()
    {
        moving = false;
        //rb.velocity = Vector3.zero;
    }

    Vector3 GetMoveDirection()
    {
        if (directionMoving == TypeDirection.Right)
        {
            return transform.right;
        }
        else if (directionMoving == TypeDirection.Left)
        {
            return -transform.right;
        }
        else if (directionMoving == TypeDirection.Forward)
        {
            return transform.forward;
        }
        else if (directionMoving == TypeDirection.Back)
        {
            return -transform.forward;
        }
        return Vector3.zero;
    }

    bool CheckGroundAhead()
    {

        return false;
    }

    void AddBlock()
    {
        GameObject obj = Instantiate(block, human.transform.position /*+ new Vector3(0, -0.46f, 0)*/, block.transform.rotation, transform);
        human.transform.localPosition = human.transform.localPosition + new Vector3(0, 0.45f, 0);
        //human.transform.DOLocalMove(human.transform.position + Vector3.up, timeJump);
        ChangeAnim("jump");

        listBlocks.Add(obj);
    }

    void SubBlock()
    {
        human.transform.localPosition = human.transform.localPosition + new Vector3(0, -0.45f, 0);
        //human.transform.DOLocalMove(human.transform.position + Vector3.down, timeJump);
        ChangeAnim("jump");
        GameObject obj = listBlocks[listBlocks.Count-1];
        listBlocks.Remove(listBlocks[listBlocks.Count - 1]);
        Destroy(obj);
    }

    public void Win()
    {
        for (int i = listBlocks.Count-1; i>=0; i--)
        {
            human.transform.localPosition = human.transform.localPosition + new Vector3(0, -0.45f, 0);
            GameObject obj = listBlocks[i];
            listBlocks.Remove(listBlocks[i]);
            Destroy(obj);
        }
        ChangeAnim("win");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.layer);
        if ((layerGround.value & (1 << collision.gameObject.layer)) > 0)
        {
            //Debug.Log("hit");
            if (collision.gameObject.CompareTag("Block"))
            {
                Debug.Log("add");
                AddBlock();
                Destroy(collision.gameObject);
            }
            else if(collision.gameObject.CompareTag("UnBlock"))
            {
                Debug.Log("sub");
                SubBlock();
            }
        }
    }
}
