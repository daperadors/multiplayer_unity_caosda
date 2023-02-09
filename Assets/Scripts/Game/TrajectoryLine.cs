using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : NetworkBehaviour
{
    //private LineRenderer m_LineRenderer;
    private NetworkVariable<LineRenderer> m_LineRenderer = new NetworkVariable<LineRenderer>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private Gradient m_Color;

    private void Awake()
    {
        m_LineRenderer = GetComponent<NetworkVariable<LineRenderer>>();
        m_LineRenderer.Value.colorGradient = m_Color;
    }
    [ServerRpc(RequireOwnership = false)]
    public void RenderLineServerRpc(Vector3 startPoint, Vector3 endPoint)
    {
        m_LineRenderer.Value.positionCount = 2;

        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;

        m_LineRenderer.Value.SetPositions(points);
    }
    [ServerRpc(RequireOwnership = false)]
    public void EndLineServerRpc()
    {
        m_LineRenderer.Value.positionCount = 0;
    }
}
