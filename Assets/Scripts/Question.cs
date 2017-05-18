using System.Collections.Generic;

public class Question
{
	public string Text {get;set;}
    public string[] Answers {get;set;}
    public int Correct { get; set; }

	public Question()
	{
		Text = "";
		Answers = new string[4];
		Correct = -1;
	}
}