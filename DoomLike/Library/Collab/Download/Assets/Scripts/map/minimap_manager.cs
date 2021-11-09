using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap_manager : MonoBehaviour
{
    public GameObject player;
    public GameObject map_up;
    public GameObject map_down;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(player.transform.position.y);
        if(player.transform.position.y > 4){
            map_up.SetActive(true);
            map_down.SetActive(false);
        }
        else{
            map_up.SetActive(false);
            map_down.SetActive(true);
        }
    }
}
