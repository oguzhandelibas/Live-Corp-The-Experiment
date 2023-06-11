// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Magazine.
    /// </summary>
    public class Magazine : MagazineBehaviour
    {
        #region FIELDS SERIALIZED

        [Header("Settings")] [Tooltip("Total Ammunition.")] 
        [SerializeField]
        private int magazineCurrent = 3;
        [SerializeField]
        private int magazineCapacity = 5;
        [SerializeField]
        private int ammunitionCurrent = 15;
        [SerializeField] 
        private int ammunitionCapacity = 20;

        [Header("Interface")]

        [Tooltip("Interface Sprite.")]
        [SerializeField]
        private Sprite sprite;

        #endregion

        #region GETTERS
        
        public override int GetMagazine() => magazineCurrent; // şarjörün şu anki sahip olduğu
        public override int GetMagazineCapacity() => magazineCapacity; // şarjörün maks kapasite
        
        public override int GetAmmunition() => ammunitionCurrent; // silahın toplam mermisi
        public override int GetAmmunitionCapacity() => ammunitionCapacity; // silah için alınabilen maks mermi

        public override void SetAmmunition(int ammunition)
        {
            ammunitionCurrent -= ammunition;
            if (ammunitionCurrent < 0) ammunitionCurrent = 0;
        }

        public override void SetMagazine(int value)
        {
            magazineCurrent =  Mathf.Clamp(magazineCurrent + value, 0, magazineCapacity);
        }

        /// <summary>
        /// Sprite.
        /// </summary>
        public override Sprite GetSprite() => sprite;

        #endregion
    }
}