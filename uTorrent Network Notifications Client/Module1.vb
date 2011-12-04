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
