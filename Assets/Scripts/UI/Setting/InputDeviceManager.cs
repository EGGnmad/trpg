using System.Linq;
using Unity.Services.Vivox;

namespace TRPG.UI
{
    public class InputDeviceManager : DeviceManager
    {
        public override void Init()
        {
            VivoxService.Instance.AvailableInputDevicesChanged += UpdateDropdown;
        }

        public override void UpdateDropdown()
        {
            _dropdown.ClearOptions();
            _dropdown.AddOptions(VivoxService.Instance.AvailableInputDevices.Select(x => x.DeviceName).ToList());
        }

        public override async void ChangeDevice(int index)
        {
            var inputDevice = VivoxService.Instance.AvailableInputDevices[index];
            await VivoxService.Instance.SetActiveInputDeviceAsync(inputDevice);
        }
    }
}