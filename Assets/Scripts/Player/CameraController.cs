using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    public Transform cameraPivot;

    public float mouseSens = 150;
    public float smoothTime = 0.2f;
    public float maxY;
    public float minY;
    public float rotationSpeed = 5f;
    public bool isMoving = false;
    public Transform target;
    private float yRotation = 0f;
    private float xRotation = 0f;
    private Vector3 lastCameraPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // para que desaparezca el cursor
        lastCameraPosition = cameraPivot.position;
    }

    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying || Inventory.Instance.inventoryOnScreen || GameManager.instance.menuReference.activeInHierarchy || ContrellersPanel.Instance.controllerPanelActive)
        {
            Cursor.lockState = CursorLockMode.None; // para que aparezca el cursor en los dialogos
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (player.isWalking || player.isRunning)

        {

            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            yRotation -= mouseY; // el menos es importante
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(yRotation, 0, 0), 1f);
            
            player.transform.Rotate(Vector3.up * mouseX); //movimiento horizontal 

            Vector3 startVector = transform.localPosition; // Vector actual de la c�mara
            Vector3 endVector = new Vector3(1f, 1.49f, -4.25f);
            float t = 0.1f;

            Vector3 lerpedVector = Vector3.Lerp(startVector, endVector, t);
            transform.localPosition = lerpedVector;
            isMoving = true;
        }
        else
        {
            isMoving = false;
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            yRotation -= mouseY; // el menos es importante
            xRotation += mouseX; // el menos es importante

            yRotation = Mathf.Clamp(yRotation, minY, maxY); // l�mites para yRotation
            //Quaternion camTargetRotation = Quaternion.Euler(yRotation, xRotation, 0); // Usar ambas rotaciones
            //transform.localRotation = Quaternion.Lerp(transform.localRotation, camTargetRotation, 1f);
            cameraPivot.RotateAround(player.transform.position, Vector3.up, mouseX * rotationSpeed);
            cameraPivot.rotation = Quaternion.Euler(yRotation, cameraPivot.rotation.eulerAngles.y, 0);
        }

        if (player.isWalking || player.isRunning)
        {
            transform.LookAt(player.transform);
            isMoving = true;
        }

        if (!player.isWalking || !player.isRunning)
        {
            transform.LookAt(target.transform);
            isMoving = false;
            //AudioManager.Instance.PlayAudio(clip.name);
        }

        ChangeBoolState();

        if (yRotation >= maxY)
        {
            yRotation = maxY;
        }
        if (yRotation <= minY)
        {
            yRotation = minY;
        }
    }
    void ChangeBoolState()
    {
        isMoving = !isMoving; //comprueba en que estado se encuentra el bool y lo cambia al pulsar la tecla

        // Iniciar el movimiento suave de la c�mara hacia la �ltima posici�n cuando el jugador deja de moverse
        cameraPivot.position = Vector3.SmoothDamp(cameraPivot.position, lastCameraPosition, ref currentVelocity, smoothTime);
        // Actualizar la �ltima posici�n de la c�mara cuando el jugador comienza a moverse
        lastCameraPosition = cameraPivot.position;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}