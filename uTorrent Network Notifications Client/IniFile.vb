Module IniFile

    '// VB Web Code Example
    '// www.vbweb.co.uk
    '// mod varable types for VB 2005
    '//by M Jenkinson 20/2/2006

    ' DLL declarations

    Private Declare Function GetPrivateProfileStringKey Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Long

    '// Functions
    Function GetFromINI(ByVal sSection As String, ByVal sKey As String, ByVal sDefault As String, ByVal sIniFile As String)
        Dim sBuffer As New String(Chr(0), 255), lRet As Long
        ' Fill String with 255 spaces

        ' Call DLL
        lRet = GetPrivateProfileStringKey(sSection, sKey, "", sBuffer, Len(sBuffer), sIniFile)
        If lRet = 0 Then
            ' DLL failed, save default
            If sDefault <> "" Then AddToINI(sSection, sKey, sDefault, sIniFile)
            GetFromINI = sDefault
        Else
            ' DLL successful
            ' return string
            GetFromINI = Left(sBuffer, InStr(sBuffer, Chr(0)) - 1)
        End If
    End Function

    '// Returns True if successful. If section does not
    '// exist it creates it.
    Function AddToINI(ByVal sSection As String, ByVal sKey As String, ByVal sValue As String, ByVal sIniFile As String) As Boolean
        Dim lRet As Long
        ' Call DLL
        lRet = WritePrivateProfileString(sSection, sKey, sValue, sIniFile)
        AddToINI = (lRet)
    End Function
End Module
