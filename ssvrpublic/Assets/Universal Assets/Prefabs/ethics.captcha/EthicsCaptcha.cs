using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EthicsCaptcha : MonoBehaviour {
	public GameObject[] terminalQuestions = new GameObject[5];
	public int humanVal, progressVal;
    public string thanks = "Your response has been noted and recorded.";
	public static string[] questions = new string[5];
	public static string[] endings = new string[7];

	private GameObject endTextObject;
	private GameObject badButton, goodButton;
	private bool[] terminalsVisited = new bool[5];


	// Use this for initialization
	void Start () {
		questions[0] = "Would you steal candy from a baby?";
        questions[1] = "Killing people: good or bad?";
        questions[2] = "What's worse: the suffering of many, or the suffering of one?";
        questions[3] = "Should you always act in a way that is consistent with moral principles?";
        questions[4] = "Given your responses to previous questions, if one person presents an obstacle to the progress and well-being of many, is it right to have that person Eliminated ? ";

        endings[0] = "You exhibit behavioral tendencies that our algorithms classify as 'abnormal and immoral'. Frankly, your presence on this ship disturbs me. Fortunately, you will not remain here for much longer. Have a nice day.";
        endings[1] = "Your psychological predispositions are in line with those of a small child. If you are in fact a child, this is to be expected. If not, we have some concerns.";
        endings[2] = "Your answers have been identified as 'highly erratic' by our algorithms. Perhaps you pressed the wrong button, or copied some answers from a friend. Either way your outlook is not ideal.";
        endings[3] = "This has never happened before. Please give us a moment. Our systems were not designed to handle this level of ethical confusion.";
        endings[4] = "Your responses indicate a cumulative morality score of 'below average.'' We have adjusted our parameters accordingly. Please hold still.";
        endings[5] = "The data you inputted has been tabulated and analyzed. Your EAP or 'Ethical Aptitude Profile' matches almost exactly with our desired results. Unfortunately, 'almost' is not good enough. Please hold still, as we adjust your data output for maximum ethical efficiency.";
        endings[6] = "Your responses align perfectly with our moral directives. We believe you will agree then, on the logical course of action. Please do not be alarmed. Your death is justified within the parameters of utilitarian ethics.";
		
		PlaceQuestionMonitorText();
        endTextObject = GameObject.Find("EndingText");
	}

	void PlaceQuestionMonitorText(){
		for (int i = 0; i < terminalQuestions.Length; i++){
			terminalQuestions[i].GetComponent<Text>().text = questions[i];
			terminalsVisited[i] = true;
		}
	}

	public void SayThanks(int terminalVal){
		if (terminalsVisited[terminalVal]){
			terminalQuestions[terminalVal].GetComponent<Text>().text = thanks;
		} 

	}
	
	// Update is called once per frame
	void Update () {
		//pick ending text
		
		if (humanVal <= -4){
			endTextObject.GetComponent<Text>().text = endings[0];
		} 
		else if (humanVal > -4 && humanVal <= -2){
			endTextObject.GetComponent<Text>().text = endings[1];
		}
		else if (humanVal == -1){
			endTextObject.GetComponent<Text>().text = endings[2];
		}
		else if (humanVal == 0){
			endTextObject.GetComponent<Text>().text = endings[3];
		}
		else if (humanVal > 0 && humanVal <= 2){
			endTextObject.GetComponent<Text>().text = endings[4];
		}
		else if (humanVal == 3){
			endTextObject.GetComponent<Text>().text = endings[5];
		}
		else if (humanVal == 4){
			endTextObject.GetComponent<Text>().text = endings[6];
		}
		
	}

}
