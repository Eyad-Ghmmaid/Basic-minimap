using UnityEngine;
using StarterAssets;

public class bigMapManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerController;

    [Header("Big Map Settings")]
    [SerializeField] private Camera bigMapCamera;
    [SerializeField] private GameObject bigMapUI;
    [SerializeField] private KeyCode toggleKey = KeyCode.M;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float maxZoom = 100f;
    
    [Header("Camera Movement Settings")]
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private int dragMouseButton = 0; // 0 = Left, 1 = Right, 2 = Middle
    
    private bool isBigMapActive = false;
    private Vector3 lastMousePosition;
    private Transform playerTransform;
    private Quaternion fixedRotation;
    private Vector3 cameraOffset;
    private bool wasCameraChild = false;
    private ThirdPersonController playerController;
    private StarterAssetsInputs starterInputs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (bigMapCamera != null)
        {
            // Spieler Transform speichern
            if (bigMapCamera.transform.parent != null)
            {
                playerTransform = bigMapCamera.transform.parent;
                wasCameraChild = true;
                cameraOffset = bigMapCamera.transform.localPosition;
            }
            else if (PlayerController != null)
            {
                playerTransform = PlayerController.transform;
            }
            
            // Feste Rotation speichern (normalerweise von oben nach unten)
            fixedRotation = bigMapCamera.transform.rotation;
            
            // Kamera sofort vom Spieler trennen
            bigMapCamera.transform.SetParent(null);
        }
        
        // PlayerController und Input Script finden
        if (PlayerController != null)
        {
            playerController = PlayerController.GetComponent<ThirdPersonController>();
            starterInputs = PlayerController.GetComponent<StarterAssetsInputs>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleBigMap();
        }
        
        // Kamera-Rotation immer fixieren
        if (bigMapCamera != null)
        {
            bigMapCamera.transform.rotation = fixedRotation;
        }
        
        if (isBigMapActive)
        {
            Cameradistance();
            HandleCameraDrag();
        }
        else
        {
            // Wenn Big Map nicht aktiv ist, Kamera Position mit Spieler synchronisieren
            if (playerTransform != null && bigMapCamera != null)
            {
                bigMapCamera.transform.position = playerTransform.position + playerTransform.TransformDirection(cameraOffset);
            }
        }
    }

    public void ToggleBigMap()
    {
        isBigMapActive = !isBigMapActive;
        bigMapCamera.gameObject.SetActive(isBigMapActive);
        bigMapUI.SetActive(isBigMapActive);
        
        if (isBigMapActive)
        {
            // Spieler-Steuerung deaktivieren (Controller + Input) und Eingaben zurücksetzen
            if (playerController != null)
            {
                playerController.enabled = false;
            }
            if (starterInputs != null)
            {
                // Sicherstellen, dass ein gedrückter Sprung nicht hängen bleibt
                starterInputs.jump = false;
                starterInputs.enabled = false;
            }
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // Spieler-Steuerung wieder aktivieren (Controller + Input)
            if (playerController != null)
            {
                playerController.enabled = true;
            }
            if (starterInputs != null)
            {
                starterInputs.enabled = true;
            }
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            // Position zurücksetzen zur Spieler-Position
            if (playerTransform != null)
            {
                bigMapCamera.transform.position = playerTransform.position + playerTransform.TransformDirection(cameraOffset);
            }
        }
    }
    
    public void CenterOnPlayer()
    {
        if (isBigMapActive && playerTransform != null)
        {
            Vector3 playerPos = playerTransform.position;
            bigMapCamera.transform.position = new Vector3(playerPos.x, bigMapCamera.transform.position.y, playerPos.z);
        }
    }

    public void Cameradistance()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0f)
        {
            if (bigMapCamera.orthographic)
            {
                bigMapCamera.orthographicSize -= scrollInput * zoomSpeed;
                bigMapCamera.orthographicSize = Mathf.Clamp(bigMapCamera.orthographicSize, minZoom, maxZoom);
            }
            else
            {
                bigMapCamera.fieldOfView -= scrollInput * zoomSpeed;
                bigMapCamera.fieldOfView = Mathf.Clamp(bigMapCamera.fieldOfView, minZoom, maxZoom);
            }
        }
    }
    
    private void HandleCameraDrag()
    {
        if (Input.GetMouseButtonDown(dragMouseButton))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(dragMouseButton))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            
            // Bewegung anpassen basierend auf Kamera-Typ und Zoom-Level
            float moveScale = dragSpeed * Time.deltaTime;
            if (bigMapCamera.orthographic)
            {
                moveScale *= bigMapCamera.orthographicSize * 0.01f;
            }
            else
            {
                moveScale *= bigMapCamera.fieldOfView * 0.01f;
            }
            
            Vector3 move = new Vector3(-delta.x * moveScale, 0, -delta.y * moveScale);
            bigMapCamera.transform.Translate(move, Space.World);
            
            lastMousePosition = Input.mousePosition;
        }
    }
}
