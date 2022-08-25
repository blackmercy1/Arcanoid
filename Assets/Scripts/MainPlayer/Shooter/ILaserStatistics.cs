using System;

namespace MainPlayer.Shooter
{
    public interface ILaserStatistics
    {
        event Action<int> LaserAmmo;
        event Action<float> TimeToReloadLaser;
    }
}