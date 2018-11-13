using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {

   public Color teamColor;

   public string teamId;

   public GameController teamController;

   public Vector3Int stackPosition;

   public void OnTurnStart()
   {
      teamController.active = true;
   }

   public void OnTurnEnd()
   {
      teamController.active = false;
   }

   // Use this for initialization
   void Start ()
   {
      teamController.SetTeam(teamId);
	}
     
   // Update is called once per frame
   void Update () {
		
	}
}
