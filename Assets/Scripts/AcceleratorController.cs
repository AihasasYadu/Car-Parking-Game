using UnityEngine;
using UnityEngine.EventSystems;

public class AcceleratorController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CarController carProps;
    private const float POWER = 2.5f;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        carProps.SetAcceleration = POWER;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        carProps.SetAcceleration = 0;
    }
}
