using System;
using System.IO;

namespace ChessSharp;

public class UciReaderWriter : IDisposable
{
    private readonly Stream _input;
    private readonly Stream _output;

    private bool _disposed;

    public UciReaderWriter(Stream input, Stream output)
    {
        _input = input;
        _output = output;
    }

    public void Start()
    {
        using StreamReader streamReader = new StreamReader(_input);

        int read;

        while ((read = streamReader.Read()) is not -1)
        {

        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _input.Dispose();
                _output.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
