﻿using System.Collections;
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

    private int l_newLinesOcuped;


	// Use this for initialization
	void Start () {

        l_spawnedList = new List<Text>();
        i_readComments.ReadString();
        //SpawnComments('6');
        l_newLinesOcuped = 0;
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
            //Add new element
            Text newText = Instantiate(m_commentPrefab, m_canvas.transform);
            newText.text = l_commentsList[idx];
            Canvas.ForceUpdateCanvases();
            l_newLinesOcuped = newText.cachedTextGenerator.lines.Count;
            newText.transform.position = m_startLine.transform.position;
            l_spawnedList.Add(newText);


            //Move all the elements on the spawned list
            //Check if the element can be destroyed

            for (int i = l_spawnedList.Count -2; i >= 0; i--)
            {   
                l_spawnedList[i].transform.position = new Vector3(l_spawnedList[i].transform.position.x, l_spawnedList[i].transform.position.y - l_moveSpace*l_newLinesOcuped, l_spawnedList[i].transform.position.z);

                if (l_spawnedList[i].transform.position.y < m_deathLine.position.y)
                {
                    
                    l_spawnedList[i].transform.position = new Vector3(l_spawnedList[i].transform.position.x + 1000, l_spawnedList[i].transform.position.y - l_moveSpace, l_spawnedList[i].transform.position.z);
                    l_spawnedList.Remove(l_spawnedList[i]);
                }
            }

            
            yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
        }

        yield break;
        
    }
}
