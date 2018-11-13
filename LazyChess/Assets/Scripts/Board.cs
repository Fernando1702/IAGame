using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour {

   public Vector2Int dimensions;
   
   public GameObject[] boardPositions;

   public GameObject boardPiece;

   public Color pairColor, unpairColor;

   public void Start()
   { 
      SetBoardColor();
      GameManager.Instance.gameBoard = this;
   }

   public int bidimensionalToUnidimensional(int x, int y)
   {
      return x + y * dimensions.x;
   }

   public GameObject getBoardPosition (int x, int y)
   {
      return boardPositions[x + y * dimensions.x];
   }

   public GameObject this[int x, int y]
   {
      get { return this.getBoardPosition(x,y); }
      set { }
   }

   void DestroyBoard()
   {
      for (int x = 0; x < dimensions.x; x++)
      {
         for (int y = 0; y < dimensions.y; y++)
         {
            Destroy(this[x, y].gameObject);
         }
      }

      boardPositions = null; 
   }

   public void GenerateBoard()
   {

      boardPositions = new GameObject[dimensions.x * dimensions.y];

      for (int x = 0; x < dimensions.x; x++)
      {
         for (int y = 0; y < dimensions.y; y++)
         {
            GameObject instancedPiece = Instantiate(boardPiece);

            instancedPiece.transform.position = new Vector3Int(x, 0 ,y);
            
            boardPositions[x + y * dimensions.x] = instancedPiece;
           
            instancedPiece.transform.SetParent(transform);
         }
      }

      SetBoardColor();
   }

   public void SetBoardColor()
   {
      for (int x = 0; x < dimensions.x; x++)
      {
         for (int y = 0; y < dimensions.y; y++)
         {
            this[x,y].GetComponent<MeshRenderer>().material.color = ((x + y) % 2 == 0) ? pairColor : unpairColor;
         }
      }
   }

   public void SetPieceColor(int x, int y, Color color)
   {
      //Debug.Log(color);
      this[x, y].GetComponent<MeshRenderer>().material.color = color;
   }

   public Vector3Int WorldPosToBoardPos(Vector3 worldPos)
   {
      // int xPos = Mathf.Clamp(Mathf.RoundToInt(worldPos.x), 0, dimensions.x - 1);
      // int zPos = Mathf.Clamp(Mathf.RoundToInt(worldPos.z), 0, dimensions.y - 1);

      //Debug.Log(worldPos);

      int xPos = Mathf.RoundToInt(worldPos.x);
      int zPos = Mathf.RoundToInt(worldPos.z);

      return new Vector3Int(xPos, 0, zPos);
   }

   public bool ItsInsideBoard(Vector3Int position)
   {
      return position.x >= 0 && position.x < dimensions.x && position.z >= 0 && position.z < dimensions.y;
   }
}
