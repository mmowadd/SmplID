Imports SmplID.Derayah.VB.Models.HCM

Namespace Data
    Public Class HCMService

        Private ReadOnly _headers As Dictionary(Of String, String)
        Private ReadOnly _baseUrl As String

        Public Sub New(baseUrl As String, userName As String, password As String)
            _headers = New Dictionary(Of String, String) From {
                {"Authorization", $"Basic {Convert.ToBase64String(Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password))}"}
            }
            _baseUrl = baseUrl
        End Sub

        Public Function GetEmployeeUniqueNumber(personNumbr As String) As String
            Dim url = BuildUrl($"emps?q=PersonNumber={personNumbr}", False)
            Dim emp = Utilities.SendGetRequest(Of Response(Of EmployeeDto))(url, _headers, "application/vnd.oracle.adf.resourcecollection+json")?.items.FirstOrDefault()
            Return emp?.links.FirstOrDefault(Function(t) t.rel = "self").href.ReverseGetUntilOrEmpty()
        End Function
        Public Function GetEmployees() As List(Of Employee)
            Dim url = BuildUrl("emps?limit=999999&&expand=assignments", False)
            Return Utilities.SendGetRequest(Of Response(Of EmployeeDto))(url, _headers, "application/vnd.oracle.adf.resourcecollection+json")?.items.ToEmployeesList()
        End Function
        Public Sub UpdateEmployeeEmail(empUniqueNumber As String, email As String)
            Dim url = BuildUrl($"emps/{empUniqueNumber}", False)
            Dim employee = New EmployeeUpdateDto With {.WorkEmail = email}

            Utilities.SendRequest(url, "PATCH", employee, _headers)
        End Sub
        Public Function GetDepartments() As List(Of Department)
            Dim url = BuildUrl("departmentsLov?fields=OrganizationId,Name&limit=999999", False)
            Return Utilities.SendGetRequest(Of Response(Of Department))(url, _headers, "application/vnd.oracle.adf.resourcecollection+json")?.items
        End Function
        Public Function GetLocations() As List(Of Location)
            Dim url = BuildUrl("locations?fields=LocationId,LocationCode,LocationName,AddressLine1,Country,TownOrCity&limit=999999", False)
            Return Utilities.SendGetRequest(Of Response(Of Location))(url, _headers, "application/vnd.oracle.adf.resourcecollection+json")?.items
        End Function
        Public Function GetPositions() As List(Of Position)
            Dim url = BuildUrl("positions?fields=PositionId,PositionCode,Name&limit=999999", False)
            Return Utilities.SendGetRequest(Of Response(Of Position))(url, _headers, "application/vnd.oracle.adf.resourcecollection+json")?.items
        End Function
        Public Function GetUsers() As List(Of User)
            Dim url = BuildUrl("Users?attributes=id", True)
            Dim scimUsers = Utilities.SendGetRequest(Of ScimResponse(Of UserDto))(url, _headers)
            Dim users = New List(Of User)
            For Each scimUser In scimUsers.Resources
                Try
                    Dim user = GetUser(scimUser.id)
                    If user IsNot Nothing Then
                        users.Add(user)
                    End If
                Catch ex As Exception
                End Try
            Next
            Return users
        End Function
        Public Function GetRoles() As List(Of Role)
            Dim url = BuildUrl("Roles", True)
            Return Utilities.SendGetRequest(Of ScimResponse(Of Role))(url, _headers).Resources
        End Function
        Public Sub UpdateRoleUsers(UserId As String, RoleId As String, Operation As String)
            Dim members = New List(Of HCMRoleMember) From {
                New HCMRoleMember With {.value = UserId, .operation = Operation}
            }
            Dim role = New Role With {.members = members}
            Dim url = BuildUrl($"Roles/{RoleId}", True)
            Utilities.SendRequest(url, "PATCH", role, _headers)
        End Sub
        Public Function GetUsersInRoles() As List(Of UsersInRoles)
            Dim url = BuildUrl("Roles", True)
            Return Utilities.SendGetRequest(Of ScimResponse(Of Role))(url, _headers).Resources.ToUsersInRolesList()
        End Function
        Private Function GetUser(userId As String) As User
            Dim url = BuildUrl($"Users/{userId}", True)
            Dim fullUser = Utilities.SendGetRequest(Of UserDto)(url, _headers)

            If fullUser Is Nothing Then
                Return Nothing
            End If

            Return New User With {
                        .id = userId,
                        .userName = fullUser?.userName,
                        .givenName = fullUser?.name.givenName,
                        .familyName = fullUser?.name.familyName,
                        .displayName = fullUser?.displayName,
                        .PersonNumber = fullUser?.schemaExtension?.workerInformation?.personNumber,
                        .active = fullUser?.active
                    }
        End Function
        Private Function BuildUrl(subUrl As String, isScim As Boolean)
            Return If(isScim, $"{_baseUrl}/hcmRestApi/scim/{subUrl}", $"{_baseUrl}/hcmRestApi/resources/11.13.18.05/{subUrl}")
        End Function
    End Class
End Namespace