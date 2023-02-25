using Unity.Netcode;
using UnityEngine;

public class DragAndShoot : NetworkBehaviour
{
    [SerializeField] private float m_PowerBall = 10f;
    [SerializeField] private float m_Impulse = 2f;
    [SerializeField] private Vector2 m_MinPower;
    [SerializeField] private Vector2 m_MaxPower;

    //TrajectoryLine m_TrajLine;
    private Camera m_Camera;
    private Rigidbody2D m_Rigidbody;
    private NetworkVariable<Vector2> m_Force = new NetworkVariable<Vector2>(/*Vector2.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner*/);
    private Vector3 m_StartPoint;
    private Vector3 m_EndPoint;


    [SerializeField] private Camera _mainCamera;
    public bool _canMove = false;
    [SerializeField] private Transform _spawnPoint;

    void Start()
    {

        BallView.OnBallWhiteEnter += SetSpawnPoint;
     
        Physics2D.gravity = Vector2.zero;
        //m_TrajLine = GetComponent<TrajectoryLine>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Camera = Camera.main;
    }

    public override void OnNetworkSpawn()
    {
        m_Force.OnValueChanged += (Vector2 previousValue, Vector2 newValue) =>
        {
            Debug.Log(OwnerClientId + " new force -> " + newValue);
        };
    }

    void Update()
    {
        MoveBall();
    }


    private void OnMouseDown()
    {
        print("Hola");
        //if (!IsOwner) return;
        if (m_Rigidbody.velocity == Vector2.zero)
        {
            m_StartPoint = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_StartPoint.z = 15;
        }
    }
    private void OnMouseDrag()
    {
        //if (!IsOwner) return;
        if (m_Rigidbody.velocity == Vector2.zero)
        {
            Vector3 currentPoint = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_StartPoint.z = 15;
            // m_TrajLine.Value.RenderLineServerRpc(m_StartPoint, currentPoint);
        }
    }
    private void OnMouseUp()
    {
        //if (!IsOwner) return;
        print("Disparo1");
        DisparoBolaServerRpc();

    }
    [ServerRpc(RequireOwnership = false)]
    private void DisparoBolaServerRpc()
    {
        if (m_Rigidbody.velocity == Vector2.zero)
        {
            m_EndPoint = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_EndPoint.z = 15;

            m_Force.Value = new Vector2(Mathf.Clamp(m_StartPoint.x - m_EndPoint.x, m_MinPower.x, m_MaxPower.x),
                                  Mathf.Clamp(m_StartPoint.y - m_EndPoint.y, m_MinPower.y, m_MaxPower.y));
            m_Rigidbody.AddForce(m_Force.Value * m_PowerBall * m_Impulse, ForceMode2D.Impulse);
            print("Disparo2");
            
            //   m_TrajLine.Value.EndLineServerRpc();
        }
    }
    private void MoveBall() {

        if (_canMove) 
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0f;
                transform.position = worldPosition;
                _canMove = false;
            }
        }


    }

    public void SetSpawnPoint() {
        _canMove = true;
        transform.position = _spawnPoint.position;
        this.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,0f,0f);
    }


}

