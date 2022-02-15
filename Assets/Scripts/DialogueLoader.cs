using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueLoader
{
    public static string[] s;
    public static Queue<string> name=new Queue<string>();
    public static Queue<string> content=new Queue<string>();
    public static Queue<string>[] LoadDialogue(string ep){
        var text=Resources.Load<TextAsset>("Text/"+ep);
        foreach(string i in text.text.Split('\n')){
            s=i.Split('#');
            name.Enqueue(s[0]);
            content.Enqueue(s[1].Substring(1));
        }
        Queue<string>[] dialg=new Queue<string>[2];
        dialg[0]=name;
        dialg[1]=content;
        return dialg;
    }
}