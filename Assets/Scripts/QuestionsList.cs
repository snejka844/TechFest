using System.Collections.Generic;

public class QuestionsList
{
	static public List<Question> Questions = new List<Question>();
	static public int CurrentQuestion = 0;

	static public Question GetCurrentQuestion()
	{
		return Questions [CurrentQuestion];
	}
}