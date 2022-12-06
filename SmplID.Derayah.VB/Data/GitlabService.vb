Imports SmplID.Common
Imports SmplID.Derayah.VB.Models.Common
Imports SmplID.Derayah.VB.Models.Gitlab

Namespace Data
    Public Class GitlabService


        Private ReadOnly _headers As Dictionary(Of String, String)
        Private ReadOnly _baseUrl As String

        Public Sub New(baseUrl As String, accessToken As String)
            _headers = New Dictionary(Of String, String) From {
                {"Authorization", $"Bearer {accessToken}"}
            }
            _baseUrl = baseUrl
        End Sub


        Public Function GetEntitlements(mainGroupId As String, Optional unique As Boolean = False) As List(Of Entitlement)

            Dim groupsUrl = $"{_baseUrl}/groups"
            Dim projectsUrl = $"{_baseUrl}/groups/{mainGroupId}/projects?include_subgroups=true"

            Dim groups = Utilities.SendGetRequest(Of List(Of Group))(groupsUrl, _headers)
            Dim projects = Utilities.SendGetRequest(Of List(Of Project))(projectsUrl, _headers)

            Return groups.ToEntitlementList(unique).Concat(projects.ToEntitlementList(unique)).ToList()
        End Function
        Public Function GetUsers(mainGroupId As String) As List(Of User)
            Dim members = GetMembers(mainGroupId)
            Dim users = New List(Of User)()

            For Each member In members.Distinct(New MembersComparer()).ToList()
                Dim gitLabUser = Utilities.SendGetRequest(Of Member)($"{_baseUrl}/users/{member.id}", _headers)
                users.Add(gitLabUser.ToUser())
            Next
            Return users
        End Function
        Public Function GetUsersEntitlements(mainGroupId As String) As List(Of UserEntitlement)
            Dim members = GetMembers(mainGroupId)
            Return members.ToUserEntitlmentList()
        End Function
        Public Sub AssignUserEntitlement(userId As String, entitlementId As String, entitlementType As String)
            Dim resourceId = entitlementId.Split("-"c)(0).Trim
            Dim accessLevel = entitlementId.Split("-"c)(1).Trim

            Dim fullUrl = $"{_baseUrl}/{entitlementType.ToLower}s/{resourceId}/members?user_id={userId.Trim}&access_level={accessLevel}"
            Utilities.SendRequest(fullUrl, "POST", _headers)
        End Sub
        Public Sub RemoveUserEntitlement(userId As String, entitlementId As String, entitlementType As String)
            Dim resourceId = entitlementId.Split("-"c)(0).Trim

            Dim fullUrl = $"{_baseUrl}/{entitlementType.ToLower.Trim}s/{resourceId.Trim}/members/{userId.Trim}"
            Utilities.SendRequest(fullUrl, "DELETE", _headers)
        End Sub
        Public Sub UpdateUserEntitlement(userId As String, entitlementId As String, entitlementType As String)
            Dim resourceId = entitlementId.Split("-"c)(0).Trim
            Dim accessLevel = entitlementId.Split("-"c)(1).Trim

            Dim fullUrl = $"{_baseUrl}/{entitlementType.ToLower.Trim}s/{resourceId}/members/{userId.Trim}?access_level={accessLevel}"
            Utilities.SendRequest(fullUrl, "PUT", _headers)
        End Sub
        Private Function GetMembers(mainGroupId As String) As List(Of Member)

            Dim entitlements = GetEntitlements(mainGroupId, True)
            Dim members = New List(Of Member)()
            Dim groupMembers = New List(Of Member)()
            Dim url As String = ""
            Dim logFilePath As String = ""
            For Each entitlement In entitlements
                If entitlement.ResourceType = "Group" Then
                    url = $"{_baseUrl}/groups/{entitlement.EntitlementID.Split("-"c)(0)}/members"
                ElseIf entitlement.ResourceType = "Project" Then
                    url = $"{_baseUrl}/projects/{entitlement.EntitlementID.Split("-"c)(0)}/members"
                End If
                groupMembers = Utilities.SendGetRequest(Of List(Of Member))(url, _headers)
                groupMembers.ToList.ForEach(Sub(x) x.entitlementName = $"{entitlement.EntitlementName}-{x.access_level.ToAccessLevelName}")
                members.AddRange(groupMembers)
            Next
            Return members
        End Function
    End Class
End Namespace
