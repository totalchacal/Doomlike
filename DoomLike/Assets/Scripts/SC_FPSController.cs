using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float maxHealth = 50.0f;
    
    public GameObject gameOverPanel;
    public bool isGameOver = false;
    private AudioSource audioSource;
    private AudioSource sfxSource;
    public AudioClip deathSfx;
    public AudioClip death;
    public AudioClip damageTakenSfx;

    public GameObject lifeBar;

    public Texture2D bloodOverlay;
    public float imageStayTime = 0.5f;
    private bool isAppearing = false;
    private bool gettingHit = false;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        sfxSource = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


        StartCoroutine("BloodOverlayAppear");
    }

    void OnGUI() {
        if(isAppearing){
            GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
            GUI.DrawTexture ( new Rect (0,0, Screen.width, Screen.height), bloodOverlay);
        }
    }

    public void LooseLife() {
        lifeBar.GetComponent<Slider>().value -= 1f;
        var fill = lifeBar.transform.GetChild(0).GetComponent<Image>();
        if (fill.color.r < 1)
            fill.color = new Color(fill.color.r + 0.02f, fill.color.g, fill.color.b, fill.color.a);
        else if (fill.color.r >= 1 && fill.color.g > 0)
            fill.color = new Color(fill.color.r, fill.color.g - 0.02f, fill.color.b, fill.color.a);
        if (lifeBar.GetComponent<Slider>().value <= 0)
        {
            GameOver();
        }
    }

    IEnumerator BloodOverlayAppear () {
        while (isAppearing) {
            yield return new WaitForSeconds(imageStayTime);
            isAppearing = false;
        }
        yield return null;
    }


    public void TakeDamage(float amount)
    {
        isAppearing = true;
        sfxSource.PlayOneShot(damageTakenSfx);
        lifeBar.GetComponent<Slider>().value -= amount;
        var fill = lifeBar.transform.GetChild(0).GetComponent<Image>();
        print(lifeBar.GetComponent<Slider>().value/255);
        if (fill.color.r < 1)
            fill.color = new Color(fill.color.r + (lifeBar.GetComponent<Slider>().value/255 * 1.5f), fill.color.g, fill.color.b, fill.color.a);
        if (fill.color.r >= 1 && fill.color.g > 0)
            fill.color = new Color(fill.color.r, fill.color.g - (lifeBar.GetComponent<Slider>().value/255 * 1.5f), fill.color.b, fill.color.a);

        // fill.color = new Color(fill.color.r + (lifeBar.GetComponent<Slider>().value/255), fill.color.g - (lifeBar.GetComponent<Slider>().value/255), fill.color.b, fill.color.a);
        if (lifeBar.GetComponent<Slider>().value <= 0)
        {
            GameOver();
        }
    }

    public void Heal(float amount)
    {
        lifeBar.GetComponent<Slider>().value += amount;
        if (lifeBar.GetComponent<Slider>().value >= maxHealth)
        {
            lifeBar.GetComponent<Slider>().value = maxHealth;
        }
    }

    public void AddHealth(float amount)
    {
        maxHealth += amount;
        Heal(amount);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        audioSource.Stop();
        sfxSource.PlayOneShot(deathSfx);
        gameOverPanel.SetActive(true);
        audioSource.PlayOneShot(death);
        isGameOver = true;
        this.enabled = false;
    }
}
