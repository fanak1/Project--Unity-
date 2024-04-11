using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewCipher", menuName = "Cipher")]
public class ScriptableCipher : ScriptableObject
{
    public Difficulty difficulty;

    public string question; //Question of this cipher
    public string[] answers; //all answer of this cipher, the right answer default will be at index 0

    public int correctAnswer;

}

[Serializable]
public enum Difficulty {
    Easy,
    Medium,
    Hard
}
