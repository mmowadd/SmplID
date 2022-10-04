Namespace Models.HCM
    Public Class Role
        Public Property id As String
        Public Property name As String
        Public Property displayName As String
        Public Property description As String
        Public Property members As List(Of HCMRoleMember)
    End Class

    Public Class HCMRoleMember
        Public Property value As String
        Public Property operation As String
    End Class
    Public Class UsersInRoles
        Public Property userId As String
        Public Property roleId As String
    End Class
End Namespace
