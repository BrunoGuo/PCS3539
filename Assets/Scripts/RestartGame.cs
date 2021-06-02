using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "Finish") {
            SceneManager.LoadScene("Main");
        }
    }

}
