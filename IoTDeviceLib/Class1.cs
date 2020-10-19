using Iot.Device.Spi;
using System;
using System.Device.Spi;

namespace IoTDeviceLib
{
    public class Class1
    {
        public void SpiTest()
        {
            using (SpiDevice spi = new SoftwareSpi(clk: 6, miso: 23, mosi: 5, cs: 24))
            {
                // do stuff over SPI
            }
        }
    }
}
