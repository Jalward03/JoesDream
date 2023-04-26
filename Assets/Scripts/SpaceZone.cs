using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

using WipeOut;

public class SpaceZone : MonoBehaviour
{
    public GameObject blackHole;
    public GameObject ragdoll;
    public GameObject player;
        
    
    [SerializeField] CameraController cameraController;
    private float delay = 3.0f;
    private bool blackHoleCanStart;
    private bool canGetUp;


    private IEnumerator SwitchRagoll()
    {
        ragdoll.SetActive(true);

        yield return new WaitForSeconds(4.0f);

        canGetUp = true;
    }

    private IEnumerator StartBlackHole()
    {
        delay = 1000f;
        blackHoleCanStart = true;
        float t = 0;
        float growTime = 1f;
        Vector3 startScale = blackHole.transform.localScale;
        Vector3 endScale = new Vector3(50,50,50);

        while(t < growTime * 1.1f)
        {
            blackHole.transform.localScale = Vector3.Lerp(startScale, endScale, t / growTime);

            yield return null;

            t += Time.deltaTime;
        }

        StartCoroutine(SwitchRagoll());
    }
    void Awake()
    {
        cameraController.canTurn = false;
        cameraController.canLookAtPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!blackHoleCanStart)
        {
            delay -= Time.fixedDeltaTime;

            if(delay < 0.0f)
            {
                StartCoroutine(StartBlackHole());
            }
        }

        if(canGetUp)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                ragdoll.SetActive(false);
                player.SetActive(true);
                cameraController.canTurn = true;
                cameraController.canLookAtPlayer = true;
            }
        }
 
       
    }
}
