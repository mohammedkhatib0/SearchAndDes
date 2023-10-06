using Photon.Pun;
using scgFullBodyController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEngine : MonoBehaviour
{
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    public float sprintSpeed;
    public float walkSpeed;
    public float crouchSpeed;
    [HideInInspector] public bool slide;
    public float slideTime;
    bool crouchToggle = false;
    bool proneToggle = false;
    bool crouch = false;
    bool prone = false;
    bool sprint = false;
    bool canVault = false;
    bool vaulting = false;
    bool strafe;
    bool forwards;
    bool backwards;
    bool right;
    bool left;
    public float vaultCancelTime;
    float horizontalInput;
    float verticalInput;
    public float sensitivity;
    public GameObject cameraController;
    GameObject collidingObj;

    //Amurii edir
    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
        if (!PV.IsMine)
            Destroy(GetComponentInChildren<Camera>().gameObject);
        // Ping = GameObject.FindGameObjectWithTag("hud").GetComponent<>;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
