using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool playerInTrigger;

    private void Awake()
    {
        playerInTrigger = false;
    }

    private void Update()
    {
        if(playerInTrigger)
        {
            this.gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        if (playerInTrigger)
        {
            InternDialogue.Instance().EnterDialogueMode(inkJSON);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            playerInTrigger = false;
        }
    }
}