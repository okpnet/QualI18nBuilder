using global::System.Runtime.Serialization;

namespace I18nBuilderrTest.I18nBuilder.I18nException
{
    public sealed class I18nBuilderException : Exception
    {
        public I18nBuilderException()
        {
        }

        public I18nBuilderException(string message) : base(message)
        {
        }

        public I18nBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public I18nBuilderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
