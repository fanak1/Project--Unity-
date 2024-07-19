using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListConfiguration<T>
{
    public static List<T> CopyRandomFromList(List<T> list, int amount, bool allowOutOfIndex = false) {
        if (amount > list.Count && allowOutOfIndex) {
            List<T> temp1 = new List<T>(list);
            for (int i = 0; i < amount - list.Count; i++) {
                int rand = Random.Range(0, list.Count);
                temp1.Add(list[rand]);
            }
            return temp1;

        }
        List<T> temp = new List<T>(list);
        int taken = 0;
        while (temp.Count > 0 && taken < list.Count - amount) {
            int random = Random.Range(0, temp.Count);
            temp.RemoveAt(random);
            taken++;
        }
        return temp;
    }

    public static List<T> TakeRandomFromList(List<T> list, int amount, bool allowOutOfIndex = false) {
        List<T> temp = new List<T>();
        int listCount = list.Count;
        int taken = 0;
        while (list.Count > 0 && taken < amount) {
            int random = Random.Range(0, list.Count);
            temp.Add(list[random]);
            list.RemoveAt(random);
            taken++;
        }
        if (amount > list.Count && allowOutOfIndex) {
            for (int i = 0; i < amount - listCount; i++) {
                int rand = Random.Range(0, listCount);
                temp.Add(temp[rand]);
            }
        }
        return temp;
    }
}
