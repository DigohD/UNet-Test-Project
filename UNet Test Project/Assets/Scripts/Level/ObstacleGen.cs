using UnityEngine;
using System.Collections;

public class ObstacleGen : MonoBehaviour {

	// the timer for the timeline
	private int timelineTime = 0;
	// as long as update is true the generation of sequences/enemies will
	// continue
	public bool update = true, enemyGenDone = false;
	// the total time in seconds for the timeline
	private int TIME;
	
	private ArrayList sequences = new ArrayList();
	private Hashtable sequenceTimeline = new Hashtable();
	private Hashtable objectTimeline = new Hashtable();
	public static Hashtable levelObjects = new Hashtable();
	

	// Use this for initialization
	void Start () {
		
	}
	
	public static void clearLevelObjects(){
		levelObjects.Clear();
	}
	
	public void mapColorToObject(Color col, GameObject go){
		levelObjects.Add(col, go);
	}
	
	public void addObjectToTimeline(GameObject go, int timeStamp){
		int min = (timeStamp * 60) - (1 * 60);
		int max = (timeStamp * 60) + (1 * 60);
		
		for (int i = min; i < max; i++)
			if (objectTimeline.ContainsKey(i))
				objectTimeline.Remove(i);
		objectTimeline.Add(timeStamp * 60, go);
	}
	
	public void addSequence(Sequence seq) {
		sequences.Add(seq);
	}
	
	public void addSequenceToTimeline(int seqID, int timeStamp) {
		int min = (timeStamp * 60) - (5 * 60);
		int max = (timeStamp * 60) + (5 * 60);
		
		sequenceTimeline.Add(timeStamp * 60, seqID);
	}
	
	public void generateRandomTimeline(){
		int min = 5;
		int max = 8;
		int i = 2;
		
		while (i < TIME) {
			sequenceTimeline.Add(i * 60, Random.Range(0, ((Sequence)sequences[0]).getSize() - 1));
			i += Random.Range(min, max);
		}
	}
	
	public void init(int time){
		TIME = time * 60;
	}
	
	public void FixedUpdate () {
		if (update) {
			timelineTime++;
			
			if(sequenceTimeline.ContainsKey(timelineTime)) {
				((Sequence)sequences[0]).runSequence((int)sequenceTimeline[timelineTime]);
				sequenceTimeline.Remove(timelineTime);
			}
			
			if(objectTimeline.ContainsKey(timelineTime)){
				GameObject go = (GameObject) Instantiate ((GameObject)objectTimeline[timelineTime], new Vector3 (500, 200, 
				                                                                                                ((GameObject)objectTimeline[timelineTime]).transform.position.z), ((GameObject)objectTimeline[timelineTime]).transform.rotation);
				go.SetActive(true);
				objectTimeline.Remove(timelineTime);
			}
			
		}
		
	}
}
