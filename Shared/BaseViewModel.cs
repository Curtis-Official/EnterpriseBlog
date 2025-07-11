using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EnterpriseBlog.Shared
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        private readonly ILogger _logger;
        private readonly bool _isDevFallbackEnabled;
        protected bool IsDevFallbackEnabled => _isDevFallbackEnabled;

        private bool _isBusy;
        private string? _errorMessage;

        public bool IsBusy
        {
            get => _isBusy;
            private set => SetProperty(ref _isBusy, value);
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected BaseViewModel(ILogger logger, IOptions<DevOptions> options)
        {
            _logger = logger;
            _isDevFallbackEnabled = options.Value.IsDevFallbackEnabled;
        }

        protected void LogException(Exception ex, [CallerMemberName] string? caller = null)
        {
            _logger.LogError(ex, "Error in {Caller}: {Message}", caller, ex.Message);
        }

        //func vs action
        //func implies side effect without return value 
        //action implies a computation that returns something 
        protected async Task RunSafeAsync(Func<Task> action, [CallerMemberName] string? caller = null)
        {
            try
            {
                IsBusy = true;
                ErrorMessage = null;
                await action();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LogException(ex, caller);
            }
            finally
            {
                IsBusy = false;
            }
        }


        protected async Task<T?> RunSafeAsync<T>(Func<Task<T>> func, [CallerMemberName] string? caller = null)
        {
            try
            {
                IsBusy = true;
                ErrorMessage = null;
                return await func();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LogException(ex, caller);
                return default;
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task<ResponseEnvelope<T>> RunSafeAsync<T>(Func<Task<ResponseEnvelope<T>>> func, [CallerMemberName] string? caller = null)
        {
            try
            {
                IsBusy = true;
                ErrorMessage = null;
                var result = await func();

                if (result == null && IsDevFallbackEnabled)
                {
                    return ResponseEnvelope<T>.CreateFailureEnvelope("Dev fallback: null response.");
                }

                return result!;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LogException(ex, caller);
                return ResponseEnvelope<T>.CreateFailureEnvelope(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
