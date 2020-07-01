using CoreCmd.Attributes;
using InfluxDBTestLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCmdPlayground.Commands
{
    [Alias("influx")]
    public class InfluxdbCommand : InfluxdbCommandBase
    { }
}
