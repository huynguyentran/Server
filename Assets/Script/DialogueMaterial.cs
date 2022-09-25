using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMaterial : MonoBehaviour
{
    public NPC npc;

    bool isTalking = false;
    
    float distance;
    float curResposneTracker = 0;
    
    public GameObject player;
    public GameObject dialogueUI;

    public TextMeshProUGUI  npcName;
    public TextMeshProUGUI  npcDialogueBox;
    public TextMeshProUGUI  playerResponse;

    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
    }

    void OnMouseOver()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance <= 3f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                curResposneTracker++;
                if (curResposneTracker >= npc.playerDialogue.Length -1)
                {
                    curResposneTracker = npc.playerDialogue.Length-1;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                curResposneTracker--;
                if(curResposneTracker < 0)
                {
                    curResposneTracker =0;
                }
            }
            //trigger the dialogue
            if(Input.GetKeyDown(KeyCode.E)&& isTalking==false)
            {
                StartConversation();
            }
            else if (Input.GetKeyDown(KeyCode.E) && isTalking == true){
                EndDialogue();
            }

            if (curResposneTracker ==0 && npc.playerDialogue.Length >=0){
                playerResponse.text = npc.playerDialogue[0];
                if(Input.GetKeyDown(KeyCode.Return)){
                    npcDialogueBox.text = npc.dialogue[1];
                }
            }
            else if(curResposneTracker == 1 && npc.playerDialogue.Length >=1)
            {
                playerResponse.text = npc.playerDialogue[1];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                      npcDialogueBox.text = npc.dialogue[2];
                }
            }
              else if(curResposneTracker == 2 && npc.playerDialogue.Length >=2)
            {
                playerResponse.text = npc.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                      npcDialogueBox.text = npc.dialogue[3];
                }
            }
        }
    }

    void StartConversation()
    {
        isTalking = true;
        curResposneTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }
}
