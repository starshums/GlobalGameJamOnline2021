using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class memoryShard : MonoBehaviour
{

    public Vector3 currPosition;

    [SerializeField] GameObject assetModel;

    [Header("Sin Shard Movement")]
    [SerializeField] float speed = 5f;

    [SerializeField] float magnitude = 1f;

    [SerializeField] float offset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //using sine , make the battery move up and down 
        assetModel.transform.position = currPosition + transform.up * Mathf.Sin(Time.time * speed + offset) * magnitude;
    }
}
