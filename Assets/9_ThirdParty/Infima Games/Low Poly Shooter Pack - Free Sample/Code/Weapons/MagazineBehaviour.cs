// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Magazine Behaviour.
    /// </summary>
    public abstract class MagazineBehaviour : MonoBehaviour
    {
        #region GETTERS
        
        /// <summary>
        /// Returns The Total Ammunition.
        /// </summary>
        public abstract int GetAmmunitionCapacity();

        public abstract int GetMagazine();
        public abstract int GetMagazineCapacity();
        public abstract int GetAmmunition();
        public abstract void SetAmmunition(int ammunition);
        public abstract void SetMagazine(int value);
        /// <summary>
        /// Returns the Sprite used on the Character's Interface.
        /// </summary>
        public abstract Sprite GetSprite();

        #endregion
    }
}