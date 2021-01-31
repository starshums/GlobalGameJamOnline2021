using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HamsCaDogReleaser : MonoBehaviour
{
    public string FinalScene = "Scene4_Final";

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(FinalScene, LoadSceneMode.Single);
        }
    }
}
