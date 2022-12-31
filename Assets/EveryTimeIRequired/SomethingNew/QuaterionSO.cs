using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 在资源创建菜单创建物体：[CreateAssetMenu(menuName = "Quiz Quaterion", fileName = "New Quaterion")]
/// 需要继承ScriptableObject
/// 可以创造一些保存不同数据的脚本实例
///// </summary>
namespace SomethingNew
{
    [CreateAssetMenu(menuName = "Quiz Quaterion", fileName = "New Quaterion")]
    public class QuaterionSO : ScriptableObject
    {
        [TextArea(2, 6)][SerializeField] string quaterion = "Enter new Quaterion";
        [SerializeField] string[] answer = new string[4];
        [SerializeField][Range(0, 3)] int correctAnswerIndex;
        public string GetQueration()
        {
            return quaterion;
        }
        public int GetCorrectAnswerIndex()
        {
            return correctAnswerIndex;
        }
        public string GetAnswer(int index)
        {
            return answer[index];
        }
    }
}
