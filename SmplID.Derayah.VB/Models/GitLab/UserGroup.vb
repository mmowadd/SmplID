Public Class UserGroup

    Public Property brands As List(Of Integer)
    Public Property groupId As Integer
    Public Property user As Integer

    Public Sub New()
        brands = New List(Of Integer)({101, 102})
    End Sub
End Class
