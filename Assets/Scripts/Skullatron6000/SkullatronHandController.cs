using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullatronHandController : MonoBehaviour
{
    [SerializeField] BoxCollider handTrigger;
    [SerializeField] SkullatronHeadController skulllatronHead;
    [SerializeField] CageHandler cage;

    public Transform player;
    private Vector3 defaultPosition = new Vector3(19f, 2.5999999f, -26.7499981f);
    public Animator animator;
    private bool isAttacked = false;
    public bool isBossFightStarted = false;

    public int AttacksCounter = 0;
    public int MaxAttacks = 5;
    public bool isBossDead = false;

    [SerializeField] GameObject[] deadPieces;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        AttacksCounter = 0;
    }

    void Update()
    {
        // InvokeRepeating("AttackSlamPlayer", 8f, 8f);
        if (isBossFightStarted)
        {
            if (!isBossDead)
            {
                if (AttacksCounter < MaxAttacks)
                {
                    AttackSlamPlayer();
                }
                else
                {
                    StartCoroutine("GoBackHandPlease");
                }
            }
            else
            {
                StartCoroutine("GoBackHandPlease");
#if UNITY_EDITOR
                Debug.Log("Boss is dead.");
#endif
            }

        }
    }

    private void AttackSlamPlayer()
    {
        if (!isAttacked)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(MoveTheHand(transform, transform.position, player.position));
        }
    }

    // Moving hand to player position!
    private IEnumerator MoveTheHand(Transform obj, Vector3 startPosition, Vector3 endPosition)
    {
        float time = 0f;
        float duration = 2;

        while (time < duration)
        {
            Vector3 lerpPosition = Vector3.Lerp(startPosition, endPosition, time / duration);
            obj.position = lerpPosition;
            time += Time.deltaTime;
            yield return null;
        }

        obj.position = endPosition;
    }

    public void CollisionDetected(HandCollisionTrigger handCollisionTrigger)
    {
        StartCoroutine("GoBackHandPlease");
    }

    private IEnumerator GoBackHandPlease()
    {
        StopCoroutine("MoveTheHand");
        AttacksCounter = 0;
        isAttacked = true;
        animator.SetTrigger("StandBy");
        StartCoroutine(MoveTheHand(transform, transform.position, defaultPosition));
        yield return new WaitForSeconds(10f);
        isAttacked = false;

        if (isBossDead) Death();
    }

    ///<summary>
    /// EVENT TO BE CALLED FROM THE ATTACK ANIMATION
    ///</summary>
    public void Shake()
    {
        CameraShaker.instance.ShakeCamera(0.7f, 6);
        AttacksCounter++;
#if UNITY_EDITOR
        Debug.Log(AttacksCounter);
#endif
    }

    ///<summary>
    /// EVENT TO BE CALLED FROM THE ATTACK ANIMATION
    /// This event activates/deactivates the trigger to avoid the player to get hit if not hit =D
    ///</summary>
    public void TriggerActive(int status)
    {
        if (handTrigger) handTrigger.enabled = status == 1 ? true : false;
    }

    ///<summary>
    /// Will be called when the Skullatron is dead and make it into piece ,then destroy the main GO
    ///</summary>
    private void Death()
    {
        if (deadPieces.Length > 0)
        {
            foreach (GameObject piece in deadPieces)
            {
                piece.GetComponent<MeshCollider>().enabled = true;
                piece.transform.parent = null;
                piece.AddComponent<Rigidbody>();
                piece.GetComponent<Rigidbody>().mass = 15000.0f;
            }
        }

        if (cage) cage.CanBeOpen();
        if (skulllatronHead) skulllatronHead.isDead = true;
        Destroy(this.gameObject);
    }
}
