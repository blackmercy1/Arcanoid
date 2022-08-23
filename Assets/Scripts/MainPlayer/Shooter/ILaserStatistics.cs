using System;

namespace MainPlayer
{
    public interface ILaserStatistics
    {
        event Action<int> LaserAmmo;
        event Action<float> TimeToReloadLaser;
    }
}