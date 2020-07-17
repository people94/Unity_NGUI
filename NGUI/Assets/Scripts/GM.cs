using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    //배경 오브젝트 Set을 정의
    public GameObject _BgSetObj;

    //추가1 배경오브젝트2
    public GameObject _BgSetObj2;

    //이동속도 정의
    public float _moveSpeed;
    public float _moveSpeedMax;

    //얼만큼 이동했는지 체크할 변수 선언
    float _xPosMoveCheck = 0f;
    float _xPosMoveCheck2 = 0f;

    public float _timerLim;
    public float _timerForSpeed;

    //게임 스코어를 표현하기 위한 부분
    public UILabel _Score;
    public int _GameScore;
    public int _GameScorePer;
    
    // Update is called once per frame
    void Update()
    {
        _timerForSpeed += Time.deltaTime;
        if(_timerForSpeed > _timerLim)
        {
            _timerForSpeed = 0;
            if(_moveSpeed < _moveSpeedMax)
            {
                _moveSpeed = _moveSpeed * 1.1f;
            }
        }

        _xPosMoveCheck += _moveSpeed * Time.deltaTime;  //매 루프마다 이동량을 누적하여 저장
        _xPosMoveCheck2 += _moveSpeed * Time.deltaTime * 0.5f;  //매 루프마다 두번째 배경 오브젝트의 이동량을 누적하여 저장

        //매 프레임마다 정해진 속도로 x축 이동
        _BgSetObj.transform.localPosition += new Vector3(_moveSpeed * -0.5f * Time.deltaTime, 0, 0);
        //매 프레임마다 정해진 속도로 x축 이동
        _BgSetObj2.transform.localPosition += new Vector3(_moveSpeed * -1.0f * Time.deltaTime,0, 0);

        if (_xPosMoveCheck > 960.0f) //누적이동량이 960보다 크면 체크
        {
            _xPosMoveCheck = 0;     //누적량 저장 변수 0으로 리셋
            _BgSetObj2.transform.localPosition = new Vector3(960 * -1.0f, 0, 0); //BgSetObj 위치 리셋
            _GameScore += _GameScorePer;    //게임점수를 계산
            _Score.text = _GameScore.ToString();    //게임 점수를 라벨에 표시
        }

        if (_xPosMoveCheck2 >= 960.0f) //누적이동량이 960보다 크면 체크
        {
            _xPosMoveCheck2 = 0;     //누적량 저장 변수 0으로 리셋
            _BgSetObj.transform.localPosition = new Vector3(960 * -1.0f, 0, 0); //BgSetObj 위치 리셋
        }
    }
}
