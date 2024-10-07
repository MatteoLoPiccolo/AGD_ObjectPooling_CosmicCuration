using CosmicCuration.PowerUps;
using CosmicCuration.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupPool : GenericObjectPool<PowerUpController>
{
    private PowerUpData powerUpData;

    public PowerUpController GetPowerup<T>(PowerUpData powerUpData) where T : PowerUpController
    {
        this.powerUpData = powerUpData;
        return GetItem<T>();
    }

    protected override PowerUpController CreateItem<T>()
    {
        if(typeof(T) == typeof(Shield))
        {
            return new Shield(powerUpData);
        }
        else if (typeof(T) == typeof(RapidFire))
        {
            return new RapidFire(powerUpData);
        }
        else if (typeof(T) == typeof(DoubleTurret))
        {
            return new DoubleTurret(powerUpData);
        }
        else
            throw new NotSupportedException();
    }
}