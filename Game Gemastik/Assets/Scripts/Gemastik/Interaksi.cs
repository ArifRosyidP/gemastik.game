using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaksi : MonoBehaviour
{
    public GameObject camera, view1, view2, task, tombol1;
    public bool isInteractable, pause, eksekusiTombol1 = false, kondisiTombol1 = false;
    // Start is called before the first frame update
    void Start()
    {
        camera.GetComponent<CameraFollow1>().enabled = true;
        camera.GetComponent<CameraFollow2>().enabled = false;
    }

    private void Update()
    {
        Task();
        if (isInteractable)
        {
            if (!eksekusiTombol1)
            {
                tombol1.SetActive(true);
                if (kondisiTombol1 == true)
                eksekusiTombol1 = true;
            }
            else
            {
                tombol1.SetActive(false);
            }    
        }
        else
        {
            tombol1.SetActive(false);
        }
    }

    public void komputer()
    {
        kondisiTombol1 = true;
        
        camera.transform.localPosition = view2.transform.position;
        camera.transform.localEulerAngles = view2.transform.eulerAngles;
        camera.GetComponent<Camera>().orthographic = false;
        camera.GetComponent<CameraFollow1>().enabled = false;
        camera.GetComponent<CameraFollow2>().enabled = true;
        task.SetActive(true);
        pause = true;
    }

    private void Task()
    {
        if (Input.GetKey(KeyCode.I))
        {
            if (isInteractable)
            {
                kondisiTombol1 = true;
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
            kondisiTombol1 = false;
            eksekusiTombol1 = false;
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
