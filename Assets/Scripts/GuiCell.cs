using UnityEngine;

public class GuiCell : MonoBehaviour
{

    public enum CellState
    {
        Disabled,
        Showing,
        Enabled,
        Hiding
    }

    public const float minclip = 0.1f;
    public const float maxclip = 0.9f;
    public float AnimSpeed = 5f;

    public CellState state = CellState.Hiding;


    void Update()
    {
        switch (state)
        {
            case CellState.Disabled:
                Destroy(gameObject);
                break;

            case CellState.Enabled:
                break;

            case CellState.Showing:
                Show();
                break;

            case CellState.Hiding:
                Hide();
                break;

            default:
                break;

        }

    }


    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        state = CellState.Showing;
    }

    public void Show()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * AnimSpeed);
        if (transform.localScale.x >= maxclip)
        {
            state = CellState.Enabled;
        }
    }

    public void Hide()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * AnimSpeed);
        if (transform.localScale.x <= minclip)
        {
            state = CellState.Disabled;
        }


    }
}
