using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples.ObjectLifeCycle
{
    class DisposableClass : IDisposable
    {
        private bool ResoucesAlreadyFreed = false;

        private void FreeResources(bool freeManagedResources)
        {
            if (!ResoucesAlreadyFreed)
            {
                if (freeManagedResources)
                {
                    Console.WriteLine("Freeing managed resources...");
                }

                Console.WriteLine("Freeing unmanaged resources...");

                ResoucesAlreadyFreed = true;

                GC.SuppressFinalize(this);
            }
        }

        ~DisposableClass()
        {
            FreeResources(false);
        }

        public void Dispose()
        {
            FreeResources(true);
        }
    }
}
