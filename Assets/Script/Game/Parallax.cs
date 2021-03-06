using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScale;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPos = cam.position;

        parallaxScale = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScale[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScale[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

        }

        previousCamPos = cam.position;
    }


    /*
    public Transform mainCam;
    public Transform middleBG;
    public Transform sideBG;

    public float length = 30.2f;

    // Update is called once per frame
    void Update()
    {
        if (mainCam.position.x > middleBG.position.x)
            sideBG.position = middleBG.position + Vector3.right * length;

        if(mainCam.position.x < middleBG.position.x)
            sideBG.position = middleBG.position + Vector3.left * length;

        if (mainCam.position.x > sideBG.position.x || mainCam.position.x < sideBG.position.x)
        {
            Transform z = middleBG;
            middleBG = sideBG;
            sideBG = z;
        }
            
    }
    */
}
