using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGameLevelBasic : Singleton<ManagerGameLevelBasic>
{
    public ManagerQuestGame managerQuest;

    public IEnumerator IniciarPartida()
    {
        Player.Instance.State();
        yield return new WaitUntil(() => !Player.Instance.IsActivate);
        managerQuest.panelSelect.SetActive(true);
        yield return new WaitUntil(() => managerQuest.startGame);
        StartCoroutine(managerQuest.GetQuestion());
        yield return new WaitUntil(() => !managerQuest.startGame);
        Player.Instance.State();
    }
}
