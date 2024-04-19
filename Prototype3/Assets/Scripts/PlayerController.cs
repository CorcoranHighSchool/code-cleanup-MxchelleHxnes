using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The player's Rigidbody
    private Rigidbody playerRb;
    //Jump force
    private float jumpForce = 15.0f;
    //Gravity Modifier
    [SerializeField] private float gravityModifier;
    //Are we on the ground?
    private bool isOnGround = true;
    //Is the Game Over
    public bool gameOver {get; private set;};
    private const string Jump_trig = "Jump_trig"
    private const sting Ground = "Ground"
    private const string Obstacle = "Obstacle"
    private const string Game over = "Game Over"
    private const string Death_b = "Death_b"
    private const string DeathType_int = "DeathType_int"


    //Player Animator
    private Animator playerAnim;

    //ParticleSystem explosion
    [SerializeField] private ParticleSystem explositionParticle;
    //ParticleSystem dirt
    [SerializeField] private ParticleSystem dirtParticle;

    //Jump sound
    [SerializeField] private AudioClip jumpSound;
    //Crash sound
    [SerializeField] private AudioClip crashSound;
    //Player Audio
    [SerializeField] private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody
        playerRb = GetComponent<Rigidbody>();
        //Connect the Animator
        playerAnim = GetComponent<Animator>();
        //Player Audio
        //playerAudio.GetComponent<AudioSource>();
        //update the gravity
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //If we press space, jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
            //trigger the jump animation
            playerAnim.SetTrigger(Jump_trig);         
            isOnGround = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Ground)) 
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag(Obstacle)) 
        {
            explositionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);

            gameOver = true;
            Debug.Log(Game Over!);                    
            playerAnim.SetBool(Death_b, true);        
            playerAnim.SetInteger(DeathType_int, 1);  
        }
    }
}
