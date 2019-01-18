using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcomVS.Tasks
{
    class DisposeUnmanaged<T> : IDisposable
    {
        public T Handle { get; private set; }
        private Action<T> disposeAction;

        public DisposeUnmanaged( T obj, Action<T> disposeAction )
        {
            Handle = obj;
            this.disposeAction = disposeAction;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose( bool disposing )
        {
            if( !disposedValue ) {
                if( disposing ) {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposeAction( Handle );
                Handle = default(T);
                disposeAction = null;

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~DisposeUnmanaged()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( false );
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose( true );
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize( this );
        }
        #endregion
    }
}
