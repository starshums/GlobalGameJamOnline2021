using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossTrigger : MonoBehaviour
{
    [SerializeField] AudioClip begin;
    [SerializeField] AudioClip letsFight;

    void Start() => GetComponent<AudioSource>().PlayOneShot(begin);

    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
#if UNITY_EDITOR
        Debug.Log("Boss Fight Starts");
#endif  
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(letsFight);
        GetComponent<AudioSource>().loop = true;
        GameObject.FindObjectOfType<SkullatronHandController>().isBossFightStarted = true;
    }
}
