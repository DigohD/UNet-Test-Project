using UnityEngine;
using System.Collections;

public class Sequence : MonoBehaviour {
	//array with sequence textures
	public Texture2D[] sequences;
	// key = integer id (same as above) , value = list of pixel values
	private Hashtable sequencePixelsMap = new Hashtable();
	// key = integer id (same as above), value = hashtable where key = color of pixel and value = list of the positions of all pixels with that color
	private Hashtable idColorPosMap = new Hashtable();

	public void Start () {
		for (int i = 0; i < sequences.Length; i++) {
			Texture2D t = (Texture2D)Instantiate (sequences [i], new Vector3 (0, 0, 0), Quaternion.identity);
		}
		
		for (int i = 0; i < sequences.Length; i++) { 
			Color[] pixels = new Color[sequences[i].width * sequences[i].height];
			pixels = sequences[i].GetPixels();
			ArrayList list = new ArrayList();
			
			for(int j = 0; j < pixels.Length; j++)
				list.Add(pixels[j]);
			
			sequencePixelsMap.Add(i, list);
		}
		
		scanSequence();
	}

	public void runSequence(int id){
		foreach(Color col in ((Hashtable)idColorPosMap[id]).Keys){
			if(ObstacleGen.levelObjects.ContainsKey(col)){
				foreach(Vector2 v in (ArrayList)((Hashtable)idColorPosMap[id])[col]){
					GameObject go = (GameObject) Instantiate((GameObject)ObstacleGen.levelObjects[col], new Vector3(v.x, v.y, 
					                                                                                                ((GameObject)ObstacleGen.levelObjects[col]).transform.position.z), ((GameObject)ObstacleGen.levelObjects[col]).transform.rotation);
					go.SetActive(true);
				}
			}
		}
	}
	
	public void scanSequence(){
		for (int i = 0; i < sequences.Length; i++) {
			idColorPosMap.Add(i, new Hashtable());
			for (int y = 0; y < sequences[i].height; y++)  
				for (int x = 0; x < sequences[i].width; x++) {
					Color col = (Color)((ArrayList)sequencePixelsMap[i])[x + (y * sequences[i].width)];
					if (!(col.Equals (Color.black))) {
					
						float widthRatio = (float) (470.0f / sequences[i].width);
						float heightRatio = (float) (225.0f / sequences[i].height);
					
						if (((Hashtable)(idColorPosMap[i])).ContainsKey (col)) {
							((ArrayList)((Hashtable) idColorPosMap[i])[col]).
								Add (new Vector2 ((x * widthRatio) + 700, y * heightRatio + 20));
						} else {
							((Hashtable)(idColorPosMap[i])).Add (col, new ArrayList ());
							((ArrayList)((Hashtable) idColorPosMap[i])[col]).
								Add (new Vector2 ((x * widthRatio) + 700, y * heightRatio + 20));
						}
					}
				}
			}
	}
	
	public int getSize(){
		return sequences.Length;
	}
}
