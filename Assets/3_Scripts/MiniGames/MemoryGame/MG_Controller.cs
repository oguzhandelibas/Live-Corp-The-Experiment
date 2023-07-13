using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Player;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MiniGame.MemoryGame
{
    public class MG_Controller : MonoBehaviour
    {
        [Header("Index Lists")]
        public List<int> PathIndexes = new List<int>();
        public List<int> SelectedIndexes = new List<int>();
        
        [Header("Inputs & Outputs")]
        [SerializeField] private MeshRenderer[] Inputs;
        [SerializeField] private MeshRenderer[] Outputs;

        [Header("Input Materials")]
        [SerializeField] private Material InputObjectMaterial;
        [SerializeField] private Material InputIndicatorMaterial;
        [SerializeField] private Material InputSelectedObjectMaterial;
        
        
        [Header("Output Materials")]
        [SerializeField] private Material OutputObjectMaterial;
        [SerializeField] private Material OutputCorrectMaterial;
        [SerializeField] private Material OutputWrongMaterial;

        [Header("Local Variables")] 
        private int pathLength = 1;
        private int SuccessCount;
        private bool Fail = false;
        public bool CanInteract { get; set; }
        public bool HasTrigger { get; set; }

        [Header("Event")] 
        public UnityEvent OnStart;
        public UnityEvent OnSuccess;
        public UnityEvent OnFail;
        public UnityEvent OnEnd;

        private void Start()
        {
            SuccessCount = 0;
        }

        public void StartPathRoutine()
        {
            OnStart?.Invoke();
            HasTrigger = true;
            StartCoroutine(ShowPathRoutine(1.0f));
        }
        
        public void Interact(int index)
        {
            if(SelectedIndexes.Count > PathIndexes.Count) return;
            MeshRenderer meshRenderer = Inputs[index];
            meshRenderer.material.DOColor(InputSelectedObjectMaterial.color, 0.1f).OnComplete(delegate
            {
                SetColor(meshRenderer, InputObjectMaterial, 0.1f);
            });;
            SelectedIndexes.Add(index);
            if (SelectedIndexes.Count == PathIndexes.Count)
            {
                CanInteract = false;
                if (SelectedIndexes.SequenceEqual(PathIndexes)) 
                    CorrectAnswer();
                else 
                    MadeMistake();
                PathIndexes.Clear();
                SelectedIndexes.Clear();
            }
        }
        
        IEnumerator ShowPathRoutine(float waitTime = 0.0f)
        {
            yield return new WaitForSeconds(waitTime);
            CanInteract = false;
            pathLength++;
            for (int i = 0; i < pathLength; i++)
            {
                MeshRenderer meshRenderer = GetRandomInputMeshRenderer();
                meshRenderer.material.DOColor(InputIndicatorMaterial.color, 0.15f);
                yield return new WaitForSeconds(0.5f);
                meshRenderer.material.DOColor(InputObjectMaterial.color, 0.15f);
                yield return new WaitForSeconds(0.5f);
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
            if(SuccessCount > 3) return;
            Outputs[SuccessCount].material = OutputCorrectMaterial;
            
            SuccessCount++;
            
            if (SuccessCount < 3)
            {
                StartCoroutine(ShowPathRoutine(0.5f));
            }
            else
            {
                OnSuccess?.Invoke();
                OnEnd?.Invoke();
                PlayerController.Instance.UnlockMovement();
            }
        }

        private void MadeMistake()
        {
            Fail = true;
            Outputs[SuccessCount].material = OutputWrongMaterial;
            OnFail?.Invoke();
            OnEnd?.Invoke();
        }
        
        private void SetColor(MeshRenderer mR, Material m, float inTime)
        {
            mR.material.DOColor(m.color, inTime);
        }
    }
}

