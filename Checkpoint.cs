using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject cpOn, cpOff;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.SetSpawnPoint(transform.position);


            Checkpoint[] allcp = FindObjectsOfType<Checkpoint>();
            for(int i = 0; i < allcp.Length; i++)
            {
                allcp[i].cpOff.SetActive(true);
                allcp[i].cpOn.SetActive(false);
            }

            cpOff.SetActive(false);
            cpOn.SetActive(true);
        }
    }
}
