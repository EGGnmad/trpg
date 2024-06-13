using System.Linq;
using Unity.Services.Vivox;
using UnityEngine;

namespace TRPG.UI
{
    public class OutputDeviceManager : DeviceManager
    {
        public override void Init()
        {
            VivoxService.Instance.AvailableOutputDevicesChanged += UpdateDropdown;
        }

        public override void UpdateDropdown()
        {
            _dropdown.ClearOptions();
            _dropdown.AddOptions(VivoxService.Instance.AvailableOutputDevices.Select(x => x.DeviceName).ToList());
        }

        public override async void ChangeDevice(int index)
        {
            var outputDevice = VivoxService.Instance.AvailableOutputDevices[index];
            await VivoxService.Instance.SetActiveOutputDeviceAsync(outputDevice);
        }
    }
}