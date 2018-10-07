﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[System.Serializable]
public class Member{
    public static Dictionary<string, Sprite> memberDict = new Dictionary<string, Sprite>();
    public string name;
    public string subname;
    public string introduce;
    public string profile;
    public Sprite Image{
        get{
            if (memberDict.ContainsKey(profile))
            {
                return memberDict[profile];
            }
            else
            {
                var sprite = Resources.Load<Sprite>("members/" + profile);
                memberDict.Add(profile, sprite);
                return sprite;
            }
        }
    }
}

[System.Serializable]
public class Thing
{
    public static Dictionary<string, Sprite> thingDict = new Dictionary<string, Sprite>();
    public string name;
    public string category;
    public string description;
    public string image;
    public Sprite Thumb
    {
        get
        {
            var path = "things/200" + image;
            if (thingDict.ContainsKey(path))
            {
                return thingDict[path];
            }
            else
            {
                var sprite = Resources.Load<Sprite>(path);
                thingDict.Add(path, sprite);
                return sprite;
            }
        }
    }
    public Sprite Image
    {
        get
        {
			var path = "things/900" + image;
            if (thingDict.ContainsKey(path))
            {
                return thingDict[path];
            }
            else
            {
                var sprite = Resources.Load<Sprite>(path);
                thingDict.Add(path, sprite);
                return sprite;
            }
        }
    }
}

public class ThingsManager : MonoBehaviour {
    public GameObject thing;
    List<GameObject> things;
    public TextAsset thingData;
    public TextAsset memberData;
    public List<Thing> list = new List<Thing>();
    public List<Member> members = new List<Member>();
    int index = 0;
    int amount = 0;

    public void Awake()
    {
        var result = JsonMapper.ToObject<List<Thing>>(thingData.text);
        var memberResult = JsonMapper.ToObject<List<Member>>(memberData.text);
        members.AddRange(memberResult);
        list.AddRange(result);
        amount = members.Count;
        InitList();
    }

    public void InitList(){
        index = 0;
        for (int i = 0; i < 3; i ++){
            int count = amount / 3;
            var type = i % 2 == 1;
            if (type) count += amount % 3;
            InitLine(i,count);
        }
    }

    public void InitLine(int line, int count){
        float offset = line % 2 == 0 ? 0 : 0.5f;
        var start = (amount / 3f)/2f * -1;
        for (int i = 0; i < count;i++){
            Vector3 pos = new Vector3();
            Member member = members[index++];
            pos.x = (start + i + offset)*3;
            pos.y = (line - 1)*3;
            var item = Instantiate(thing,transform);
            var selectedThing = list[Random.Range(0, list.Count)];
            item.GetComponent<ThingObject>().SetThing(selectedThing,member);
            item.transform.localPosition = pos;
        }

    }
}