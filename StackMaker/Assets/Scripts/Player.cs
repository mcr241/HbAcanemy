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

    [SerializeField] Material material;

    [SerializeField] List<GameObject> listBlocks = new List<GameObject>();

    public bool canMove;
    public bool isWin;
    bool moving = false;
    public TypeDirection directionMoving;
    Vector3 firstMousePoint;
    Quaternion rotationHuman;

    private void Start()
    {
        rotationHuman = human.transform.rotation;
        Init();
    }

    private void Init()
    {
        ChangeAnim("idle");
        human.transform.rotation = rotationHuman;
        moving = false;
        isWin = false;
    }

    private void Update()
    {
        if (!isWin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canMove = true;
                firstMousePoint = Input.mousePosition;
                //Debug.Log(firstMousePoint);
                /*Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    firstMousePoint = raycastHit.point;
                }*/
            }

            if (canMove && !moving)
            {
                /*Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    if (Vector3.Distance(firstMousePoint, raycastHit.point) >= 3f)
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
                }*/
                float distanceX = Input.mousePosition.x - firstMousePoint.x;
                float distanceY = Input.mousePosition.y - firstMousePoint.y;
                if (Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY) >= 3f)
                {
                    Vector3 direction;
                    if (Mathf.Abs(distanceX) > Mathf.Abs(distanceY))
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
                        if (distanceY > 0)
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
                    firstMousePoint = Input.mousePosition;
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

            if (Input.GetMouseButtonUp(0))
            {
                canMove = false;
            }
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
        while (listBlocks.Count > 0)
        {
            human.transform.localPosition = human.transform.localPosition + new Vector3(0, -0.45f, 0);
            GameObject obj = listBlocks[listBlocks.Count - 1];
            listBlocks.Remove(listBlocks[listBlocks.Count - 1]);
            Destroy(obj);
        }
        ChangeAnim("win");
        human.transform.rotation = Quaternion.identity;
        isWin = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.layer);
        if ((layerGround.value & (1 << collision.gameObject.layer)) > 0)
        {
            //Debug.Log("hit");
            if (collision.gameObject.CompareTag("Block"))
            {
                AddBlock();
                Destroy(collision.gameObject);
            }
            else if(collision.gameObject.CompareTag("UnBlock"))
            {
                SubBlock();
                collision.gameObject.GetComponent<MeshRenderer>().material = material;
            }
        }
    }
}
