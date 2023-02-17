using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillarView : MonoBehaviour
{

    public GameObject[] _redBallsImage;
    public GameObject[] _yellowBallsImage;
    public int _quantityRedBall = 0;
    public int _quantityYellowBall = 0;


    void Start()
    {
        InitializeBallsImage();
    }


    private void InitializeBallsImage()
    {

        int iterador = _redBallsImage.Length;

        for (int x = 0; x < iterador; x++)
        {
            _redBallsImage[x].SetActive(false);
            _yellowBallsImage[x].SetActive(false);
        }

    }


    public void ActiveBalls(int id)
    {

        if (id == 1)
        {
            _quantityRedBall++;

            if (_quantityRedBall < _redBallsImage.Length)
            {
                for (int x = 0; x < _quantityRedBall; x++)
                {
                    _redBallsImage[x].SetActive(true);
                }
            }
        }


        else if (id == 2)
        {
            _quantityYellowBall++;

            if (_quantityYellowBall < _yellowBallsImage.Length)
            {
                for (int x = 0; x < _quantityYellowBall; x++)
                {
                    _yellowBallsImage[x].SetActive(true);
                }
            }
        }
        


    }

    public void ComproveVictory()
    {
        int ballsMax = _redBallsImage.Length;
        if (_quantityRedBall == ballsMax || _quantityYellowBall == ballsMax)
        {
            Debug.Log("MI TURNO WIN");
        }
        else
        {
            Debug.Log("OTRO TURNO WIN");
        }
    }


}
