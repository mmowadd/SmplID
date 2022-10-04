Namespace Models.HCM
    Public Class EmployeeDto
        Public Property Salutation As String
        Public Property FirstName As String
        Public Property MiddleName As String
        Public Property LastName As String
        Public Property DisplayName As String
        Public Property PersonNumber As String
        Public Property WorkPhoneNumber As String
        Public Property WorkMobilePhoneNumber As String
        Public Property HomePhoneNumber As String
        Public Property WorkEmail As String
        Public Property City As String
        Public Property Country As String
        Public Property HireDate As String
        Public Property TerminationDate As String
        Public Property Gender As String
        Public Property PersonId As ULong
        Public Property UserName As String
        Public Property WorkerType As String
        Public Property links As List(Of EmployeeLink)
        Public Property assignments As List(Of Assignment)
    End Class
    Public Class Employee
        Public Property Salutation As String
        Public Property FirstName As String
        Public Property MiddleName As String
        Public Property LastName As String
        Public Property DisplayName As String
        Public Property PersonNumber As String
        Public Property WorkPhoneNumber As String
        Public Property WorkMobilePhoneNumber As String
        Public Property HomePhoneNumber As String
        Public Property WorkEmail As String
        Public Property City As String
        Public Property Country As String
        Public Property HireDate As String
        Public Property TerminationDate As String
        Public Property Gender As String
        Public Property PersonId As ULong
        Public Property UserName As String
        Public Property WorkerType As String
        Public Property LocationId As ULong?
        Public Property PositionId As ULong?
        Public Property ManagerId As ULong?
        Public Property DepartmentId As ULong?
    End Class

    Public Class Assignment
        Public Property LocationId As ULong?
        Public Property DepartmentId As ULong?
        Public Property AssignmentStatus As String
        Public Property PositionId As ULong?
        Public Property ManagerId As ULong?
    End Class
    Public Class EmployeeLink
        Public Property rel As String
        Public Property href As String
    End Class
    Public Class EmployeeUpdateDto
        Public Property WorkEmail As String
    End Class
End Namespace
