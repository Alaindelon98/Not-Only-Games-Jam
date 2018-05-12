using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentsManager : MonoBehaviour {

    public ReadComments i_readComments;

    List<string> l_commentsList;
    List<Text> l_spawnedList;

    public Canvas m_canvas;
    public Text m_commentPrefab;
    public Transform m_startLine;
    public Transform m_deathLine;



    public float l_moveSpace;




	// Use this for initialization
	void Start () {

        l_spawnedList = new List<Text>();
        i_readComments.ReadString();
        SpawnComments('6');
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnComments(char l_sceneChar)
    {
        l_commentsList = i_readComments.GetComments(l_sceneChar);
        StartCoroutine(SpawnNextComment());
        Debug.Log("comments list count: " + l_commentsList.Count);

    }




    IEnumerator SpawnNextComment()
    {
        l_spawnedList.Clear();


        //for (int idx = l_commentsList.Count-1; idx >= 0; idx--)
        for(int idx = 0; idx < l_commentsList.Count -1; idx++)
        {
            //Move all the elements on the spawned list
            //Check if the element can be destroyed

            for (int i = l_spawnedList.Count -1; i >= 0; i--)
            {   
                l_spawnedList[i].transform.position = new Vector3(l_spawnedList[i].transform.position.x, l_spawnedList[i].transform.position.y - l_moveSpace, l_spawnedList[i].transform.position.z);

                if (l_spawnedList[i].transform.position.y < m_deathLine.position.y)
                {
                    l_spawnedList.Remove(l_spawnedList[i]);
                    l_spawnedList[i].transform.position = new Vector3(l_spawnedList[i].transform.position.x + 1000, l_spawnedList[i].transform.position.y - l_moveSpace, l_spawnedList[i].transform.position.z);
                }
            }

            //Add new element
            Text newText = Instantiate(m_commentPrefab, m_canvas.transform);
            newText.transform.position = m_startLine.transform.position;
            newText.text = l_commentsList[idx];
            l_spawnedList.Add(newText);
            yield return new WaitForSeconds(0.5f);
        }

        yield break;
        
    }
}
