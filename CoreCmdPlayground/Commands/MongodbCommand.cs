using System;
using System.Collections.Generic;
using System.Text;
using UtilityLib;
using MongoDBLib;
using CoreCmd.Attributes;

namespace CoreCmdPlayground.Commands
{
    [Alias("mongo")]
    public class MongodbCommand : MongodbCommandBase { }
}
