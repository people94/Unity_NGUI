using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    //배경 오브젝트 Set을 정의
    public GameObject _BgSetObj;

    //추가1 배경오브젝트2
    public GameObject _BgSetObj2;

    public GameObject _EnemySet1;
    public List<GameObject> _nowEnemyChild = new List<GameObject>();
    public int _EnemyInt = 0;
    public float _YPos;

    //이동속도 정의
    public float _moveSpeed;
    public float _moveSpeedMax;

    //얼만큼 이동했는지 체크할 변수 선언
    float _xPosMoveCheck = 0f;
    float _xPosMoveCheck2 = 0f;

    public float _xPosMoveCheckVal1 = 0f;
    public float _xPosMoveCheckVal2 = 0f;

    public float _timerLim;
    public float _timerForSpeed;

    //게임 스코어를 표현하기 위한 부분
    public UILabel _Score;
    public float _GameScore;

    public GameObject _ResultUI;
    public UILabel _ResultPoint;
    
    // Update is called once per frame
    void Update()
    {
        SpeedLimCheck();    //난이도 조정을 위하여 속도 빨라지는 구문 설정

        _xPosMoveCheck += _moveSpeed * Time.deltaTime;  //매 루프마다 이동량을 누적하여 저장
        _xPosMoveCheck2 += _moveSpeed * Time.deltaTime * 0.5f;  //매 루프마다 두번째 배경 오브젝트의 이동량을 누적하여 저장

        //매 프레임마다 정해진 속도로 x축 이동
        _BgSetObj.transform.localPosition += new Vector3(_moveSpeed * -0.5f * Time.deltaTime, 0, 0);
        //매 프레임마다 정해진 속도로 x축 이동
        _BgSetObj2.transform.localPosition += new Vector3(_moveSpeed * -1.0f * Time.deltaTime,0, 0);
        _EnemySet1.transform.localPosition += new Vector3(_moveSpeed * -1.0f * Time.deltaTime, 0, 0);   //적 세트 이동

        _GameScore += _moveSpeed * Time.deltaTime * 0.01f;  //이도량에 따른 점수 계산
        _Score.text = _GameScore.ToString("N0");

        if (_xPosMoveCheck > 960.0f) //누적이동량이 960보다 크면 체크
        {
            _xPosMoveCheck = 0;     //누적량 저장 변수 0으로 리셋
            _BgSetObj2.transform.localPosition = new Vector3(960 * -1.0f, 0, 0); //BgSetObj 위치 리셋
            _Score.text = _GameScore.ToString();    //게임 점수를 라벨에 표시
        }

        if (_xPosMoveCheck2 >= 960.0f) //누적이동량이 960보다 크면 체크
        {
            _xPosMoveCheck2 = 0;     //누적량 저장 변수 0으로 리셋
            _BgSetObj.transform.localPosition = new Vector3(960 * -1.0f, 0, 0); //BgSetObj 위치 리셋
        }

        if (_EnemySet1.transform.localPosition.x < _xPosMoveCheckVal1)
        {
            _xPosMoveCheckVal1 -= _xPosMoveCheckVal2;
            ResetChildSet();
            _EnemyInt++;
            if(_EnemyInt > 4)
            {
                _EnemyInt = 0;
            }
        }
    }

    void SpeedLimCheck()
    {
        _timerForSpeed += Time.deltaTime;
        if (_timerForSpeed > _timerLim)
        {
            _timerForSpeed = 0;
            if (_moveSpeed < _moveSpeedMax)
            {
                _moveSpeed = _moveSpeed * 1.05f;
            }
            else
            {
                _moveSpeed = _moveSpeedMax;
            }
        }
    }

    void ResetChildSet()
    {
        _nowEnemyChild[_EnemyInt].transform.localPosition += new Vector3(1440.0f, 0, 0);
        switch(Random.Range(1,4))
        {
            case 1:
                _YPos = 0.0f;
                break;
            case 2:
                _YPos = 100.0f;
                break;
            case 3:
                _YPos = -100.0f;
                break;
        }

        _nowEnemyChild[_EnemyInt].transform.localPosition = new Vector3(_nowEnemyChild[_EnemyInt].transform.localPosition.x, _YPos, _nowEnemyChild[_EnemyInt].transform.localPosition.z);
    }

    //게임이 끝났을때 호출
    public void GameOver()
    {
        Time.timeScale = 0;
        _ResultUI.SetActive(true);
        _ResultPoint.text = _GameScore.ToString("N0");
    }

    //다시하기 버튼을 눌렀을때 호출
    void Regame()
    {
        _ResultUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("gameScene");
    }
}
