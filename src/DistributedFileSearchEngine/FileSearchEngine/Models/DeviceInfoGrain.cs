using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchEngine.Models
{
    public class DeviceInfoGrain : Grain, IDeviceInfoGrain
    {
        public Task<DeviceInfo> GetInfo()
        {
            var info = DeviceInfo.GetDeviceInfo();
            return Task.FromResult( info);
        }
    }
}
