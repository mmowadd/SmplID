Imports SmplID.Derayah.VB.Models.Common
Imports SmplID.Derayah.VB.Models.BOS


Namespace Data
    Public Class BOSService

        Private ReadOnly _headers As Dictionary(Of String, String)
        Private ReadOnly _baseUrl As String

        Public Sub New(baseUrl As String, accessToken As String, ocpToken As String)
            _baseUrl = baseUrl
            _headers = New Dictionary(Of String, String) From {
                {"Authorization", accessToken},
                {"Ocp-Apim-Subscription-Key", ocpToken},
                {"BrandId", "101"}
            }
        End Sub
        Public Function GetEntitlements() As List(Of Entitlement)

            Dim groupsUrl = $"{_baseUrl}/groups/all"

            Dim groups = Utilities.SendGetRequest(Of List(Of Group))(groupsUrl, _headers)

            Return groups.ToEntitlementList().ToList()
        End Function
        Public Function GetUsers() As List(Of User)
            Dim usersUrl = $"{_baseUrl}/users/all"

            Dim users = Utilities.SendGetRequest(Of List(Of User))(usersUrl, _headers)

            Return users.ToList()
        End Function
        Public Function GetUserIdByEmail(ByVal email As String) As String
            Dim usersUrl = $"{_baseUrl}/users/all"

            Dim users = Utilities.SendGetRequest(Of List(Of User))(usersUrl, _headers)

            Return users.FirstOrDefault(Function(r) r.email = email)?.id
        End Function
        Public Function GetUsersEntitlements() As List(Of UserEntitlement)

            Dim userEntitlements As New List(Of UserEntitlement)

            Dim usersUrl = $"{_baseUrl}/users/all"

            Dim users = Utilities.SendGetRequest(Of List(Of User))(usersUrl, _headers)

            For Each user As User In users
                For Each group As Group In user.groups
                    userEntitlements.Add(New UserEntitlement With {.UserName = user.id, .EntitlementName = group.id})
                Next
            Next
            Return userEntitlements
        End Function
        Public Sub CreateUser(userEmail As String)
            Dim user As New User With {.email = userEmail}
            Utilities.SendRequest($"{_baseUrl}/users", "POST", user, _headers, "application/json")
        End Sub
        Public Sub DisableUser(userID As String)
            Utilities.SendRequest($"{_baseUrl}/users/{userID}", "DELETE", _headers)
        End Sub
        Public Sub RemoveUserFromGroup(userID As String, groupID As String)
            Utilities.SendRequest($"{_baseUrl}/groups/{groupID}/member/{userID}", "DELETE", _headers)
        End Sub
        Public Sub AddUserToGroup(userID As String, groupID As String)
            Dim userGroup As New UserGroup With {.user = userID, .groupId = groupID}
            Utilities.SendRequest($"{_baseUrl}/groups/users", "POST", userGroup, _headers, "application/json")
        End Sub


    End Class
End Namespace
