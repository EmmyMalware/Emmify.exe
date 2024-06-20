Imports System.Runtime.InteropServices
Imports System.Threading

Public Class Form1
    <DllImport("user32.dll")>
    Private Shared Function SetWindowText(hWnd As IntPtr, text As String) As Boolean
    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = "the day is: " + DateAndTime.DateString
        Label3.Text = "your name is: " + Environment.UserName
        Label4.Text = "your pc name is: " + Environment.MachineName
        Dim process1 As Process = New Process()
        For Each process2 In Process.GetProcesses()
            Console.WriteLine("Process Name: {0} ", process2.ProcessName)
            Dim mainWindowHandle = process2.MainWindowHandle
            If mainWindowHandle = IntPtr.Zero Then
                Continue For
            End If
            Dim random1 As Random = New Random()
            Dim str = "Emmyify"
            Dim chArray = New Char(random1.Next(1, 50) - 1) {}
            Dim index = 0
            While index < chArray.Length
                chArray(index) = str(random1.[Next](str.Length))
                Threading.Interlocked.Increment(index)
            End While
            Dim text As String = New String(chArray)
            SetWindowText(GetForegroundWindow(), text)
            SetWindowText(process2.Handle, text)
            SetWindowText(mainWindowHandle, text)
            Thread.Sleep(100)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lol As Form2 = New Form2()
        lol.Show()
    End Sub
End Class
