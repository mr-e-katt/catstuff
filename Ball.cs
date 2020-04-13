using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config Parameters
    [SerializeField] Paddle paddle1; // initializes variable paddle1
    [SerializeField] float xVelocity = 2f; // initializes variable xVelocity and sets it equal to 2f
    [SerializeField] float yVelocity = 7f; // initializaes variable yVelocity and sets it equal to 7f
    [SerializeField] AudioClip[] ballsounds; // initializes array ballsounds
    [SerializeField] float randomFactor;

    // state
    Vector2 paddleToBallVector; // initializes variable paddleToBallVector
    bool hasStarted = false; // initializes variable hasStarted and sets it equal to false

    // Cached component references
    AudioSource myAudioSource; // initializes variable myAudioSource;
    Rigidbody2D myRigideBody;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position; // sets paddleToBallVector equal to position of the Ball minus the position of the paddle
        myAudioSource = GetComponent<AudioSource>(); // sets variable myAudioSource equal to any component within AudioSource
        myRigideBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBalltoPaddle(); // calls LockBalltoPaddle() function
            LaunchBallOnMouseClick(); // calls LaunchOnMuseClick() function
        }
        
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // if primary mouse clicked down, do...
        {
            hasStarted = true; // sets bool hasStarted to true
            myRigideBody.velocity = new Vector2(xVelocity, yVelocity); // calls Rigid Body velocity component and allows you to adjust x and y velocity (in this case xVelocity and yVelocity are the values for x and y)
        }

    }

    private void LockBalltoPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y); //sets variable paddlePos equal to x and y coordiantes of paddle1
        transform.position = paddlePos + paddleToBallVector; // transforms position of the Ball object to equal paddlePos
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if (hasStarted) //if hasStarted equals true...
        {
            AudioClip variableClip = ballsounds[UnityEngine.Random.Range(0, ballsounds.Length)]; // initializes variable variableClip and sets it equal to a Random sound in the ballsounds array
            myAudioSource.PlayOneShot(variableClip); // plays audio clip pulled in from Audio Source component AudioClip
            myRigideBody.velocity += velocityTweak;
        }
    }
}
