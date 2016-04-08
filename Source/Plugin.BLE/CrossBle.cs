﻿using System;
using Plugin.BLE.Abstractions;

namespace Plugin.BLE
{
    public static class CrossBle
    {
        static readonly Lazy<IBluetoothLE> Implementation = new Lazy<IBluetoothLE>(CreatePermissions, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static IBluetoothLE Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IBluetoothLE CreatePermissions()
        {
#if PORTABLE
            return null;
#else
            return new BleImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}