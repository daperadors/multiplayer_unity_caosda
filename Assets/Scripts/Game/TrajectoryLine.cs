using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    //private LineRenderer m_LineRenderer;
    private LineRenderer m_LineRenderer = new LineRenderer();
    [SerializeField] private Gradient m_Color;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.colorGradient = m_Color;
    }
    [ServerRpc(RequireOwnership = false)]
    public void RenderLineServerRpc(Vector3 startPoint, Vector3 endPoint)
    {
        m_LineRenderer.positionCount = 2;

        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;

        m_LineRenderer.SetPositions(points);
    }
    [ServerRpc(RequireOwnership = false)]
    public void EndLineServerRpc()
    {
        m_LineRenderer.positionCount = 0;
    }
}
