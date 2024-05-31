using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0142;


namespace R5T.S0043
{
    [DraftTypeMarker]
    public class ServicesContext : IDisposable, IAsyncDisposable
    {
        private ServiceProvider ServiceProvider { get; set; }


        public ServicesContext(ServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public T GetService<T>()
        {
            var output = this.ServiceProvider.GetRequiredService<T>();
            return output;
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsyncCore().ConfigureAwait(false);

            this.Dispose(disposing: false);

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
            GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                (this.ServiceProvider as IDisposable)?.Dispose();

                this.ServiceProvider = null;
            }
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (this.ServiceProvider is not null)
            {
                await this.ServiceProvider.DisposeAsync().ConfigureAwait(false);
            }

            this.ServiceProvider = null;
        }
    }
}
