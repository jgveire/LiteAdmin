namespace LiteAdmin.Constants
{
    /// <summary>
    /// JSON error messages.
    /// </summary>
    public static class JsonErrorMessage
    {
        /// <summary>
        /// Internal JSON-RPC error.
        /// </summary>
        public const string InternalError = "Internal JSON-RPC error.";

        /// <summary>
        /// Invalid method parameter(s).
        /// </summary>
        public const string InvalidParams = "Invalid method parameter(s).";

        /// <summary>
        /// The JSON sent is not a valid Request object.
        /// </summary>
        public const string InvalidRequest = "The JSON sent is not a valid Request object.";

        /// <summary>
        /// The method does not exist / is not available.
        /// </summary>
        public const string MethodNotFound = "The method does not exist or is not available.";

        /// <summary>
        /// Invalid JSON was received by the server.
        /// An error occurred on the server while parsing the JSON text.
        /// </summary>
        public const string ParseError = "Invalid JSON was received by the server.";
    }
}
