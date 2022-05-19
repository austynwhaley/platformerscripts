using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Vector3 respawnPosition;

    //called on startup 
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        respawnPosition = PlayerController1.instance.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
        HealthManager.instance.ResetHealth();
    }

    public IEnumerator RespawnCo()
    {
        PlayerController1.instance.gameObject.SetActive(false);
        CameraController.instance.theCMBrain.enabled = false;
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(1f);
        UIManager.instance.fadeFromBlack = true;
        PlayerController1.instance.transform.position = respawnPosition;
        CameraController.instance.theCMBrain.enabled = true;
        PlayerController1.instance.gameObject.SetActive(true);
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("CHECKPOINT BUDDY");
    }
}
