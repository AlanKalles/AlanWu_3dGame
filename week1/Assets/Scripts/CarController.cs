using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject playerModel; // The player's original model
    public GameObject newPlayerModel; // The new player model to swap to

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerInteraction()
    {
        CharacterController playerController = FindObjectOfType<CharacterController>();
        playerController.gameObject.SetActive(false);
        newPlayerModel.SetActive(true);
    }
}
