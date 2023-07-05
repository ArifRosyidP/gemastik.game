using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaksi : MonoBehaviour
{
    public GameObject camera, view1, view2, task;
    public bool isInteractable, pause;
    // Start is called before the first frame update
    void Start()
    {
        camera.GetComponent<CameraFollow1>().enabled = true;
        camera.GetComponent<CameraFollow2>().enabled = false;
    }

    private void Update()
    {
        Task();
    }

    private void Task()
    {
        if (Input.GetKey(KeyCode.I))
        {
            if (isInteractable)
            {
                camera.transform.localPosition = view2.transform.position;
                camera.transform.localEulerAngles = view2.transform.eulerAngles;
                camera.GetComponent<Camera>().orthographic = false;
                camera.GetComponent<CameraFollow1>().enabled = false;
                camera.GetComponent<CameraFollow2>().enabled = true;
                task.SetActive(true);
                pause = true;
            }
            
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            camera.transform.position = view1.transform.position;
            camera.transform.eulerAngles = view1.transform.eulerAngles;
            camera.GetComponent<Camera>().orthographic = true;
            camera.GetComponent<CameraFollow1>().enabled = true;
            camera.GetComponent<CameraFollow2>().enabled = false;
            task.SetActive(false);
            pause = false;
        }

    }

    private void OnTriggerStay(Collider collider)
    {
        Debug.Log("Test");
        if (collider.gameObject.tag == "Interaksi")
        {
            isInteractable = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Interaksi")
        {
            isInteractable = false;
        }
    }

}
