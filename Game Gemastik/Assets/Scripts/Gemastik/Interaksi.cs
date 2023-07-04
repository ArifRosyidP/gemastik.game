using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interaksi : MonoBehaviour
{
    public GameObject camera, view1, view2, task;
    [SerializeField] bool isInteractable;
    public bool tugas;
    // Start is called before the first frame update
    void Start()
    {
        camera.GetComponent<CameraFollow1>().enabled = true;
        camera.GetComponent<CameraFollow2>().enabled = false;
        tugas= false;
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
                tugas= true;

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
            tugas= false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("bonus ditemukan");
            isInteractable = true;
            //anim.SetBool("open", true);
        }

    }

    private void OnTriggerExit()
    {
        isInteractable = false;

    }

}
