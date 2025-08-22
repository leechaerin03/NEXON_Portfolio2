using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData() 
    {
        talkData.Add(1, new string[] { "드디어 입사 첫날이다...!", "음 근데 뭘 해야하지?", "일단 부장님께 가보자!" });
        //대화 데이터
        talkData.Add(1000, new string[] { "자네가 이번에 입사한 친구구만?", "할 일은 다 하고 나에게 말을 거는건가?","어서 가서 일하게 !" });
        talkData.Add(2000, new string[] { "지금은 할 일이 없네...부장님께 가보자" });

        //퀘스트 대화 데이터
        talkData.Add(10 + 1000, new string[] {"흠...그래 자네의 첫 업무를 주어야지" , "면접 때 분명 Git에 대해 잘 안다고 했었지?", "마침 우리 회사가 새로운 프로젝트를 진행하려고 하는데", "git으로 작업할 저장소랑 파일 만들어오게나" });
        talkData.Add(11 + 2000, new string[] { "아...큰일났다...", "면접 땐 자신 있게 할 줄 안다고 했지만 난 Git에 대해 아무것도 모르는데...", "분명 들키면 해고당할게 분명해....","부장님에게 들키지 않도록 Git을 독학하자 !" });

        talkData.Add(20 + 1000, new string[] { "오호~ 좋아 정사원 아주 일을 잘하네~", "흠..근데 우리 회사엔 직원이 많은데 말이지...", "같은 곳에서 작업하면 안되지 않나?", "서로 다른 곳에서 작업하고 한번에 합칠 수 있는 방법 좀 알아와주게나" });
        talkData.Add(21 + 2000, new string[] { "휴... 첫임무는 겨우 넘겼네", "Git 독학 할만한걸?!", "바로 다음 업무도 빨리해보자!", "음... 아! Branch를 이용하면 되겠는데?" });

        talkData.Add(30 + 1000, new string[] { "자네 세상에서 제일 뜨거운 과일이 뭔지 아는가?", "바로 천도복숭아 ! 하하 재밌지?", "아 정사원 근데 내가 말이야 옆 부서 박부장이랑 같이 업무를 했는데...", "merge가 안돼... 박부장 말로는 뭐 ‘salary’단어 뒤쪽만 고쳤다는데 오류가 나네?", "왤까? 당장 알아오게나" });
        talkData.Add(31 + 2000, new string[] { "어휴 부장님 개그...재미없어서 혼났네", "정신 바짝 차리고 다음 업무 해보자!" });

        talkData.Add(40 + 1000, new string[] { "오 그래그래 그게 문제였군...", "벌써 마지막 업무라네...", "지금 내가 한 업무들을 원격저장소에 Push하고 이걸 자네 컴퓨터에 Pull하는 작업 해주게", "오늘 오후까지야!" });
        talkData.Add(41 + 2000, new string[] { "마지막이라 그런가...어렵네", "음 다시 되새겨보자 부장님이 주신 파일을 원격저장소에 push하고","이걸 내 컴퓨터 브런치에서 reaf노드에 pull하면 되겠지?","해보자!" });

        talkData.Add(50 + 1000, new string[] { "자네! 정말 GIT을 잘하는구만", "자네를 뽑길 잘했어! 허허", "앞으로도 이렇게 일해주게나 이제 자네는 정부장이야 정부장!", "앞으로도 이렇게 일해주게 정부장!!" });
        talkData.Add(51 + 2000, new string[] { "내가 벌써 부장이라니...", "앞으로도 열심히 GIT을 공부하자!" });
    }

    public string GetTalk(int id, int talkIndex) 
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //기본 대사 예외처리
                if (talkIndex == talkData[id - id % 100].Length)
                    return null;
                else
                    return talkData[id - id % 100][talkIndex];
            }
            else 
            {
                //퀘스트 진행중 대사가 없을때, 퀘스트 맨 처음 대사 끌고오기
                if (talkIndex == talkData[id - id % 10].Length)
                    return null;
                else
                    return talkData[id - id % 10][talkIndex];
            }
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public string StartGetTalk(int talkIndex) 
    {
        if (talkIndex == talkData[1].Length)
            return null;
        else
            return talkData[1][talkIndex];
    }



}
