﻿using System;

namespace LiteNetLibManager
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class TargetRpcAttribute : RpcAttribute
    {
    }
}
