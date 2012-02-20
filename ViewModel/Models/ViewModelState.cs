namespace ViewModel.Models
{
    /// <summary>
    /// Represents the state of the view model
    /// </summary>
    public enum ViewModelState
    {
        /// <summary>
        /// No operation is going on
        /// </summary>
        Still, 
        /// <summary>
        /// Downloading from the server
        /// </summary>
        Loading, 
        /// <summary>
        /// Uploading to the server
        /// </summary>
        Uploading,
        /// <summary>
        /// Exception happened during last operation
        /// </summary>
        Faulted
    }
}