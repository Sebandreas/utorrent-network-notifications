'Copyright 2011 Zarek Jenkinson. All rights reserved.

'Redistribution and use in source and binary forms, with or without modification, are
'permitted provided that the following conditions are met:

'1. Redistributions of source code must retain the above copyright notice, this list of
'   conditions and the following disclaimer.

'2. Redistributions in binary form must reproduce the above copyright notice, this list
'   of conditions and the following disclaimer in the documentation and/or other materials
'   provided with the distribution.

'THIS SOFTWARE IS PROVIDED BY Zarek Jenkinson ''AS IS'' AND ANY EXPRESS OR IMPLIED
'WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
'FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL Zarek Jenkinson OR
'CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
'CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
'SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
'ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
'NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
'ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

Imports System.Net.Sockets
Imports System.Text
Class Module1
    Public Shared Sub Main()
        Dim message As String
        Dim ipaddresses() As String
        message = My.Application.CommandLineArgs.Item(0)
        Dim ipaddressescommadelimited As String = IniFile.GetFromINI("settings", "ipaddresses", "", My.Application.Info.DirectoryPath & "\settings.ini")
        ipaddresses = Split(ipaddressescommadelimited, ",")
        For Each ipaddress As String In ipaddresses
            Dim result = sendMessage(ipaddress, message)
        Next

    End Sub
    Public Shared Function sendMessage(ByVal ip As String, ByVal message As String)
        Try
            Dim tcpClient As New System.Net.Sockets.TcpClient()
            Console.WriteLine("Connecting to " & ip)
            tcpClient.Connect(ip, 12882)
            Dim networkStream As NetworkStream = tcpClient.GetStream()
            If networkStream.CanWrite And networkStream.CanRead Then
                Console.WriteLine("Connected.")
                ' Do a simple write.
                Console.WriteLine("Sending message...")
                Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(message)
                networkStream.Write(sendBytes, 0, sendBytes.Length)
                ' Read the NetworkStream into a byte buffer.
                Dim bytes(tcpClient.ReceiveBufferSize) As Byte
                networkStream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
                ' Output the data received from the host to the console.
                Dim returndata As String = Encoding.ASCII.GetString(bytes)
                Console.WriteLine("Closing connection to " & ip)
                tcpClient.Close()

            Else
                If Not networkStream.CanRead Then
                    Console.WriteLine("Error: cannot not write data to this stream.")
                    Console.WriteLine("Closing connection to " & ip)
                    tcpClient.Close()
                Else
                    If Not networkStream.CanWrite Then
                        Console.WriteLine("Error: cannot read data from this stream.")
                        Console.WriteLine("Closing connection to " & ip)
                        tcpClient.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Exception: " & ex.Message)
        End Try

    End Function
End Class
