using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastInteraction : MonoBehaviour
{
    public Transform rayOrigin;
    public float rayLenght=3;
    public LayerMask layer;
    public GameObject uiGO;
    public Text hintText;
    public float hintTime;
    public string defaultHint;
    private bool isMessageShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        defaultHint = "Presione E para abrir ";
        hintText.text = defaultHint;
        uiGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        InteractableObject interactable = null;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLenght, layer))
        {            
            interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable)
            {
                if (!isMessageShowing)
                {
                    StartCoroutine(ShowHintDuringTime(defaultHint + interactable.gameObject.transform.parent.gameObject.name));
                }
            }
        }
        uiGO.SetActive(interactable);
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            string message = "";
            if (!interactable.activated)
            {
                interactable.PlayObjectAnimation();
                message = "Abriste " + interactable.gameObject.transform.parent.gameObject.name;
            }
            else
            {
                message = interactable.gameObject.transform.parent.gameObject.name + " ya esta abierto";
            }
            StopAllCoroutines();
            StartCoroutine(ShowHintDuringTime(message));
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * rayLenght);
    }

    IEnumerator ShowHintDuringTime(string hintMessage)
    {
        isMessageShowing = true;
        float time = 0;
        hintText.text = hintMessage;
        while (time < hintTime)
        {
            time += Time.deltaTime;
            uiGO.SetActive(true);
            yield return null;
        }
        uiGO.SetActive(false);
        hintText.text = defaultHint;
        isMessageShowing = false;
    }

}
