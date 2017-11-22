using UnityEngine;

public class Cursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public GameObject FocusedObject { get; private set; }

    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        var headRotation = Camera.main.transform.rotation;

        RaycastHit hitInfo;

        Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        #region Keyboard Controls for testing

        // head movement
        if (Input.GetKeyDown(KeyCode.A))
        {
            Camera.main.transform.position = headPosition - new Vector3(0.2f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Camera.main.transform.position = headPosition + new Vector3(0.2f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Camera.main.transform.position = headPosition - new Vector3(0.0f, 0.0f,0.2f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Camera.main.transform.position = headPosition + new Vector3(0.0f, 0.0f,0.2f);
        }

        // head rotation
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.main.transform.rotation = new Quaternion(headRotation.x, headRotation.y - 0.01f, headRotation.z, headRotation.w);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Camera.main.transform.rotation = new Quaternion(headRotation.x, headRotation.y + 0.01f, headRotation.z, headRotation.w);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.rotation = new Quaternion(headRotation.x - 0.01f, headRotation.y, headRotation.z, headRotation.w);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Camera.main.transform.rotation = new Quaternion(headRotation.x + 0.01f, headRotation.y, headRotation.z, headRotation.w);
        }

        

        #endregion


        if (Physics.Raycast(MouseRay, out hitInfo))      // (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram...
            // Display the cursor mesh.
            meshRenderer.enabled = true;

            // Move thecursor to the point where the raycast hit.
            this.transform.position = hitInfo.point;

            // Rotate the cursor to hug the surface of the hologram.
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            // Get the newly focused object
            var newFocusedObject = hitInfo.collider.gameObject;

            // If user is gazing at a new object, reset the old one
            if (FocusedObject != null && newFocusedObject != FocusedObject)
            {
                FocusedObject.SendMessage("OnReset");
            }

            // Store the newly focused object and set the "selected" colour
            FocusedObject = newFocusedObject;
            FocusedObject.SendMessage("OnSelect");
        }
        else
        {
            // If the raycast did not hit a hologram, hide the cursor mesh.
            meshRenderer.enabled = false;

            // Reset the colour of the cube
            if (FocusedObject != null)
            {
                FocusedObject.SendMessage("OnReset");
            }
            FocusedObject = null;

        }
    }
}
