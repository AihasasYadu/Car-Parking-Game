using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField] private float accelerationPower,
                                   steeringPower = 5f,
                                   steeringAmount,
                                   speed,
                                   dir;
    [SerializeField] private Button forwardGear,
                                    reverseGear,
                                    brakesButton;
    [SerializeField] private RawImage directionIndicator;
    private int forward = 1,
                direction = 1,
                obstacleLayer = 8;

    private string horizontalAxis = "Horizontal";
    private Rigidbody2D rb;

    public float SetAcceleration { set { accelerationPower = value; 
                                         UpdateFreezeRotation();} }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = 0;
        brakesButton.onClick.AddListener(ApplyBrakes);
        forwardGear.onClick.AddListener(SwitchToForward);
        reverseGear.onClick.AddListener(SwitchToReverse);
        SwitchToForward();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void SwitchToForward()
    {
        ColorBlock color = forwardGear.colors;
        color.normalColor = Color.green;
        forwardGear.colors = color;
        color = reverseGear.colors;
        color.normalColor = Color.white;
        reverseGear.colors = color;
        direction = 1;
        iTween.MoveTo(directionIndicator.gameObject, iTween.Hash("y", 30, 
            "time", 0.2f,
            "islocal", true, 
            "easetype", iTween.EaseType.linear));
    }
    
    private void SwitchToReverse()
    {
        ColorBlock color = forwardGear.colors;
        color.normalColor = Color.white;
        forwardGear.colors = color;
        color = reverseGear.colors;
        color.normalColor = Color.green;
        reverseGear.colors = color;
        direction = -1;
        iTween.MoveTo(directionIndicator.gameObject, iTween.Hash("y", -30,
            "time", 0.2f,
            "islocal", true,
            "easetype", iTween.EaseType.linear));
    }

    private void ApplyBrakes()
    {
        accelerationPower = 0;
        rb.freezeRotation = true;
    }

    private void UpdateFreezeRotation()
    {
        if(accelerationPower > 0)
            rb.freezeRotation = false;
        else
            rb.freezeRotation = true;
    }

    private void Movement()
    {
        steeringAmount = -1 * SimpleInput.GetAxis(horizontalAxis);
        speed = direction * accelerationPower;
        dir = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * dir;

        rb.AddRelativeForce(Vector2.up * speed);

        rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * steeringAmount / 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer.Equals(obstacleLayer))
        {
            GameManager.Instance.SetScore = ResultsEnum.GameOver;
        }
    }
}
