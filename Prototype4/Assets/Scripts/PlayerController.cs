using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerialieField] private float speed = 5.0f;
    [SerialieField] private GameObject focalPoint;
    [SerialieField] private bool hasPowerup;
    private float powerUpStrength = 15.0f;
    [SerialieField] private GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point"); //TODO: String
    }

    // Update is called once per frame
    void Update()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        float verticalInput = Input.GetAxis("Vertical");        //TODO: String
        playerRb.AddForce(focalPoint.transform.forward * (speed * verticalInput)); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))                    //TODO: String
        {
            powerupIndicator.SetActive(true);   
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    private IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup) //TODO: String
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + //TODO: String
                "with powerup set to " + hasPowerup);               //TODO: String
            enemyRigidbody.AddForce(awayfromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
}
