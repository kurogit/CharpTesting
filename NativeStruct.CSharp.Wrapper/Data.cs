using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace NativeStruct.CSharp.Wrapper
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct DataNative
    {
        public double objective;
        public double* paramters;
        public int parameterCount;
        public double* objectives;
        public int objectiveCount;

        [SuppressUnmanagedCodeSecurity]
        [DllImport("NativeStruct.Cpp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_data")]
        public static extern DataNative* New();

        [SuppressUnmanagedCodeSecurity]
        [DllImport("NativeStruct.Cpp.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "free_data")]
        public static extern void Free(DataNative* data);
    }

    internal static class DataNativeExtensions
    {
        public static unsafe Data ToManaged(this DataNative data)
        {
            var parameters = new List<double>();
            var objectives = new List<double>();

            var managedData = new Data
            {
                Objective = data.objective,
                Objectives = objectives,
                Parameters = parameters
            };

            for (int i = 0; i < data.parameterCount; ++i)
            {
                parameters.Add(data.paramters[i]);
            }
            for (int i = 0; i < data.objectiveCount; ++i)
            {
                objectives.Add(data.objectives[i]);
            }

            return managedData;
        }
    }


    public class Data
    {
        public double Objective { get; set; }
        public IEnumerable<double> Parameters { get; set; }
        public IEnumerable<double> Objectives { get; set; }

        public static unsafe Data GetData()
        {
            var dataNative = default(DataNative*);

            try
            {
                dataNative = DataNative.New();

                return dataNative->ToManaged();
            }
            finally
            {
                DataNative.Free(dataNative);
            }
        }

    }

}
