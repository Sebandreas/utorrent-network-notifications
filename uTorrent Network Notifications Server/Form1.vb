Imports System.Net.Sockets
Imports System.Text
Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = 1
        Me.Height = 1
        Me.Hide()
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ' Must listen on correct port- must be same as port client wants to connect on.
        Const portNumber As Integer = 12882
        Dim tcpListener As New TcpListener(portNumber)
        tcpListener.Start()
        Try
            'Accept the pending client connection and return 
            'a TcpClient initialized for communication. 
            Dim tcpClient As TcpClient = tcpListener.AcceptTcpClient()
            ' Get the stream
            Dim networkStream As NetworkStream = tcpClient.GetStream()
            ' Read the stream into a byte array
            Dim bytes(tcpClient.ReceiveBufferSize) As Byte
            networkStream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
            ' Return the data received from the client to the console.
            Dim clientdata As String = Encoding.ASCII.GetString(bytes)
            NotifyIcon1.BalloonTipText = clientdata
            NotifyIcon1.BalloonTipTitle = "uTorrent Network"
            NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
            NotifyIcon1.ShowBalloonTip(10)
            Dim responseString As String = "ack"
            Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(responseString)
            networkStream.Write(sendBytes, 0, sendBytes.Length)
            'Any communication with the remote client using the TcpClient can go here.
            'Close TcpListener and TcpClient.
            tcpClient.Close()
            tcpListener.Stop()
        Catch ex As Exception

        End Try
        BackgroundWorker1.CancelAsync()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        BackgroundWorker1.RunWorkerAsync()
    End Sub
End Class
