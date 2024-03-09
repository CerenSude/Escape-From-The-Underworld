using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody Rigid;
    AudioSource Audio;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float horizontalThrust = 500f;
    [SerializeField] AudioClip wingFlap; //we're adding a variable for adding audio
    [SerializeField] ParticleSystem pFlap;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private bool turnedLeft = false;
    
    private void Start()
    {
        Rigid = GetComponent<Rigidbody>();
        Audio = GetComponent<AudioSource>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        processThrust();
        processHorizontal();
    }

    void processThrust(){
        if (Input.GetKey(KeyCode.Space))
        {
            StartFlying();
        }
        else
        {
            StopFlying();
        }
    }
    
    void StartFlying()
    {
        Rigid.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        skinnedMeshRenderer.SetBlendShapeWeight(0, 50);    
        if (!Audio.isPlaying){
            Audio.PlayOneShot(wingFlap); 
        }

        if (!pFlap.isPlaying){
            pFlap.Play();
        }
    }
    
    void StopFlying()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(0,0); //to close the wings
        Audio.Stop();
        pFlap.Stop();
    }

    void processHorizontal()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            moveLeftRight(horizontalThrust);
            if (!turnedLeft)
            {
                transform.Rotate(Vector3.up*180);
                turnedLeft = true;
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            moveLeftRight(-horizontalThrust);
            if (turnedLeft)
            {
                transform.Rotate(Vector3.up*180);
                turnedLeft = false;
            }
        }
    }
    
    void moveLeftRight(float rotationThisFrame)
    {
        skinnedMeshRenderer.SetBlendShapeWeight(0,100); //to open the wings
        Rigid.AddForce(Vector3.left * rotationThisFrame * Time.deltaTime);
    }
}
