using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace MiniGame.MemoryGame
{
    public class MG_Controller : MonoBehaviour
    {
        [Header("Index Lists")]
        private List<int> PathIndexes = new List<int>();
        private List<int> SelectedIndexes = new List<int>();
        
        [Header("Inputs & Outputs")]
        [SerializeField] private MeshRenderer[] Inputs;
        [SerializeField] private MeshRenderer[] Outputs;

        [Header("Input Materials")]
        [SerializeField] private Material InputObjectMaterial;
        [SerializeField] private Material InputIndicatorMaterial;
        
        
        [Header("Output Materials")]
        [SerializeField] private Material OutputObjectMaterial;
        [SerializeField] private Material OutputCorrectMaterial;
        [SerializeField] private Material OutputWrongMaterial;
        
        [Header("Local Variables")]
        private int SuccessCount;
        private bool Fail = false;
        public bool CanInteract { get; set; }
        
        public void StartPathRoutine(int counter)
        {
            StartCoroutine(ShowPathRoutine(counter));
        }
        
        public void Interact(int index)
        {
            if(SelectedIndexes.Count > PathIndexes.Count) return;
            
            SelectedIndexes.Add(index);
            if (SelectedIndexes.Count == PathIndexes.Count)
            {
                CanInteract = false;
                if (SelectedIndexes.SequenceEqual(PathIndexes)) 
                    CorrectAnswer();
                else 
                    MadeMistake();
            }
        }
        
        IEnumerator ShowPathRoutine(int pathLength)
        {
            CanInteract = false;
            for (int i = 0; i < pathLength; i++)
            {
                MeshRenderer meshRenderer = GetRandomInputMeshRenderer();
                meshRenderer.material.DOColor(InputIndicatorMaterial.color, 0.3f);
                yield return new WaitForSeconds(1f);
                meshRenderer.material.DOColor(InputObjectMaterial.color, 0.15f);
            }

            CanInteract = true;
        }

        private MeshRenderer GetRandomInputMeshRenderer()
        {
            int index = Random.Range(0, Inputs.Length);
            PathIndexes.Add(index);
            return Inputs[index];
        }

        private void CorrectAnswer()
        {
            Outputs[SuccessCount].material = OutputCorrectMaterial;
        }

        private void MadeMistake()
        {
            Fail = true;
            Outputs[SuccessCount].material = OutputWrongMaterial;
        }
    }
}

