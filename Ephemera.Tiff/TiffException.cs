﻿using System;
using System.Runtime.Serialization;

namespace Ephemera.Tiff
{
    /// <summary>
    /// Represents errors that occur when reading or writing a <see cref="TiffDocument"/>.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class TiffException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TiffException"/> class.
        /// </summary>
        public TiffException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TiffException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TiffException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TiffException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public TiffException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TiffException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected TiffException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}