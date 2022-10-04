Imports SmplID.Derayah.VB.Data

Module Program
    Private ReadOnly _HCMBaseURL As String = "https://fa-erqw-test-saasfaprod1.fa.ocs.oraclecloud.com"
    Private ReadOnly _HCMUser As String = "12"
    Private ReadOnly _HCMPassword As String = "welcome123"
    Private ReadOnly _HCMPath As String = "C:\Files\HCM"

    Private ReadOnly _GitLabBase As String = "https://gitlab.com/api/v4"
    Private ReadOnly _AccessToken As String = "glpat-XtPkG-9zFcTbiVtDWB6X"
    Private ReadOnly _GitLabPath As String = "C:\Files\GitLab"
    Private ReadOnly _MainGroupId As String = "8435666"

    Private ReadOnly _BOSBaseURL As String = "https://cbs.uat.nonprod.d360apps.com/iam"
    Private ReadOnly _BOSAccessToken As String = "84a959b5-7457-4480-8062-db004c2a397e"
    Private ReadOnly _BOSOCPTokend As String = "qVVYuK9Y56mufZ0clDSg1nYgoa62K3JU"
    Private ReadOnly _BOSPath As String = "C:\Files\BOS"

    Sub Main()

        'CreateGitLabCSVs()
        'CreateHCMCSVs()
        'CreateBOSCSVs()

        Dim userID As String = GetUserIDByEmail("mohamed.moawad@smplid.com")
        System.Console.WriteLine(userID)
        System.Console.ReadLine()

    End Sub
    Public Function CreateHCMCSVs() As Boolean

        Dim svc As New HCMService(_HCMBaseURL, _HCMUser, _HCMPassword)
        Try
            Utilities.CreateCsvFile(_HCMPath & "\Users.csv", svc.GetUsers())
            Utilities.CreateCsvFile(_HCMPath & "\Roles.csv", svc.GetRoles())
            Utilities.CreateCsvFile(_HCMPath & "\UsersInRoles.csv", svc.GetUsersInRoles())

            Utilities.CreateCsvFile(_HCMPath & "\Departments.csv", svc.GetDepartments())
            Utilities.CreateCsvFile(_HCMPath & "\Positions.csv", svc.GetPositions())
            Utilities.CreateCsvFile(_HCMPath & "\Employees.csv", svc.GetEmployees())
            Utilities.CreateCsvFile(_HCMPath & "\Locations.csv", svc.GetLocations())
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function CreateGitLabCSVs() As Boolean

        Dim svc As New GitlabService(_GitLabBase, _AccessToken)
        Try
            Utilities.CreateCsvFile($"{_GitLabPath}\Entitlements.csv", svc.GetEntitlements(_MainGroupId))
            Utilities.CreateCsvFile($"{_GitLabPath}\UserInEntitlements.csv", svc.GetUsersEntitlements(_MainGroupId))
            Utilities.CreateCsvFile($"{_GitLabPath}\Users.csv", svc.GetUsers(_MainGroupId))
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function CreateBOSCSVs() As Boolean

        Dim svc As New BOSService(_BOSBaseURL, _BOSAccessToken, _BOSOCPTokend)
        ' svc.RemoveUserFromGroup(1000, 4264)
        'svc.AddUserToGroup(1000, 2005)
        Try
            'Utilities.CreateCsvFile($"{_BOSPath}\Entitlements.csv", svc.GetEntitlements())
            ' Utilities.CreateCsvFile($"{_BOSPath}\Users.csv", svc.GetUsers())
            Utilities.CreateCsvFile($"{_BOSPath}\UserInEntitlements.csv", svc.GetUsersEntitlements())
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function GetUserIDByEmail(ByVal email As String) As String

        Dim svc As New BOSService(_BOSBaseURL, _BOSAccessToken, _BOSOCPTokend)
        Dim res As String

        Try
            res = svc.GetUserIdByEmail(email)
        Catch ex As Exception
            Return ex.Message
        End Try
        Return res
    End Function

End Module


