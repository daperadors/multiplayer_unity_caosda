using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallView : MonoBehaviour
{

    [SerializeField] private BillarView _billarController;

    public delegate void BallWhite();
    public static BallWhite OnBallWhiteEnter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Agujero")) {

            if (gameObject.tag.Equals("PoolBallRed"))
            {
                _billarController.ActiveBalls(1);
                Debug.Log("BallRed");
                Destroy(gameObject);
            }
            else if (gameObject.tag.Equals("PoolBallYellow"))
            {
                _billarController.ActiveBalls(2);
                Debug.Log("BallYellow");
                Destroy(gameObject);
            }
            else if (gameObject.tag.Equals("PoolBall"))
            {
                OnBallWhiteEnter?.Invoke();
            }
            else if (gameObject.tag.Equals("PoolBallBlack")) {
                _billarController.ComproveVictory();
                Destroy(gameObject);
            }


        }
    }
   


}
