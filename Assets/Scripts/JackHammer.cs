using UnityEngine;
using UnityEngine.Events;

public class JackHammer : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private JackHammerAnimations jackHammerAnimations;
    [Header("Jackhammer Positioning")] 
    [SerializeField] private float initialZPosition;
    [SerializeField] private float targetZPosition;
    [SerializeField] private bool useInitialCameraDistance;
    [Header("Particles")] [SerializeField] private ParticleSystem dustCloud;
    [SerializeField] private Transform jackHammerTip;

    private UnityEvent leftClickPressed = new UnityEvent();
    private UnityEvent leftClickReleased = new UnityEvent();

    private float actualDistance;
    private bool updatePosition;

    private void Awake()
    {
        leftClickPressed.AddListener(OnLeftClickPressed);
        leftClickReleased.AddListener(OnLeftClickReleased);
    }

    private void Start()
    {
        if (useInitialCameraDistance)
        {
            var toObjectVector = transform.position - Camera.main.transform.position;
            var linearDistanceVector = Vector3.Project(toObjectVector, Camera.main.transform.forward);
            actualDistance = linearDistanceVector.magnitude;
        }
        else
        {
            actualDistance = targetZPosition;
        }
    }

    private void OnLeftClickPressed()
    {
        jackHammerAnimations.StartShaking();
        updatePosition = true;
        ShowDustCloud(true);
        dustCloud.Play();
    }

    private void OnLeftClickReleased()
    {
        updatePosition = false;
        jackHammerAnimations.StopShaking();
        transform.position.Set(transform.position.x, transform.position.y, initialZPosition);
        ShowDustCloud(false);
        dustCloud.Stop();
    }

    private void ShowDustCloud(bool state)
    {
        dustCloud.Stop();
        var dustCloudMain = dustCloud.main;
        dustCloudMain.loop = state;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) leftClickReleased.Invoke();
        if (Input.GetMouseButtonDown(0)) leftClickPressed.Invoke();

        if (!updatePosition) return;

        var mousePosition = Input.mousePosition;
        mousePosition.z = actualDistance;
        var targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, targetZPosition);
        Instantiate(dustCloud, transform.TransformPoint(jackHammerTip.localPosition), Quaternion.identity);
    }
}