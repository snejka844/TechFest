using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
	private Dictionary<uint, Answers> answers = new Dictionary<uint, Answers>();
	private Dictionary<uint, int> leaderboard = new Dictionary<uint, int>();
	
	public string GetQuestionStats()
	{
		string stats = "";

		//answers [0].answer [QuestionsList.CurrentQuestion];

		return stats;
	}
	
	public string GetQuizStats()
	{
		string stats = "";

		//Sort

		return stats;
	}

	public void AddAnswer(string username, string answer, uint networkId)
	{
		answers [networkId].username = username;
		
		if (answer.Equals (QuestionsList.GetCurrentQuestion ().Correct))
		{
			leaderboard [networkId]++;
		}
	}

	public void Setup()
	{
		
	}
}
