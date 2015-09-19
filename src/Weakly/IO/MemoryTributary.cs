using System;
using System.Collections.Generic;
using System.IO;

namespace Weakly
{
    /// <summary>
    /// MemoryTributary is a re-implementation of <see cref="MemoryStream"/> that uses a dynamic list of byte arrays as a backing store,
    /// instead of a single byte array, the allocation of which will fail for relatively small streams as it requires contiguous memory.
    /// </summary>
    /// <remarks>Based on http://memorytributary.codeplex.com/ by Sebastian Friston.</remarks>
    public sealed class MemoryTributary : Stream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTributary"/> class with an expandable capacity initialized to zero.
        /// </summary>
        public MemoryTributary()
        {
            Position = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTributary"/> class based on the specified byte array.
        /// </summary>
        /// <param name="source">The array of unsigned bytes from which to create the current stream.</param>
        public MemoryTributary(byte[] source)
        {
            Write(source, 0, source.Length);
            Position = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryTributary"/> class with an expandable capacity initialized as specified.
        /// </summary>
        /// <param name="length">The initial size of the internal array in bytes.</param>
        public MemoryTributary(int length)
        {
            SetLength(length);
            Position = length;
            var d = Block;   //access block to prompt the allocation of memory
            Position = 0;
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <returns>true if the stream supports reading; otherwise, false.</returns>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <returns>true if the stream supports seeking; otherwise, false.</returns>
        public override bool CanSeek
        {
            get { return true; }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <returns>true if the stream supports writing; otherwise, false.</returns>
        public override bool CanWrite
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// </summary>
        /// <returns>A long value representing the length of the stream in bytes.</returns>
        public override long Length
        {
            get { return _length; }
        }

        /// <summary>
        /// Gets or sets the position within the current stream.
        /// </summary>
        /// <returns>The current position within the stream.</returns>
        public override long Position { get; set; }

        private long _length;

        private const long BlockSize = 65536;

        private readonly List<byte[]> _blocks = new List<byte[]>();

        /// <summary>
        /// The block of memory currently addressed by Position
        /// </summary>
        private byte[] Block
        {
            get
            {
                while (_blocks.Count <= BlockId)
                    _blocks.Add(new byte[BlockSize]);
                return _blocks[(int)BlockId];
            }
        }
        /// <summary>
        /// The id of the block currently addressed by Position
        /// </summary>
        private long BlockId
        {
            get { return Position / BlockSize; }
        }
        /// <summary>
        /// The offset of the byte currently addressed by Position, into the block that contains it
        /// </summary>
        private long BlockOffset
        {
            get { return Position % BlockSize; }
        }

        /// <summary>
        /// Cears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
        /// </returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            var lcount = (long)count;

            if (lcount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Number of bytes to copy cannot be negative.");
            }

            var remaining = (_length - Position);
            if (lcount > remaining)
                lcount = remaining;

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer), "Buffer cannot be null.");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Destination offset cannot be negative.");
            }

            var read = 0;
            do
	        {
                var copysize = Math.Min(lcount, (BlockSize - BlockOffset));
                Buffer.BlockCopy(Block, (int)BlockOffset, buffer, offset, (int)copysize);
                lcount -= copysize;
                offset += (int)copysize;

                read += (int)copysize;
                Position += copysize;

	        } while (lcount > 0);

            return read;
        }

        /// <summary>
        /// Sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
        /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
        /// <returns>
        /// The new position within the current stream.
        /// </returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = Length - offset;
                    break;
            }
            return Position;
        }

        /// <summary>
        /// Sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        public override void SetLength(long value)
        {
            _length = value;
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            var initialPosition = Position;
            try
            {
                do
                {
                    var copysize = Math.Min(count, (int)(BlockSize - BlockOffset));

                    EnsureCapacity(Position + copysize);

                    Buffer.BlockCopy(buffer, offset, Block, (int)BlockOffset, copysize);
                    count -= copysize;
                    offset += copysize;

                    Position += copysize;

                } while (count > 0);
            }
            catch (Exception)
            {
                Position = initialPosition;
                throw;
            }
        }

        /// <summary>
        /// Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.
        /// </summary>
        /// <returns>
        /// The unsigned byte cast to an Int32, or -1 if at the end of the stream.
        /// </returns>
        public override int ReadByte()
        {
            if (Position >= _length)
                return -1;

            var b = Block[BlockOffset];
            Position++;

            return b;
        }

        /// <summary>
        /// Writes a byte to the current position in the stream and advances the position within the stream by one byte.
        /// </summary>
        /// <param name="value">The byte to write to the stream.</param>
        public override void WriteByte(byte value)
        {
            EnsureCapacity(Position + 1);
            Block[BlockOffset] = value;
            Position++;
        }

        private void EnsureCapacity(long intendedLength)
        {
            if (intendedLength > _length)
                _length = intendedLength;
        }

        /// <summary>
        /// Returns the entire content of the stream as a byte array. This is not safe because the call to new byte[] may 
        /// fail if the stream is large enough. Where possible use methods which operate on streams directly instead.
        /// </summary>
        /// <returns>A byte[] containing the current data in the stream</returns>
        public byte[] ToArray()
        {
            var firstposition = Position;
            Position = 0;
            var destination = new byte[Length];
            Read(destination, 0, (int)Length);
            Position = firstposition;
            return destination;
        }

        /// <summary>
        /// Reads length bytes from source into the this instance at the current position.
        /// </summary>
        /// <param name="source">The stream containing the data to copy</param>
        /// <param name="length">The number of bytes to copy</param>
        public void ReadFrom(Stream source, long length)
        {
            var buffer = new byte[4096];
            do
            {
                var read = source.Read(buffer, 0, (int) Math.Min(4096, length));
                length -= read;
                Write(buffer, 0, read);

            } while (length > 0);
        }

        /// <summary>
        /// Writes the entire stream into destination, regardless of Position, which remains unchanged.
        /// </summary>
        /// <param name="destination">The stream to write the content of this stream to</param>
        public void WriteTo(Stream destination)
        {
            var initialpos = Position;
            Position = 0;
            CopyTo(destination);
            Position = initialpos;
        }
    }
}
