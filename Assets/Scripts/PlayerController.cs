using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SyncVar(hook = "OnQeustionReceived")]
    public string questionText;

    [SyncVar]
    private int amountOfAnswers = 0;

	public override void OnStartServer()
	{
		UIController.Instance.Populate ();
		UIController.Instance.serverUI.SetActive(true);
		UIController.Instance.clientUI.SetActive(false);

		UIController.Instance.SetUsername("Player" + netId);
		UIController.Instance.pc = this;
	}

	public override void OnStartClient()
    {
		StartCoroutine (PostStartClientRoutine ());
    }

	IEnumerator PostStartClientRoutine()
	{
		yield return null;
		
		if(isServer)
		{
			UIController.Instance.Populate ();
			UIController.Instance.serverUI.SetActive(true);
			UIController.Instance.clientUI.SetActive(false);
		}
		else
		{
			UIController.Instance.serverUI.SetActive(false);
			UIController.Instance.clientUI.SetActive(false);
		}

		if (isLocalPlayer)
		{
			UIController.Instance.SetUsername("Player" + netId);
			UIController.Instance.pc = this;
		}
	}
    
    public void OnQeustionReceived(string value)
    {
        UIController.Instance.question.text = value;

        if(!isServer)
        {
            UIController.Instance.clientUI.SetActive(true);
        }
    }

    public void OnQuestionSent(string text)
    {
        if(!isServer)
        {
            return;
        }

        UIController.Instance.serverUI.SetActive(false);
        amountOfAnswers = 0;
        questionText = text;
		Question q = QuestionsList.GetCurrentQuestion ();
		RpcSendAnwers (q.Answers);
    }

    public void OnAnswerSent(string username, string answer)
    {
        CmdSendAnswer(username, answer);
        
        UIController.Instance.clientUI.SetActive(false);
    }

    [Command]
    public void CmdSendAnswer(string username, string answer)
    {
        amountOfAnswers++;
		Debug.Log(username + " answered with answer " + answer + " answers so far: " + amountOfAnswers + " out of " + (NetworkServer.connections.Count - 1));

        if(amountOfAnswers >= NetworkServer.connections.Count - 1)
        {
			QuestionsList.CurrentQuestion++;
            Debug.Log("Ready with all anwers. Show Statistics.");
            UIController.Instance.serverUI.SetActive(true);

			if (QuestionsList.CurrentQuestion >= QuestionsList.Questions.Count)
			{
				Debug.LogWarning ("Finished with all questions. Do something");
			}
        }
    }

	[ClientRpc]
	public void RpcSendAnwers(string[] ans)
	{
		UIController.Instance.SetAnswerTexts (ans);
	}
}