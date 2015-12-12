using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventText : MonoBehaviour {

    [SerializeField]
    private Text[] eventText;

    [SerializeField]
    private Transform eventTextSpawn;

    [SerializeField]
    private bool sendEvent = false;

	// Update is called once per frame
	void Update () {
        if( sendEvent )
        {
            int temp = (int)Random.Range( 0, 5 );
            string message = "";
            switch( temp )
            {
                case 0:
                    message = "Winter is coming!";
                    break;
                case 1:
                    message = "Cloudy day!";
                    break;
                case 2:
                    message = "Rain showers bring mayflowers";
                    break;
                case 3:
                    message = "Meteor shower!!";
                    break;
                case 4:
                    message = "Tornado warning!";
                    break;
                default:
                    print( "broke temp event switch" );
                    message = "you broke this";
                    break;
            }
            SpawnEventText( message );
            sendEvent = false;
        }	
	}

    public void SpawnEventText( string textEvent )
    {
        Text temp = eventText[FindText()];
        temp.text = textEvent;
        temp.transform.position = eventTextSpawn.position;
        temp.gameObject.SetActive( true );
        TextEventScript textScript = temp.GetComponent<TextEventScript>();
        textScript.StartLife();
    }

    private int FindText()
    {
        int result = -1;
        for( int i = 0; i < eventText.Length; i++ )
        {
            if( !eventText[i].gameObject.activeInHierarchy )
            {
                result = i;
                break;
            }
        }

        return result;
    }
}
