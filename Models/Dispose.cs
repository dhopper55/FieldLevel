using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FieldLevel.Models
{
    public class DisposableWrapper : IDisposable
    {
        private IDisposable _unmanagedResource;
        private bool _disposed = false;
        public DisposableWrapper(IDisposable unmanagedResource)
        {
            _unmanagedResource = unmanagedResource;

            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _unmanagedResource?.Dispose();
            }

            _disposed = true;
        }
    }

}