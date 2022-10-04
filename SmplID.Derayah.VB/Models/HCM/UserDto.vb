Imports Newtonsoft.Json

Namespace Models.HCM
    Public Class UserDto
        Public Property id As String
        Public Property userName As String
        Public Property name As UserName
        Public Property active As String
        Public Property displayName As String
        Public Property PersonNumber As String

        <JsonProperty("urn:scim:schemas:extension:fa:2.0:faUser")>
        Public Property schemaExtension As SchemasExtension

    End Class
    Public Class User
        Public Property id As String
        Public Property userName As String
        Public Property givenName As String
        Public Property familyName As String
        Public Property displayName As String
        Public Property PersonNumber As String
        Public Property active As String

    End Class

    Public Class UserName
        Public Property givenName As String
        Public Property familyName As String
    End Class
End Namespace

Public Class WorkerInformation
    Public Property personNumber As String
    Public Property manager As String
    Public Property businessUnit As String
    Public Property department As String
End Class

Public Class SchemasExtension
    Public Property userCategory As String
    Public Property accountType As String
    Public Property workerInformation As WorkerInformation
End Class
