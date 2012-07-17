using UnityEngine;
using System.Collections;

public class ConversationDisplayer : MonoBehaviour {
	
	public int height = 300;
	public int width = 300;
	
	public Font font;
	
	private Conversation conversation;
	
	void Start(){
		font = Resources.Load("Erika Ormig") as Font;	
	}
	
	void OnGUI(){
		if(conversation == null)
			return;
		
		GUIStyle guiStyle = new GUIStyle();
		guiStyle.wordWrap = true;
		guiStyle.font = font;
		guiStyle.normal.background = Resources.Load("Textures/SyntaxHighlightBlue") as Texture2D;
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 20;
		guiStyle.normal.textColor = Color.black;
		
		GUI.Box(new Rect(Screen.width/2-width/2,Screen.height/2-height/2,width,height), conversation.GetText(), guiStyle);
		
		int response_height = 0;
		
		if(conversation.GetResponses() != null)
		{	
			foreach(object responseObject in conversation.GetResponses())
			{
				Response response = responseObject as Response;
				
				string response_text = response.getResponseText();
				
				GUIStyle buttonStyle = new GUIStyle();
				buttonStyle.wordWrap = true;
				buttonStyle.font = font;
				buttonStyle.normal.background = Resources.Load("Textures/SyntaxHighlightGreen") as Texture2D;
				buttonStyle.alignment = TextAnchor.MiddleCenter;
				buttonStyle.fontSize = 20;
				buttonStyle.normal.textColor = Color.black;
				
				if(GUI.Button(new Rect(Screen.width/2+width/2 + 10,Screen.height/2-height/2 + response_height,200,50), response_text, buttonStyle))
				{
					conversation.Respond(response);
					
					if(response.isExit())
					{
						conversation = null;
						Time.timeScale = 1;
					}
				}
				response_height += 60;
			}
		}
	}
	
	public void Converse(Conversation conversation)
	{
		this.conversation = conversation;
	}
}
