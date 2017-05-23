using System.Collections.Generic;

public class Question
{
	public string Text {get;set;}
    public string[] Answers {get;set;}
    public string Correct { get; set; }

	public Question()
	{
		Text = string.Empty;
		Answers = new string[4];
		Correct = string.Empty;
	}
}