using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullatronHandController : MonoBehaviour {
    public Transform player;
    private Vector3 defaultPosition = new Vector3(19f,2.5999999f,-26.7499981f);
    public Animator animator;
    private bool isAttacked = false;

    void Start() {
         player = GameObject.FindGameObjectWithTag("Player").transform;
         animator = GetComponent<Animator> ();
    }

    void Update() {
        // InvokeRepeating("AttackSlamPlayer", 8f, 8f);
        AttackSlamPlayer();
    }

    private void AttackSlamPlayer() {
        if(!isAttacked) {
            StartCoroutine(MoveTheHand(transform, transform.position, player.position));
        }
    }

    // Moving hand to player position!
    private IEnumerator MoveTheHand(Transform obj, Vector3 startPosition, Vector3 endPosition) {
        float time = 0f;
        float duration = 2;

        while (time < duration) {
            Vector3 lerpPosition = Vector3.Lerp(startPosition, endPosition, time / duration);
            obj.position = lerpPosition;
            time += Time.deltaTime;
            yield return null;
        }

        obj.position = endPosition;
    }

    public void CollisionDetected(HandCollisionTrigger handCollisionTrigger) {
        StartCoroutine("GoBackHandPlease");
    }

    private IEnumerator GoBackHandPlease() {
        isAttacked = true;
        StartCoroutine(MoveTheHand(transform, transform.position, defaultPosition));
        yield return new WaitForSeconds(10f);
        isAttacked = false;
    }
}
