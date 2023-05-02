namespace PresTrust.FloodMitigation.Application.ApiExceptions
{
    /// <summary>
    ///     Api validation exception
    /// </summary>
    public class ApiModelValidationException : Exception
    {
        public string[] Errors { get; private set; }

        /// <summary>
        ///     ctor
        /// </summary>
        public ApiModelValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new string[] { };
        }

        public ApiModelValidationException(string[] errors)
            : this()
        {
            Errors = errors;
        }
    }
}
